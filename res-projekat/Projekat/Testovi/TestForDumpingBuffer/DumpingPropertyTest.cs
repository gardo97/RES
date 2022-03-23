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
    public class DumpingPropertyTest
    {
        [Test]
        public void KonstrutorBezParamtara()
        {
            Assert.DoesNotThrow(() =>
            {
                DumpingProperty dp = new DumpingProperty();
                Assert.IsNotNull(dp);
            });
        }

        [Test]
        [TestCase(Kodovi.CODE_ANALOG, 220.2)]
        [TestCase(Kodovi.CODE_CUSTOM, 1)]
        [TestCase(Kodovi.CODE_LIMITSET, -1304.98)]
        public void KonstruktorSaParametrima(int k, double vrednost)
        {
            DumpingProperty dp;
            Assert.DoesNotThrow(() =>
            {
                dp = new DumpingProperty(k, vrednost);
                Assert.IsNotNull(dp);
                Assert.AreEqual(dp.Kodovi, (Kodovi)k);
                Assert.AreEqual(dp.DumpingValue, vrednost);
            });
        }
    }
}
