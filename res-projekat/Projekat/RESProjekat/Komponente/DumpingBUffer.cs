using RESProjekat.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat.Komponente
{
    public class DumpingBUffer
    {
        private static int CDID = 0;
        private static Dictionary<int, CollectionDescription> CDList = new Dictionary<int, CollectionDescription>();
        private static Dictionary<int, CollectionDescription> CDListKolekcija = new Dictionary<int, CollectionDescription>();
        private static int[] counters = { 0, 0, 0, 0, 0 };
        private static Dictionary<int, bool> flags = new Dictionary<int, bool>();
        private static DeltaCD DCD = new DeltaCD();
        private static DumpingBUffer instance = null;
        private static readonly object padlock = new object();
        private static int recieveCounter = 0;

        public DumpingBUffer() { }
        static DumpingBUffer()
        {
            flags = new Dictionary<int, bool>() { { 1, false }, { 2, false }, { 3, false }, { 4, false }, { 5, false } };
            CDList = new Dictionary<int, CollectionDescription>() { { 1, null }, { 2, null }, { 3, null }, { 4, null }, { 5, null } };
            CDListKolekcija = new Dictionary<int, CollectionDescription>() { { 1, null }, { 2, null }, { 3, null }, { 4, null }, { 5, null } };
        }

        public static DumpingBUffer Instance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DumpingBUffer();
                }
                return instance;
            }
        }

        public static bool WriteToHistory(int kod, double vrednost)//igor
        {
            bool retval = true;
            if (kod < 1 || kod > 10)
            {
                return false;
            }
            if (vrednost > 1000 || vrednost < 0)
            {
                return false;
            }
            int dataSet = kod % 5 + 1;
            int index = kod / 5;
            int index2 = 0;
            if (index == 0)
                index2 = 1;


            if (CDList[dataSet] == null)
            {
                //treba napraviti novi collection description zato sto je prvi put stigla vrednost za taj CD
                CDID++;
                Guid g = new Guid();
                CDList[dataSet] = new CollectionDescription(Guid.NewGuid().GetHashCode(), dataSet);
                Logger.Instanca().UpisLogger("DumpingBuffer", string.Format("Novi CollectionDescription({0}) je dodat.", dataSet));
                CDList[dataSet].DumpingPropertyCollection.Add(null);
                CDList[dataSet].DumpingPropertyCollection.Add(null);
            }

            if (CDList[dataSet].DumpingPropertyCollection[index] != null)
            {
                if (CDList[dataSet].DumpingPropertyCollection[index].Kodovi == (Kodovi)kod)
                {
                    if (CDList[dataSet].DumpingPropertyCollection[index2] == null)
                    {
                        CDList[dataSet].DumpingPropertyCollection[index].DumpingValue = vrednost;   //updatuj vrednost ako je stigla nova vrednost a nije popunjen ceo collection
                        Logger.Instanca().UpisLogger("DumpingBuffer", string.Format("Izmena {0} na novu vrednost({1})", kod, vrednost));
                        retval = true;
                    }
                    else
                    {

                        retval = Kolekcija(kod, vrednost);
                    }
                }
            }
            else //null je, prvi put je dodat pa ga ubacujemo u cd
            {
                CDList[dataSet].DumpingPropertyCollection[index] = new DumpingProperty(kod, vrednost);
                Logger.Instanca().UpisLogger("DumpingBuffer", string.Format("Nova vrednost dodata u CollectionDescription({0})", kod));
                counters[CDList[dataSet].DataSet - 1]++;
                retval = true;
            }


            recieveCounter++;
            if (recieveCounter % 10 == 0)
            {
                retval = SlanjeUHistorical();
            }
            Logger.Instanca().UpisLogger("DumpingBuffer", "Upisivanje u Historical komponentu je zavrseno");
            return retval;
        }

        public static bool Kolekcija(int kod, double vrednost) //luka
        {
            if (kod < 1 || kod > 10)
            {
                Logger.Instanca().UpisLogger("DumpingBuffer", "Vrednost nije validna");
                return false;
            }
            if (vrednost > 1000 || vrednost < 0)
            {
                Logger.Instanca().UpisLogger("DumpingBuffer", "Vrednost nije validna");
                return false;
            }

            bool retVal = true;
            int dataSet = kod % 5 + 1;
            //ako se nalazi vec u kolekciji i treba update-ovati njegovu vrednost
            if (CDListKolekcija[dataSet] == null)
            {
                //treba napraviti novi collection description zato sto je prvi put stigla vrednost za taj CD
                CDID++;
                CDListKolekcija[dataSet] = new CollectionDescription(CDID, dataSet);
                CDListKolekcija[dataSet].DumpingPropertyCollection.Add(null);
                CDListKolekcija[dataSet].DumpingPropertyCollection.Add(null);
                Logger.Instanca().UpisLogger("DumpingBuffer", string.Format("Novi CollectionDescription({0}) dodat u kolekciju", dataSet));
            }

            if (CDListKolekcija[dataSet].DumpingPropertyCollection[0] != null)
            {
                if (CDListKolekcija[dataSet].DumpingPropertyCollection[0].Kodovi == (Kodovi)kod)
                {
                    if (CDListKolekcija[dataSet].DumpingPropertyCollection[1] == null)
                    {
                        CDListKolekcija[dataSet].DumpingPropertyCollection[0].DumpingValue = vrednost;   //updatuj vrednost ako je stigla nova vrednost a nije popunjen ceo collection
                        Logger.Instanca().UpisLogger("DumpingBuffer", string.Format("Izmena {0} u novu vrednost ({1}) ", kod, vrednost));
                        retVal = true;
                    }
                }
            }
            else //null je, prvi put je dodat pa ga ubacujemo u cd
            {
                CDListKolekcija[dataSet].DumpingPropertyCollection[0] = new DumpingProperty(kod, vrednost);
                Logger.Instanca().UpisLogger("DumpingBuffer", string.Format("Dodata nova vrednost u CollectionDescription({0}) u kolekciji", kod));
                retVal = true;
            }

            if (CDListKolekcija[dataSet].DumpingPropertyCollection[1] != null)
            {
                if (CDListKolekcija[dataSet].DumpingPropertyCollection[1].Kodovi == (Kodovi)kod)
                {
                    if (CDListKolekcija[dataSet].DumpingPropertyCollection[0] == null)
                    {
                        CDListKolekcija[dataSet].DumpingPropertyCollection[1].DumpingValue = vrednost;
                        Logger.Instanca().UpisLogger("DumpingBuffer", string.Format("Izmena {0} u novu vrednost({1}) u kolekciji", kod, vrednost));
                        retVal = true;
                    }
                }
            }
            else
            {
                Logger.Instanca().UpisLogger("DumpingBuffer", string.Format("Dodata nova vrednost u CollectionDescription({0}) u kolekciji", kod));
                CDListKolekcija[dataSet].DumpingPropertyCollection[1] = new DumpingProperty(kod, vrednost);
                retVal = true;
            }


            return retVal;
        }

        public static bool SlanjeUHistorical()//igor
        {
            DeltaCD deltaCD = new DeltaCD();
            deltaCD.Id = Guid.NewGuid().ToString();
            foreach (CollectionDescription cd in CDList.Values)
            {
                if (cd != null)
                {
                    if (flags[cd.DataSet] == false)
                    {
                        if (counters[cd.DataSet - 1] == 2)
                        {
                            deltaCD.Dodaj.Add(cd);
                            flags[cd.DataSet] = true;
                        }
                    }
                    else
                    {
                        if (counters[cd.DataSet - 1] == 2)
                        {
                            deltaCD.Izmeni.Add(cd);
                        }
                    }
                }
            }

            //pozvati metodu iz historicala da upise ovo u fajl
            //Historical.Instanca().WriteToXML(deltaCD);
            //preuzima one sto smo odlozili i sad opet nastavlja sa radom
            CDList = null;
            for (int i = 0; i < 5; i++)
            {
                counters[i] = 0;
            }
            foreach (CollectionDescription cd in CDListKolekcija.Values)
            {
                if (cd != null)
                    if (cd.DumpingPropertyCollection != null)
                        counters[cd.DataSet - 1] = cd.DumpingPropertyCollection.Count();
            }
            CDList = null;
            CDList = CDListKolekcija;
            CDListKolekcija = new Dictionary<int, CollectionDescription>() { { 1, null }, { 2, null }, { 3, null }, { 4, null }, { 5, null } };
            return true;
        }
    }
}
