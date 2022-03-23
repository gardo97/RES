using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat.Klase
{
    public class Vrednost
    {
        private DateTime vreme;
        private int id;
        private float potrosnja;

        public Vrednost(DateTime Vreme, int ID, float p)
        {
            Vreme = vreme;
            ID = id;
            p = potrosnja;
        }

        public DateTime Vreme { get => vreme; set => vreme = value; }
        public int Id { get => id; set => id = value; }
        public float Potrosnja { get => potrosnja; set => potrosnja = value; }
    }
}
