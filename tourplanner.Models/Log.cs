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
        public string log_Name { get; set; }
        public string log_Date { get; set; }
        public int log_Duration { get; set; }
        public double log_Distance { get; set; }
        public int log_Rating { get; set; }
        public string log_Report { get; set; }
        public int tour_ID { get; set; }
        public string log_Author { get; set; }
        public double log_Speed { get; set; }
        public string log_Start { get; set; }
        public string log_End { get; set; }
        public TransportEnum log_transport { get; set; }

    }
}
