using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner.DAL
{
    public interface DB_Interface
    {
        public List<Tour> GetTourList();
    }
}
