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

        public string tour_Name { get; set; }

        public tour_Details_VM(string selected_Tour_Name)
        {
            this.tour_Name = selected_Tour_Name;
        }
        
    }
}
