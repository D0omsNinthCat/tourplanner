using NUnit.Framework;
using System.Configuration;

namespace tourplanner.UnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        private string connectionstring = "Host = localhost; Username=postgres;Password=1234;Database=tourplanner";
        private string Connectionstring = ConfigurationManager.AppSettings.Get("connectionstring");

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(connectionstring, Connectionstring);
        }
        [Test]
        public void TestMethod2()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
