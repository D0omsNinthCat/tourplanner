using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;


namespace tourplanner.DALayer
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
        public void DeleteTour(Tour t)
        {
            dataAccessObject.DeleteTour(t);
        }
        public void EditTour(Tour t)
        {
            dataAccessObject.EditTour(t);
        }
        public void AddTour(Tour t)
        {
            dataAccessObject.AddTour(t);
        }
        public void AddLog(Log l)
        {
            dataAccessObject.AddLog(l);
        }
        public void EditLog(Log l)
        {
            dataAccessObject.EditLog(l);
        }
        public void DeleteLog(Log l)
        {
            dataAccessObject.DeleteLog(l);
        }
        public async Task<Tour> GetAPI(Tour t)
        {
            t = await dataAccessObject.GetAPI(t);
            return t;
        }
    }
}
