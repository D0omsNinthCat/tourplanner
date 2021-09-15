using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner.Viewmodels
{
    class tour_Details_VM : base_VM
    {
        public Tour selected_Tour { get; set; }
        public tour_Details_VM(Tour selected_Tour)
        {
            this.selected_Tour = selected_Tour;
        }
    }
}
