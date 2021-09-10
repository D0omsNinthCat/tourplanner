using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tourplanner.Models
{
    public class Tour
    {
        public int tour_ID { get; set; }
        public string tour_Name { get; set; }
        public string tour_Description { get; set; }
        public float tour_Distance { get; set; }
    }
}
