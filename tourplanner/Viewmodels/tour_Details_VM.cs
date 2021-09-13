using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner.Viewmodels
{
    class tour_Details_VM : base_VM
    {
        public string tour_Name;
        public string tour_Description;
        public string tour_Distance;
        public Tour selected_Tour { get; set; }
        public string selected_Tour_Name { get; set; }
        public tour_Details_VM(Tour selected_Tour)
        {
            this.selected_Tour = selected_Tour;
            this.selected_Tour_Name = selected_Tour.tour_Name;
        }
    }
}
