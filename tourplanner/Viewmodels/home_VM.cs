using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using tourplanner.Models;

namespace tourplanner.Viewmodels
{
    class home_VM : base_VM
    {
        public Tour selected_Tour { get; set; }
        public Uri imageUri { get; set; }
        public BitmapImage imageBitmap { get; set; }
        public home_VM(Tour selected_Tour)
        {
            this.selected_Tour = selected_Tour;
            //DISCLAIMER: THIS SHOULD BE HANDLED BY DB.cs and was moved here because I couldn't get rid of the error:
            //CS7069: Reference to type 'Freezable' claims it is defined in 'WindowsBase', but it could not be found.
            //This prevented me from saving them images directly onto the objects
            if (selected_Tour.tour_Map != "")
            {
                imageUri = new Uri(selected_Tour.tour_Map, UriKind.Relative);
                imageBitmap = new BitmapImage();
                imageBitmap.BeginInit();
                imageBitmap.CacheOption = BitmapCacheOption.OnLoad;
                imageBitmap.UriSource = imageUri;
                imageBitmap.EndInit();
            }
        }
    }
}
