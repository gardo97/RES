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
    public class CollectionDescriptionTest
    {
        [Test]
        public void KonstruktorBezParametara()
        {
            Assert.DoesNotThrow(() =>
            {
                CollectionDescription cd = new CollectionDescription();
                Assert.IsNotNull(cd);
                Assert.IsNotNull(cd.DumpingPropertyCollection);
            });
        }

        [Test]
        [TestCase(1, 200)]
        [TestCase(2, 250)]
        [TestCase(3, 300)]
        public void KontruktorSaParamtetrima(int iD, int dataSet)
        {
            CollectionDescription cd;
            Assert.DoesNotThrow(() =>
            {
                cd = new CollectionDescription(iD, dataSet);
                Assert.IsNotNull(cd);
                Assert.AreEqual(cd.ID, iD);
                Assert.AreEqual(cd.DataSet, dataSet);
            });
        }
    }
}
