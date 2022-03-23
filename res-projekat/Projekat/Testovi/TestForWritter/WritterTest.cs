using NUnit.Framework;
using RESProjekat.Komponente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testovi.TestForWritter
{
    [TestFixture]
    [TestClass]
    public class WritterTest
    {
        [Test]
        public void KostruktorBezParametara()
        {
            NUnit.Framework.Assert.DoesNotThrow(() =>
            {
                Writer w = new Writer();
                NUnit.Framework.Assert.IsNotNull(w);
            });
        }

        [TestMethod]
        public void TestMethodUpisiUFajl()
        {
            Writer w = new Writer();
            NUnit.Framework.Assert.IsNotNull(new Writer());
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(1, 123));
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(2, 123));
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(3, 123));
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(4, 123));
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(5, 123));
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(6, 123));
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(7, 123));
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(8, 123));
            NUnit.Framework.Assert.AreEqual(true, w.UpisUFajl(9, 123));
            NUnit.Framework.Assert.AreEqual(false, w.UpisUFajl(13, 123));
            NUnit.Framework.Assert.AreEqual(false, w.UpisUFajl(-1, 123));
        }

        [TestMethod]
        public void TestForWriteToDumpingBUffer()
        {
            //Writer w = new Writer();
            //NUnit.Framework.Assert.AreEqual(,w.WriteToDumpingBuffer());
        }

        [TestMethod]
        public void TestForManualWriteToHistory()
        {
           //Writer w = new Writer();
            //NUnit.Framework.Assert.AreEqual(, w.ManualWriteToHistory());
        }
    }
}
