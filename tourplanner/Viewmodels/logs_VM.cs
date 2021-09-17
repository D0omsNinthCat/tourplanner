using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;

namespace tourplanner.Viewmodels
{
    class logs_VM : base_VM
    {
        public Tour selected_Tour { get; set; }
        public Log selected_Log { get; set; }

        public logs_VM(Tour selected_Tour){
            this.selected_Tour = selected_Tour;
        }

        public Log Selected_Log
        {
            get { return selected_Log; }
            set
            {
                if ((value != null) && (selected_Log != value))
                {
                    selected_Log = value;
                    OnPropertyChanged(nameof(selected_Log));
                }
            }
        }

    }
}
