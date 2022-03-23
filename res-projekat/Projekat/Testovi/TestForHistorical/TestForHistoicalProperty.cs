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
    public class TestForHistoicalProperty
    {
        [Test]
        public void KonsturktorBezParametara()
        {
            Assert.DoesNotThrow(() =>
            {
                HistoricalPropertz db = new HistoricalPropertz();
                Assert.IsNotNull(db);
            });
        }

        [Test]
        [TestCase(Kodovi.CODE_CONSUMER, 234.22)]
        [TestCase(Kodovi.CODE_DIGITAL, -5234.22)]
        [TestCase(Kodovi.CODE_LIMITSET, 0.2)]
        public void KonstruktorSaParametrima(Kodovi code, int value)
        {
            Assert.DoesNotThrow(() =>
            {
                DateTime timestamp = DateTime.Now;
                HistoricalPropertz hs = new HistoricalPropertz(code, value);
                Assert.IsNotNull(hs);
                Assert.AreEqual(code, hs.Kodovi);
                Assert.AreEqual(value, hs.HistoricalValue1);
            });
        }
    }
}
