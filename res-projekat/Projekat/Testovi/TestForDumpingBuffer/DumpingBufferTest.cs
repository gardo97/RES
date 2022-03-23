using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RESProjekat.Komponente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovi.TestForDumpingBuffer
{
    [TestFixture]
    [TestClass]
    public class DumpingBufferTest
    {
        [Test]
        public void KonstruktorBezParametara()
        {
            NUnit.Framework.Assert.DoesNotThrow(() =>
            {
                DumpingBUffer db = new DumpingBUffer();
                NUnit.Framework.Assert.IsNotNull(db);
            });
        }

        [TestMethod]
        public void TestForWriteToHistory()
        {
            DumpingBUffer db = new DumpingBUffer();
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(1, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(2, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(3, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(4, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(5, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(6, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(7, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(8, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.WriteToHistory(9, 95));
            NUnit.Framework.Assert.AreEqual(false, DumpingBUffer.WriteToHistory(-1, 95));
            NUnit.Framework.Assert.AreEqual(false, DumpingBUffer.WriteToHistory(11, 95));
            NUnit.Framework.Assert.AreEqual(false, DumpingBUffer.WriteToHistory(1100, 95));
            NUnit.Framework.Assert.AreEqual(false, DumpingBUffer.WriteToHistory(1, 5000));
        }

        [TestMethod]
        public void TestForKolekcija()
        {
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(1, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(2, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(3, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(4, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(5, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(6, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(7, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(8, 95));
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.Kolekcija(9, 95));
            NUnit.Framework.Assert.AreEqual(false, DumpingBUffer.Kolekcija(-1, 95));
            NUnit.Framework.Assert.AreEqual(false, DumpingBUffer.Kolekcija(11, 95));
            NUnit.Framework.Assert.AreEqual(false, DumpingBUffer.Kolekcija(1100, 95));
            NUnit.Framework.Assert.AreEqual(false, DumpingBUffer.Kolekcija(1, 5000));
        }

        [TestMethod]
        public void TestForSlanjeUHistorical()
        {
            NUnit.Framework.Assert.AreEqual(true, DumpingBUffer.SlanjeUHistorical());
        }
    }
}
