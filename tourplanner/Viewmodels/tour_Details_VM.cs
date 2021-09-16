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
        public Tour selected_Tour { get; set; }
        public ICommand _editFunc;
        private DAO dataAccessObject { get; set; }
        private RelayCommand RelayCommand { get; set; }
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
        public tour_Details_VM(Tour selected_Tour)
        {
            this.selected_Tour = selected_Tour;
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
            SelectedViewModel = new main_Window_VM();
        }
    }
}
