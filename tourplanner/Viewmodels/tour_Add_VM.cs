using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tourplanner.DALayer;
using tourplanner.Models;

namespace tourplanner.Viewmodels
{
    class tour_Add_VM : base_VM
    {
        private RelayCommand RelayCommand { get; set; }
        public Tour tour_New { get; set; }

        public ICommand _addFunc;
        private DAO dataAccessObject { get; set; }
        public tour_Add_VM(Tour t)
        {
            this.tour_New = t;
        }
        public ICommand AddFunc
        {
            get
            {
                if (_addFunc == null)
                {
                    _addFunc = new RelayCommand(param => this.AddTour(), param => this.CanExecute());
                }
                return _addFunc;
            }
        }
        public Tour Tour_New
        {
            get { return tour_New; }
            set
            {
                if ((value != null) && (tour_New != value))
                {
                    tour_New = value;
                    OnPropertyChanged(nameof(tour_New));
                }
            }
        }
        private bool CanExecute()
        {
            //ADD CONDITIONS
            return true;
        }
        private void AddTour()
        {
            dataAccessObject = new DAO();
            dataAccessObject.AddTour(tour_New);
            SelectedViewModel = new main_Window_VM();
        }
    }
}
