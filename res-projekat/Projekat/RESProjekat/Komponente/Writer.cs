using RESProjekat.Klase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RESProjekat.Komponente
{
    public class Writer
    {

        public Writer()
        {

        }
        async public void WriteToDumpingBuffer()//luka
        {
            int kod;
            double vrednost;

            while(true)
            {
                Random e = new Random();
                kod = e.Next(10);
                vrednost = e.Next(100, 10000) / 100;
                DumpingBUffer.WriteToHistory(kod, vrednost);
                Logger.Instanca().UpisLogger("Writer", string.Format("Upisivanje u dumping buffer sa kodom {0}, vrednost koda {1}", kod, vrednost));
                await Task.Delay(2000);
            }
        }


        public bool ManualWriteToHistory() //ovo treba direktno da salje na historical komponentu//igor
        {
            double vrednost = 0;
            Console.WriteLine("Upisisvanje manuelno u Historical komponentu");
            Console.WriteLine("Izaberite kod: ");
            Console.WriteLine("1. CODE_ANALOG ");
            Console.WriteLine("2. CODE_DIGITAL ");
            Console.WriteLine("3. CODE_CUSTOM ");
            Console.WriteLine("4. CODE_LIMITSET ");
            Console.WriteLine("5. CODE_SINGLENODE");
            Console.WriteLine("6. CODE_MULTIPLENODE ");
            Console.WriteLine("7. CODE_CONSUMER ");
            Console.WriteLine("8. CODE_SOURCE ");
            Console.WriteLine("9. CODE_MOTION ");
            Console.WriteLine("10. CODE_SENSOR ");

            int kod;
            try
            {
                kod = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Logger.Instanca().UpisLogger("Writer", "greska tokom parsiranja");
                throw new Exception("Ne moze da se parsira uneta vrednost");
            }
            Console.WriteLine("Unesite vrednost: ");
            vrednost = double.Parse(Console.ReadLine());

            return UpisUFajl(kod, vrednost);
        }

        public bool UpisUFajl(int kod, double vrednost)//igor
        {
            if (kod > 9 || kod < 0)
            {
                Logger.Instanca().UpisLogger("Writer", "Manuelno upisivanje u Historical komponentu je prekinuto| Greska u unetom kodu");
                return false;
            }
            Historical.Instanca().ManualWriting((Kodovi)(kod), vrednost);
            Logger.Instanca().UpisLogger("Writer", "Manuelno upisivanje zavrseno");
            return true;
        }

    }
}
