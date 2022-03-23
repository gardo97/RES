using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat.Klase
{
    public class DeltaCD
    {
        private string id;
        private List<CollectionDescription> dodaj;
        private List<CollectionDescription> izmeni;
        private List<CollectionDescription> brisanje;

        public List<CollectionDescription> Dodaj { get => dodaj; set => dodaj = value; }
        public List<CollectionDescription> Izmeni { get => izmeni; set => izmeni = value; }
        public List<CollectionDescription> Brisanje { get => brisanje; set => brisanje = value; }
        public string Id { get => id; set => id = value; }

        public DeltaCD(string id, List<CollectionDescription> dodaj, List<CollectionDescription>izmeni, List<CollectionDescription>brisanje)
        {
            Id = id;
            Dodaj = dodaj;
            Izmeni = izmeni;
            Brisanje = brisanje;
        }

        public DeltaCD()
        {
            dodaj = new List<CollectionDescription>();
            izmeni = new List<CollectionDescription>();
            brisanje = new List<CollectionDescription>();
        }
    }
}
