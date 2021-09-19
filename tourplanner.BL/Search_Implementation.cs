using System;
using System.Collections.ObjectModel;
using tourplanner.Models;

namespace tourplanner.BL
{
    public class Search_Implementation : IFactory
    {
        public ObservableCollection<Tour> search(string search_Term, ObservableCollection<Tour> tours)
        {
            if(search_Term != null|| search_Term != "")
            {
                ObservableCollection<Tour> search_Result = new ObservableCollection<Tour>();
                search_Term = search_Term.ToLower();
                foreach(Tour t in tours)
                {
                    if(t.tour_Name.ToLower().Contains(search_Term)|| t.tour_ID.ToString().Contains(search_Term)|| t.tour_From.ToLower().Contains(search_Term) || t.tour_To.ToLower().Contains(search_Term) || t.tour_Description.ToLower().Contains(search_Term))
                    {
                        search_Result.Add(t);
                    }
                    else
                    {
                        foreach(Log l in t.Logs)
                        {
                            if (l.log_Author.ToLower().Contains(search_Term) || l.log_Date.ToString().Contains(search_Term) || l.log_Name.ToLower().Contains(search_Term) || l.log_Transport.ToLower().Contains(search_Term) || l.log_Report.ToLower().Contains(search_Term)){
                                search_Result.Add(t);
                            }
                        }
                    }
                }
                return search_Result;
            }
            else
            {
                return tours;
            }
        }
    }
}
