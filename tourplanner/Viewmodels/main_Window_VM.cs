using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;
using System.Windows.Input;
using tourplanner.DALayer;

namespace tourplanner.Viewmodels
{
    public class main_Window_VM : base_VM
    {
        private DAO dataAccessObject = new DAO();
        public main_Window_VM()
        {
            List<Tour> tour_List = dataAccessObject.GetTourList();
            DetCommand = new BaseCommand(OpenDet);
            OptCommand = new BaseCommand(OpenOpt);
            AddCommand = new BaseCommand(OpenAdd);
        }
        public Tour selected_Tour { get; set; }
        public Collection<Tour> tour_List { get; set; }
        public ObservableCollection<string> tour_Names { get; } = new ObservableCollection<string>();
        public ICommand DetCommand { get; set; }
        public ICommand OptCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private object selectedViewModel;
        //public Tour Selected_Tour
        //{
        //    get { return selected_Tour; }
        //    set
        //    {
        //        if ((value != null) && (selected_Tour != value))
        //        {
        //            selected_Tour = value;
        //            OnPropertyChanged(nameof(selected_Tour));
        //        }
        //    }
        //}

        //source: https://social.technet.microsoft.com/wiki/contents/articles/30898.simple-navigation-technique-in-wpf-using-mvvm.aspx

        //Navigation Handeling
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        private void OpenDet(object obj)
        {
            SelectedViewModel = new tour_Details_VM(selected_Tour);
        }
        private void OpenOpt(object obj)
        {
            SelectedViewModel = new options_VM();
        }
        private void OpenAdd(object obj)
        {
            SelectedViewModel = new tour_Add_VM();
        }

        public class BaseCommand : ICommand
        {
            private Predicate<object> _canExecute;
            private Action<object> _method;

            public event EventHandler CanExecuteChanged;


            public BaseCommand(Action<object> method) : this(method, null)
            {
            }

            public BaseCommand(Action<object> method, Predicate<object> canExecute)
            {
                _method = method;
                _canExecute = canExecute;
            }


            public bool CanExecute(object parameter)
            {
                if (_canExecute == null)
                {
                    return true;
                }

                return _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _method.Invoke(parameter);
            }
        }
    }
}
