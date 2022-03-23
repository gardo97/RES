using RESProjekat.Klase;
using Microsoft.VisualStudio.TestTools.UITesting;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testovi
{
    [TestClass]
    public class TestZaKodove1
    {
        [TestMethod]
        public void TestMetoda1()
        {
            string imeKoda0 = "CODE_ANALOG";
            string imeKoda1 = "CODE_DIGITAL";
            string imeKoda2 = "CODE_CUSTOM";
            string imeKoda3 = "CODE_LIMITSET";
            string imeKoda4 = "CODE_SINGLENODE";
            string imeKoda5 = "CODE_MULTIPLENODE";
            string imeKoda6 = "CODE_CONSUMER";
            string imeKoda7 = "CODE_SOURCE";
            string imeKoda8 = "CODE_MOTION";
            string imeKoda9 = "CODE_SENSOR";

            Kodovi1 kodovi = new Kodovi1();

            NUnit.Framework.Assert.AreEqual(imeKoda0, kodovi.PokupiKodove(Kodovi.CODE_ANALOG));
            NUnit.Framework.Assert.AreEqual(imeKoda1, kodovi.PokupiKodove((Kodovi)1));
            NUnit.Framework.Assert.AreEqual(imeKoda2, kodovi.PokupiKodove((Kodovi)2));
            NUnit.Framework.Assert.AreEqual(imeKoda3, kodovi.PokupiKodove((Kodovi)3));
            NUnit.Framework.Assert.AreEqual(imeKoda4, kodovi.PokupiKodove((Kodovi)4));
            NUnit.Framework.Assert.AreEqual(imeKoda5, kodovi.PokupiKodove((Kodovi)5));
            NUnit.Framework.Assert.AreEqual(imeKoda6, kodovi.PokupiKodove((Kodovi)6));
            NUnit.Framework.Assert.AreEqual(imeKoda7, kodovi.PokupiKodove((Kodovi)7));
            NUnit.Framework.Assert.AreEqual(imeKoda8, kodovi.PokupiKodove((Kodovi)8));
            NUnit.Framework.Assert.AreEqual(imeKoda9, kodovi.PokupiKodove((Kodovi)9));
            NUnit.Framework.Assert.AreEqual(null, kodovi.PokupiKodove((Kodovi)11));
        }
    }
}
