﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;
using tourplanner.Readers;

namespace tourplanner.Viewmodels
{
    class main_Window_VM : base_VM
    {
        public main_Window_VM()
        {
            object_Reader_Tour rdr = new object_Reader_Tour();
            tour_List = rdr.Execute();
            //foreach (Tour t in tour_List)
            //{
            //    tour_Names.Add(t.tour_Name);
            //}
            if (tour_List.Count > 0)
            {
                selected_Tour = tour_List[0];
            }
        }
        public Tour selected_Tour;
        public string selected_Tour_Name { get; set; }
        public Collection<Tour> tour_List { get; set; }
        public ObservableCollection<string> tour_Names { get; } = new ObservableCollection<string>();  
    }
}
