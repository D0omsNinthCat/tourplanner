using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.ObjectModel;
using tourplanner.Models;
using Microsoft.Win32;

namespace tourplanner.DALayer
{
    public class Filesystem
    {
        private string JsonPath = ConfigurationManager.AppSettings.Get("jsonpath");
        

        public void ExportTour(Tour t)
        {

            var jsonfile = JsonConvert.SerializeObject(new { tour_Name = t.tour_Name, tour_From = t.tour_From, tour_To = t.tour_To, tour_Description = t.tour_Description, tour_Distance = t.tour_Distance, tour_Map = t.tour_Map });
            File.WriteAllText(JsonPath + t.tour_ID + ".json", jsonfile);
        }
        //public Tour ImportTour()
        //{
        //    var filedialogservice =
        //    openfiledialog(jsonpath);

        //    return;
        //}
        
        public Tour OpenFile()
        {
            var dialog = new OpenFileDialog();
            Tour t = new Tour();
            dialog.InitialDirectory = JsonPath;
            dialog.Filter = "All Json objects|*.json";
            dialog.ShowDialog();

            using(StreamReader r = new StreamReader(dialog.FileName))
            {
                string json = r.ReadToEnd();
                t = JsonConvert.DeserializeObject<Tour>(json);
            }
            return t;
        }
    }
}
