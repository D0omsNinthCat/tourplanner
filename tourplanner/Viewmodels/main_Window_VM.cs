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
            string Dummy_List = "testetsestset";
            ObservableCollection<Tour> Tour_List = new ObservableCollection<Tour>(dataAccessObject.GetTourList());
            DetCommand = new BaseCommand(OpenDet);
            OptCommand = new BaseCommand(OpenOpt);
            AddCommand = new BaseCommand(OpenAdd);
            DelCommand = new BaseCommand(OpenDel);
        }
        public Tour selected_Tour;
        public ObservableCollection<Tour> tour_List;
        public string dummy_List;
        public ICommand DetCommand { get; set; }
        public ICommand OptCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DelCommand { get; set; }

        private object selectedViewModel;

        public ObservableCollection<Tour> Tour_List
        {
            get { return tour_List; }
            set
            {
                if ((value != null) && (tour_List != value))
                {
                    tour_List = value;
                    OnPropertyChanged(nameof(tour_List));
                }
            }
        }
        public Tour Selected_Tour
        {
            get { return selected_Tour; }
            set
            {
                if ((value != null) && (selected_Tour != value))
                {
                    selected_Tour = value;
                    OnPropertyChanged(nameof(selected_Tour));
                }
            }
        }
        public string Dummy_List
        {
            get { return dummy_List; }
            set
            {
                if ((value != null) && (dummy_List != value))
                {
                    dummy_List = value;
                    OnPropertyChanged(nameof(dummy_List));
                }
            }
        }



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
        private void OpenDel(object obj)
        {
            
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
