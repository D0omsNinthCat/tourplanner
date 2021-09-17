using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;
using tourplanner.DALayer;
using System.Windows.Input;

namespace tourplanner.Viewmodels
{
    class add_Log_VM : base_VM
    {
        private DAO dataAccessObject { get; set; }
        private RelayCommand RelayCommand { get; set; }
        public Log new_Log { get; set; }

        public ICommand _addLogFunc;
        public add_Log_VM (Tour selected_Tour)
        {
            this.new_Log = new Log();
            new_Log.tour_ID = selected_Tour.tour_ID;
            new_Log.log_Distance = selected_Tour.tour_Distance;
        }

        public ICommand AddLogFunc
        {
            get
            {
                if (_addLogFunc == null)
                {
                    _addLogFunc = new RelayCommand(param => this.AddLog(), param => this.CanExecute());
                }
                return _addLogFunc;
            }
        }
        private bool CanExecute()
        {
            //ADD CONDITIONS
            return true;
        }
        private void AddLog()
        {
            dataAccessObject = new DAO();
            dataAccessObject.AddLog(new_Log);
            //SelectedViewModel = new main_Window_VM();
        }
    }
}
