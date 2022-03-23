using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat.Klase
{
    public class HistoricalPropertz
    {
        private Kodovi kodovi;
        private int historicalValue;

        public Kodovi Kodovi { get => kodovi; set => kodovi = value; }
        public int HistoricalValue1 { get => historicalValue; set => historicalValue = value; }

        public HistoricalPropertz() { }
        public HistoricalPropertz(Kodovi k, int v)
        {
            k = kodovi;
            v = historicalValue;
        }
    }
}
