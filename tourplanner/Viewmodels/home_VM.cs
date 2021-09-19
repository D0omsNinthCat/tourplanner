using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using tourplanner.DALayer;
using tourplanner.Models;

namespace tourplanner.Viewmodels
{
    class home_VM : base_VM
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Tour selected_Tour { get; set; }
        public Uri imageUri { get; set; }
        public BitmapImage imageBitmap { get; set; }
        public Filesystem filesystem { get; set; }
        public RelayCommand RelayCommand { get; set; }
        public ICommand _exportFunc;
        public home_VM(Tour selected_Tour)
        {
            this.selected_Tour = selected_Tour;
            //DISCLAIMER: THIS SHOULD BE HANDLED BY DB.cs and was moved here because I couldn't get rid of the error:
            //CS7069: Reference to type 'Freezable' claims it is defined in 'WindowsBase', but it could not be found.
            //This prevented me from saving them images directly onto the objects
            if (selected_Tour.tour_Map != "")
            {
                log.Info("trying to create BitmapImage");
                imageUri = new Uri(selected_Tour.tour_Map, UriKind.Relative);
                imageBitmap = new BitmapImage();
                imageBitmap.BeginInit();
                imageBitmap.CacheOption = BitmapCacheOption.OnLoad;
                imageBitmap.UriSource = imageUri;
                imageBitmap.EndInit(); //Can't save this to Tours object :(
            }
        }
        private bool CanExecute()
        {
            //ADD CONDITIONS
            return true;
        }
        public ICommand ExportFunc
        {
            get
            {
                if (_exportFunc == null)
                {
                    _exportFunc = new RelayCommand(param => this.Export(), param => this.CanExecute());
                }
                return _exportFunc;
            }
        }
        private void Export()
        {
            filesystem = new Filesystem();
            filesystem.ExportTour(this.selected_Tour);
        }


    }
}
