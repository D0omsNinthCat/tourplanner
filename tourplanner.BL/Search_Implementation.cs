using System;
using System.Collections.ObjectModel;
using tourplanner.Models;

namespace tourplanner.BL
{
    public class Search_Implementation : IFactory
    {
        public ObservableCollection<Tour> search(string search_Term, ObservableCollection<Tour> tours)
        {

            return tours;
        }
    }
}
