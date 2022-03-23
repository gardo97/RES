using RESProjekat.Klase;
using RESProjekat.Komponente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat
{
    class Program
    {
        private static Writer writer = new Writer();
        private static Task upisBafer = null;
        private static Logger logger = new Logger();
        static void Main(string[] args)
        {
            Meni();
            Console.WriteLine("Pritisnite enter za izlazak iz programa");
            Console.ReadLine();
        }

        static void Meni()
        {
            int i = 0;
            do
            {

                Console.WriteLine("Molimo vas izaberite neku od opcija");
                Console.WriteLine("1. Opcija za manuelno upisivanje u bafer");
                Console.WriteLine("2. Opcija za automatsko upisivanje u bafer");
                Console.WriteLine("3. Opcija za Ispis vrednosti iz XML fajla po vremeskom intervalu");
                Console.WriteLine("4. Izlaz");

                try
                {
                    i = Int32.Parse(Console.ReadLine());
                }
                catch(Exception e)
                {
                    throw new Exception("Unos nije validan, pokusajte ponovo");
                }
                if(i > 4 || i < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Unos je pogresan, molimo vas pokusajte ponovo. Pritisnite enter za nazad");
                    Console.ReadLine();
                    continue;
                }
                IzborKomandi(i);

            } while (i != 4);
        }
        static void IzborKomandi(int m)
        {
            if (m == 1)
            {
                Console.Clear();
                try
                {
                    writer.ManualWriteToHistory();
                    Logger.Instanca().UpisLogger("Program", "Izabrano ManualWriteToHistory");
                }
                catch (Exception e)
                {
                    Logger.Instanca().UpisLogger("Program", "Greska u ManualWriteToHistory" + e.Message);
                }
                Logger.Instanca().UpisLogger("Program", "Zavrseno ManualWriteToHistory");

            }
            else if (m == 2)
            {
                /*if (upisBafer != null && upisBafer.Status == TaskStatus.RanToCompletion)
                {
                    /*Console.Clear();
                    Console.WriteLine("Automatko upisivanje je prekinuto");
                    Console.WriteLine("Kliknite enter za nazad");
                    Console.ReadLine();
                    return;*/
                Console.Clear();
                upisBafer = new Task(() => writer.WriteToDumpingBuffer());
                upisBafer.Start();
                Logger.Instanca().UpisLogger("Program", "Izabrano WriteToDumpingBuffer");
                Console.WriteLine("Pokrenuto je upisivanje u dumping buffer");
                Console.WriteLine("Kliknite enter za nazad");
                Console.ReadLine();
                //}
                /*else
                {
                    Console.Clear();
                    upisBafer = new Task(() => writer.WriteToDumpingBuffer());
                    upisBafer.Start();
                    Logger.Instanca().UpisLogger("Program", "Izabrano WriteToDumpingBuffer");
                    Console.WriteLine("Pokrenuto je upisivanje u dumping buffer");
                    Console.WriteLine("Kliknite enter za nazad");
                    Console.ReadLine();
                }*/
            }
            else if (m == 3)
            {
                Console.WriteLine("Unesite kod za koji zelite da procitate vrednosti");
                Kodovi kodovi = (Kodovi)(Int32.Parse(Console.ReadLine()) - 1);
                Console.WriteLine("Unesite pocetno vreme: ");
                DateTime startInterval = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Unesite vreme zavrsetka: ");
                DateTime endInterval = DateTime.Parse(Console.ReadLine());

                //upis u reader
            }
        }
    }
}
