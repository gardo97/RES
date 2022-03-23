using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESProjekat.Komponente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovi.TestForLogger
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception), "Null argument passed.")]
        public void TestMethod1()
        {
            Logger loger = new Logger();
            loger.UpisLogger(null, "fsadf");
            loger.UpisLogger("fsadf", null);
            loger.UpisLogger(null, null);
        }
        [TestMethod]

        public void TestMethod2()
        {
            Logger loger = Logger.Instanca();
            try
            {
                loger.UpisLogger("asfdas", "fasdf");
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception but got one");
            }




        }
    }
}
