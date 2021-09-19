using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner.BL
{
    public interface IFactory
    {
        ObservableCollection<Tour> search(string search_Term, ObservableCollection<Tour> tours);
    }
}
