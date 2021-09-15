using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner.DAL
{
    public class DAO
    {
        private DB_Interface dataAccessObject;

        public DAO()
        {
            dataAccessObject = DB.Database_instance();
        }
        public List<Tour> GetTourList()
        {
            return dataAccessObject.GetTourList();
        }
    }
}
