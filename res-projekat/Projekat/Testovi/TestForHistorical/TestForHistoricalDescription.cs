using NUnit.Framework;
using RESProjekat.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovi.TestForHistorical
{
    [TestFixture]
    public class TestForHistoricalDescription
    {
        [Test]
        public void KonstruktorBezParametara()
        {
            Assert.DoesNotThrow(() =>
            {
                HistoricalDescription hd = new HistoricalDescription();
                Assert.IsNotNull(hd);
            });
        }

        [Test]
        [TestCase(1, 200)]
        [TestCase(2, 250)]
        [TestCase(3, 300)]
        public void KonstruktorSaParametrima(int id, int dataSet)
        {
            HistoricalDescription hd;
            Assert.DoesNotThrow(() =>
            {
                hd = new HistoricalDescription(id, dataSet);
                Assert.IsNotNull(hd);
                Assert.AreEqual(hd.ID, id);
                Assert.AreEqual(hd.DataSet, dataSet);
            });
        }
    }
}
