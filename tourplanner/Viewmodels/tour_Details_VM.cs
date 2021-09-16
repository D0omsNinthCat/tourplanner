using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;
using tourplanner.DALayer;
using System.Windows.Input;

namespace tourplanner.Viewmodels
{
    class tour_Details_VM : base_VM
    {
        public Tour selected_Tour { get; set; } //Zwischenspeichern falls verändert aber nicht Edited wird

        public ICommand _editFunc;
        public ICommand _copyFunc;
        private DAO dataAccessObject { get; set; }
        private RelayCommand RelayCommand { get; set; }
        public tour_Details_VM(Tour selected_Tour)
        {
            this.selected_Tour = selected_Tour;
        }
        public ICommand EditFunc
        {
            get
            {
                if(_editFunc == null)
                {
                    _editFunc = new RelayCommand(param => this.EditTour(), param => this.CanExecute());
                }
                return _editFunc;
            }
        }
        public ICommand CopyFunc
        {
            get
            {
                if (_copyFunc == null)
                {
                    _copyFunc = new RelayCommand(param => this.CopyTour(), param => this.CanExecute());
                }
                return _copyFunc;
            }
        }

        private bool CanExecute()
        {
            //ADD CONDITIONS
            return true;
        }
        private void EditTour()
        {
            dataAccessObject = new DAO();
            dataAccessObject.EditTour(selected_Tour);
            //Not sure what this is doing. How do I change back to "no User Control"?
            SelectedViewModel = new main_Window_VM(); 
        }
        private void CopyTour()
        {
            dataAccessObject = new DAO();
            dataAccessObject.AddTour(selected_Tour);
            //Not sure what this is doing. How do I change back to "no User Control"?
            SelectedViewModel = new main_Window_VM();
        }
    }
}
