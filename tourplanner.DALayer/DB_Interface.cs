using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner.DALayer
{
    public interface DB_Interface
    {
        public List<Tour> GetTourList();
        public void DeleteTour(Tour t);
        public void EditTour(Tour t);
        public void AddTour(Tour t);
        public Task<Tour> GetAPI(Tour t);
        public List<Tour> GetLogs(List<Tour> tours);


    }
}
