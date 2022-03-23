using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat.Klase
{
    public class Kodovi1
    {
        private Dictionary<int, string> imenaKodova;
        public Kodovi1()
        {
            imenaKodova = new Dictionary<int, string>();
            imenaKodova[0] = "CODE_ANALOG";
            imenaKodova[1] = "CODE_DIGITAL";
            imenaKodova[2] = "CODE_CUSTOM";
            imenaKodova[3] = "CODE_LIMITSET";
            imenaKodova[4] = "CODE_SINGLENODE";
            imenaKodova[5] = "CODE_MULTIPLENODE";
            imenaKodova[6] = "CODE_CONSUMER";
            imenaKodova[7] = "CODE_SOURCE";
            imenaKodova[8] = "CODE_MOTION";
            imenaKodova[9] = "CODE_SENSOR";
        }

        public string PokupiKodove(Kodovi kod)
        {
            if(!imenaKodova.Keys.Contains((int)kod))
            {
                return null;
            }
            return imenaKodova[(int)kod];
        }
    }
}
