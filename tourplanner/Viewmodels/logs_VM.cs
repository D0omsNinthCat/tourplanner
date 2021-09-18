using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tourplanner.DALayer;
using tourplanner.Models;

namespace tourplanner.Viewmodels
{
    class logs_VM : base_VM
    {
        public Tour selected_Tour { get; set; }
        public Log selected_Log { get; set; }
        public ICommand _editFunc;
        public ICommand _deleteFunc;
        public string unit { get; set; }
        private DAO dataAccessObject { get; set; }

        public logs_VM(Tour selected_Tour){
            this.selected_Tour = selected_Tour;
        }
        public ICommand EditFunc
        {
            get
            {
                if (_editFunc == null)
                {
                    _editFunc = new RelayCommand(param => this.EditLog(), param => this.CanExecute());
                }
                return _editFunc;
            }
        }
        public ICommand DeleteFunc
        {
            get
            {
                if (_deleteFunc == null)
                {
                    _deleteFunc = new RelayCommand(param => this.DeleteLog(), param => this.CanExecute());
                }
                return _deleteFunc;
            }
        }
        private bool CanExecute()
        {
            //ADD CONDITIONS
            return true;
        }
        private void EditLog()
        {
            dataAccessObject = new DAO();
            dataAccessObject.EditLog(Selected_Log);
        }
        private void DeleteLog()
        {
            dataAccessObject = new DAO();
            dataAccessObject.DeleteLog(Selected_Log);
            selected_Tour.Logs.Remove(Selected_Log);
            selected_Log = selected_Tour.Logs[0];
        }

        public Log Selected_Log
        {
            get { 
                return selected_Log; }
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
