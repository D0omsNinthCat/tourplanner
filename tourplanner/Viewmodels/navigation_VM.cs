using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace tourplanner.Viewmodels
{
    class navigation_VM : main_Window_VM
    {
        //source: https://social.technet.microsoft.com/wiki/contents/articles/30898.simple-navigation-technique-in-wpf-using-mvvm.aspx
        public ICommand DetCommand { get; set; }
        public ICommand OptCommand { get; set; }
        private object selectedViewModel;
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }
        public navigation_VM()
        {
            DetCommand = new BaseCommand(OpenDet);
            OptCommand = new BaseCommand(OpenOpt);
        }

        private void OpenDet(object obj)
        {
            SelectedViewModel = new tour_Details_VM(selected_Tour_Name);
        }
        private void OpenOpt(object obj)
        {
            SelectedViewModel = new options_VM();
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
