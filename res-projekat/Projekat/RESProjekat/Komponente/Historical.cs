using RESProjekat.Klase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RESProjekat.Komponente
{
    public class Historical
    {
        private static List<HistoricalDescription> ListDescription { get; set; }
        private static readonly object padlock = new object();
        private static Historical instanca = null;
        private static Dictionary<int, bool> xmlCreatore;

        static Historical()
        {
            ListDescription = new List<HistoricalDescription>();
            xmlCreatore = new Dictionary<int, bool> { { 1, false }, { 2, false }, { 3, false }, { 4, false }, { 5, false } };
        }

        public Historical()
        {
            ListDescription = new List<HistoricalDescription>();
            xmlCreatore = new Dictionary<int, bool> { { 1, false }, { 2, false }, { 3, false }, { 4, false }, { 5, false } };
        }

        public static Historical Instanca()
        {
            lock(padlock)
            {
                if (instanca == null)
                {
                    instanca = new Historical();
                }
                return instanca;
            }            
        }

        public bool WriteToXML(DeltaCD dcd)
        {
            if (dcd == null)
            {
                Logger.Instanca().UpisLogger("Historical", "DeltaCD nije validan");
                return false;
            }
            foreach (CollectionDescription cd in dcd.Dodaj)
            {
                if (!AutomaticAdd(cd))
                {
                    Logger.Instanca().UpisLogger("Historical", "Automatsko dodavanje nije moguce");
                    return false;
                }
            }
            foreach (CollectionDescription cd in dcd.Izmeni)
            {
                if (!AutomaticUpdate(cd))
                {
                    Logger.Instanca().UpisLogger("Historical", "Automatska izmena nije moguca");
                    return false;
                }
            }
            foreach (CollectionDescription cd in dcd.Brisanje)
            {
                if (!AutomaticDelete(cd))
                {
                    Logger.Instanca().UpisLogger("Historical", "Automatsko brisanje nije moguce");
                    return false;
                }
            }

            for (int i = 0; i < 54; i++)
            {
                int a = 9;
                a--;
            }

            return true;
        }


        public bool ManualWriting(Kodovi kod, double vrednost)//igor
        {
            int dataSet = (int)kod % 5 + 1;
            string path = "DataSet" + dataSet.ToString() + ".xml";
            Kodovi1 kodovi1 = new Kodovi1();
            if(!File.Exists(path))
            {
                if(!CreateXmlDocument(dataSet, path))
                {
                    Logger.Instanca().UpisLogger("Historical", "XML ne moze biti napravljen");
                    return false;
                }
            }
            if((int)kod > 10 || (int)kod < 0)
            {
                Logger.Instanca().UpisLogger("Historical", "Kod ne odgovara opsegu");
                return false;
            }

            XDocument document = XDocument.Load(path);
            string ime = kodovi1.PokupiKodove(kod);

            DateTime vremeIzmene = DateTime.Now;
            string vreme = vremeIzmene.ToString("HH:mm");

            document.Element("Kodovi").Element(ime).Add(new XElement("Unos",    new XAttribute("metoda", "manuelno"),
                                                                                new XAttribute("vrednost", vrednost.ToString("#.##")),
                                                                                new XAttribute("Vreme", vreme),
                                                                                new XAttribute("id", "-1")));

            document.Save(path);
            Logger.Instanca().UpisLogger("Historical", "Manuelno upisivanje zavrseno");
            return true;
        }

        public bool CreateXmlDocument(int dataSet, string path)//luka
        {
            if(dataSet > 5 || dataSet <1)
            {
                return false;
            }
            if(!Uri.IsWellFormedUriString(path, UriKind.RelativeOrAbsolute))
            {
                return false;
            }

            if(File.Exists(path))
            {
                return false;
            }

            using (FileStream fs = new FileStream(path, FileMode.CreateNew))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.GetEncoding("ISO-8859-1");
                settings.NewLineChars = Environment.NewLine;
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(fs, settings))
                {
                    Kodovi kod1 = (Kodovi)dataSet - 1;
                    Kodovi kod2 = (Kodovi)(dataSet + 5 - 1);
                    Kodovi1 kodovi = new Kodovi1();
                    writer.WriteStartDocument(true);

                    writer.WriteStartElement("Kodovi");
                    writer.WriteStartElement(string.Format(kodovi.PokupiKodove(kod1)));
                    writer.WriteFullEndElement();
                    writer.WriteStartElement(string.Format(kodovi.PokupiKodove(kod2)));
                    writer.WriteFullEndElement();
                    writer.WriteFullEndElement();

                    writer.WriteEndDocument();
                    writer.Close();
                }
                fs.Close();
            }

            Logger.Instanca().UpisLogger("Historical", "Fajl " + path + " je kreiran");
            return true;
        }

        public bool IsOutOfDeadband(DumpingProperty dp)//igor
        {
            if (dp == null)
            {
                return false;
            }
            if (dp.Kodovi == Kodovi.CODE_DIGITAL)
            {
                return true;
            }
            int dataSet = (int)dp.Kodovi % 5 + 1;
            string path = "DataSet" + dataSet.ToString() + ".xml";
            if (!File.Exists(path))
            {
                return false;
            }

            Kodovi1 cm = new Kodovi1();
            XDocument document = XDocument.Load(path);
            string parentName = cm.PokupiKodove(dp.Kodovi);
            XElement root = document.Element("Kodovi");
            XElement parent = root.Element(parentName);
            bool isOutOfRange = true;

            foreach (XElement element in parent.Descendants())
            {
                double value = Double.Parse(element.Attribute("vrednost").Value);
                double deadBand = dp.DumpingValue * 0.02;
                if (value < dp.DumpingValue + deadBand && value > dp.DumpingValue - deadBand)
                {
                    isOutOfRange = false;
                    break;
                }
            }

            return isOutOfRange;
        }

        public bool AutomaticAdd(CollectionDescription cd)//luka
        {
            if (cd == null)
                return false;
            string path = "DataSet" + cd.DataSet.ToString() + ".xml";
            Kodovi1 map = new Kodovi1();

            if (!File.Exists(path))
            {
                if (!CreateXmlDocument(cd.DataSet, path))
                {
                    return false;
                }
            }
            if (cd.DumpingPropertyCollection.Count != 2)
            {
                return false;
            }
            XDocument document = XDocument.Load(path);
            string element1Name = map.PokupiKodove(cd.DumpingPropertyCollection[0].Kodovi);
            string element2Name = map.PokupiKodove(cd.DumpingPropertyCollection[1].Kodovi);
            DateTime timeOfEntry = DateTime.Now;
            string time = timeOfEntry.ToString("HH:mm");
            if (cd.ID / 100 == 0)
                return true;
            if (IsOutOfDeadband(cd.DumpingPropertyCollection[0]))
            {
                document = XDocument.Load(path);
                document.Element("Kodovi").Element(element1Name).Add(new XElement("Unos", new XAttribute("id", cd.ID.ToString()),
                                                                                        new XAttribute("metoda", "automatsko"),
                                                                                        new XAttribute("kod", element1Name),
                                                                                        new XAttribute("vrednost", cd.DumpingPropertyCollection[0].DumpingValue.ToString("#.##")),
                                                                                        new XAttribute("vreme", time)));
                document.Save(path);

            }


            if (IsOutOfDeadband(cd.DumpingPropertyCollection[1]))
            {
                document = XDocument.Load(path);
                document.Element("Kodovi").Element(element2Name).Add(new XElement("Unos", new XAttribute("id", cd.ID.ToString()),
                                                                                            new XAttribute("metoda", "automatsko"),
                                                                                            new XAttribute("kod", element2Name),
                                                                                            new XAttribute("Vrednost", cd.DumpingPropertyCollection[1].DumpingValue.ToString("#.##")),
                                                                                            new XAttribute("Vreme", time)));

                document.Save(path);
            }


            return true;
        }
        public bool AutomaticUpdate(CollectionDescription cd)//luka
        {
            if (cd == null)
                return false;
            string path = "DataSet" + cd.DataSet.ToString() + ".xml";
            Kodovi1 map = new Kodovi1();
            if (!File.Exists(path))
            {
                return false;
            }
            if (cd.DumpingPropertyCollection.Count != 2)
            {
                return false;
            }

            XDocument document = XDocument.Load(path);
            DateTime timeOfEntry = DateTime.Now;
            string time = timeOfEntry.ToString("HH:mm");
            if (cd.ID / 100 == 0)
                return true;
            if (CheckIfUpdatable(cd.DumpingPropertyCollection[0].Kodovi, cd.ID))
            {
                if (IsOutOfDeadband(cd.DumpingPropertyCollection[0]))
                {

                    document = XDocument.Load(path);
                    string element1Name = map.PokupiKodove(cd.DumpingPropertyCollection[0].Kodovi);
                    document.Element("Kodovi").Element(element1Name).Add(new XElement("Unos", new XAttribute("id", cd.ID.ToString()),
                                                                                           new XAttribute("metoda", "automatsko"),
                                                                                           new XAttribute("Kod", element1Name),
                                                                                           new XAttribute("Vrednost", cd.DumpingPropertyCollection[0].DumpingValue.ToString("#.##")),
                                                                                           new XAttribute("Vreme", time)));
                    document.Save(path);
                }
            }
            if (CheckIfUpdatable(cd.DumpingPropertyCollection[1].Kodovi, cd.ID))
            {
                if (IsOutOfDeadband(cd.DumpingPropertyCollection[1]))
                {

                    document = XDocument.Load(path);
                    string element2Name = map.PokupiKodove(cd.DumpingPropertyCollection[1].Kodovi);
                    document.Element("Kodovi").Element(element2Name).Add(new XElement("Unos", new XAttribute("id", cd.ID.ToString()),
                                                                                                new XAttribute("metoda", "automatsko"),
                                                                                                new XAttribute("kod", element2Name),
                                                                                                new XAttribute("vrednost", cd.DumpingPropertyCollection[1].DumpingValue.ToString("#.##")),
                                                                                                new XAttribute("vreme", time)));

                    document.Save(path);
                }
            }



            return true;
        }
        public bool AutomaticDelete(CollectionDescription cd)//igor
        {
            if (cd == null)
                return false;

            string path = "DataSet" + cd.DataSet.ToString() + ".xml";
            Kodovi1 map = new Kodovi1();
            bool ret1 = true;
            bool ret2 = true;
            if (!File.Exists(path))
            {
                return false;
            }
            if (cd.DumpingPropertyCollection.Count != 2)
            {
                return false;
            }

            XDocument document = XDocument.Load(path);
            string name = map.PokupiKodove(cd.DumpingPropertyCollection[0].Kodovi);
            XElement element = document.Element("Kodovi").Element(name);
            XElement temp = null;
            foreach (XElement e in element.Elements())
            {
                if (Double.Parse(e.Attribute("vrednost").Value) == cd.DumpingPropertyCollection[0].DumpingValue)
                {
                    temp = e;
                    break;
                }
            }
            if (temp != null)
                temp.Remove();
            else
                ret1 = false;

            string name2 = map.PokupiKodove(cd.DumpingPropertyCollection[1].Kodovi);
            XElement element2 = document.Element("Kodovi").Element(name2);
            XElement temp2 = null;
            foreach (XElement e in element2.Elements())
            {
                if (Double.Parse(e.Attribute("vrednost").Value) == cd.DumpingPropertyCollection[1].DumpingValue)
                {
                    temp2 = e;
                    break;
                }
            }
            if (temp2 != null)
                temp2.Remove();
            else
                ret2 = false;

            return ret1 && ret2;
        }
        public bool CheckIfUpdatable(Kodovi code, int id)
        {
            if ((int)code > 10 || (int)code < 0)
                return false;

            string path = "DataSet" + ((int)code % 5 + 1) + ".xml";
            bool found = false;
            Kodovi1 map = new Kodovi1();
            if (!File.Exists(path))
            {
                return false;
            }

            XDocument document = XDocument.Load(path);
            string element1Name = map.PokupiKodove(code);

            foreach (XElement e in document.Element("Kodovi").Element(element1Name).Elements())
            {
                if (e.Attribute("metoda").Value == "automatsko")
                {
                    found = true;
                    break;
                }
            }
            bool unique = true;

            foreach (XElement e in document.Element("Kodovi").Element(element1Name).Elements())
            {
                if (Int32.Parse(e.Attribute("id").Value) == id)
                {
                    unique = false;
                    break;
                }
            }


            return found && unique;
        }

    }
}
