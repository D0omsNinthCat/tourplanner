using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tourplanner.Models
{
    public class Log
    {
        public int log_ID { get; set; }
        public DateTime log_Date { get; set; }
        public int log_Duration { get; set; }
        public double log_Distance { get; set; }
        public int log_rating { get; set; }
        public string log_Report { get; set; }
        public int tour_ID { get; set; }
    }
}
