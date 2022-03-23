using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESProjekat.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovi
{
    [TestClass]
    public class TestClasses
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(new Kodovi1());
            Assert.IsNotNull(new CollectionDescription());
            Assert.IsNotNull(new CollectionDescription(12, 2));
            Assert.IsNotNull(new CollectionDescription(13, 4, new List<DumpingProperty>()));
            Assert.IsNotNull(new DeltaCD());
            Assert.IsNotNull(new DeltaCD("asgafgsdfgrewqgewrger", new List<CollectionDescription>(), new List<CollectionDescription>(), new List<CollectionDescription>()));
            Assert.IsNotNull(new DumpingProperty());
            Assert.IsNotNull(new DumpingProperty(3, 4324));
            Assert.IsNotNull(new HistoricalDescription());
            Assert.IsNotNull(new HistoricalDescription(31, 2));
            Assert.IsNotNull(new HistoricalPropertz());
            Assert.IsNotNull(new HistoricalPropertz(Kodovi.CODE_ANALOG, 143));
        }
    }
}
