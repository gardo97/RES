using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESProjekat.Komponente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovi
{
    [TestClass]
    public class TestKomponente
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(new DumpingBUffer());
            Assert.IsNotNull(new Historical());
            Assert.IsNotNull(new Logger());
            Assert.IsNotNull(new Reader());
            Assert.IsNotNull(new Writer());
        }
    }
}
