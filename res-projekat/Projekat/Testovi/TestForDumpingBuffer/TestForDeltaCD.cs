using NUnit.Framework;
using RESProjekat.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovi.TestForDumpingBuffer
{
    [TestFixture]
    public class TestForDeltaCD
    {
        [Test]
        public void KonstruktorBezParametara()
        {
            Assert.DoesNotThrow(() =>
            {
                DeltaCD cd = new DeltaCD();
                Assert.IsNotNull(cd);
            });
        }

        [Test]
        public void KonstruktorSaParametrima(string id, List<CollectionDescription>dodaj, List<CollectionDescription>izmeni, List<CollectionDescription>brisanje)
        {
            DeltaCD cd;

            Assert.DoesNotThrow(() =>
            {
                cd = new DeltaCD(id, dodaj, izmeni, brisanje);
                Assert.IsNotNull(cd);
                Assert.AreEqual(cd.Dodaj, dodaj);
                Assert.AreEqual(cd.Izmeni, izmeni);
                Assert.AreEqual(cd.Brisanje, brisanje);
            });
        }
    }
}
