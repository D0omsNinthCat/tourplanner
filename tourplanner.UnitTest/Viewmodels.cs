using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using tourplanner.Viewmodels;

namespace tourplanner.UnitTest
{
    [TestFixture]
    public class Viewmodels
    {
        main_Window_VM mainVM;

        [SetUp]
        public void Setup()
        {
            mainVM = new main_Window_VM();
            
        }
        [Test]
        public void TourListTest()
        {
            
            Assert.IsTrue(1 == 1);
        }
    }
}
