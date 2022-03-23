using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat.Klase
{
    public class HistoricalDescription
    {
        public int ID { get; set; }
        public List<HistoricalPropertz> HistoricalProperties { get; set; }
        public int DataSet { get; set; }

        public HistoricalDescription()
        {
            HistoricalProperties = new List<HistoricalPropertz>();
        }

        public HistoricalDescription(int iD, int dataSet)
        {
            ID = iD;
            DataSet = dataSet;
            HistoricalProperties = new List<HistoricalPropertz>();
        }
    }
}
