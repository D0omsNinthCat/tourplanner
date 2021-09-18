using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tourplanner.DALayer;

namespace tourplanner.UnitTests
{


    [TestClass]
    public class DB_Tests
    {
        private DAO dataAccessObject { get; set; }
        private static DB database_instance;
        private NpgsqlConnection connection;


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
