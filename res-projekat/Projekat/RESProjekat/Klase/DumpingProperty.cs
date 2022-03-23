using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat.Klase
{
    public class DumpingProperty
    {
        private Kodovi kodovi;
        //private List<kodovi> listaKodova;
        private double dumpingValue;

        public double DumpingValue { get => dumpingValue; set => this.dumpingValue = value; }
        public Kodovi Kodovi { get => kodovi; set => kodovi = value; }

        public DumpingProperty() { }
        public DumpingProperty(int k, double v)
        {
            Kodovi = (Kodovi)k;
            DumpingValue = v;
        }
        //public List<kodovi> ListaKodova { get => listaKodova; set => listaKodova = value; }
    }
}
