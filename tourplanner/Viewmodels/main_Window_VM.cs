using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;
using System.Windows.Input;
using tourplanner.DALayer;
using Microsoft.Xaml.Behaviors.Core;

namespace tourplanner.Viewmodels
{
    public class main_Window_VM : base_VM
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public main_Window_VM()
        {
            log.Info("Initiating Main View");
            DetCommand = new BaseCommand(OpenDet);
            OptCommand = new BaseCommand(OpenOpt);
            AddCommand = new BaseCommand(OpenAdd);
            DelCommand = new BaseCommand(OpenDel);
            LogCommand = new BaseCommand(OpenLog);
            AddLCommand = new BaseCommand(OpenAddLog);
            SumCommand = new BaseCommand(CreateSum);
            RepCommand = new BaseCommand(CreateRep);

            GetTours();
        }
        public Tour selected_Tour { get; set; }
        private DAO dataAccessObject { get; set; }
        public ObservableCollection<Tour> tour_List { get; set; }
        public ICommand DetCommand { get; set; }
        public ICommand OptCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DelCommand { get; set; }
        public ICommand LogCommand { get; set; }
        public ICommand AddLCommand { get; set; }
        public ICommand RepCommand { get; set; }
        public ICommand SumCommand { get; set; }

        public object selectedViewModel;
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        public void GetTours()
        {
            log.Info("Trying to refresh Tourlist to no avail :(");
            dataAccessObject = new DAO();
            Tour_List= new ObservableCollection<Tour>();
            if (Tour_List != null)
            {
                this.Tour_List.Clear();
            }
            foreach(Tour t in dataAccessObject.GetTourList())
            {
                Tour_List.Add(t);
                //BUG: Tour_List Binding does not update even when emptying and refilling List with new items
            }
        }


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
                    SelectedViewModel = new home_VM(selected_Tour);
                    OnPropertyChanged(nameof(selected_Tour));
                }
            }
        }


        //source: https://social.technet.microsoft.com/wiki/contents/articles/30898.simple-navigation-technique-in-wpf-using-mvvm.aspx

        //Navigation Handeling

        private void OpenDet(object obj)
        {
            log.Info("Changing to Tour Detail view");
            if (selected_Tour != null)
            {
                SelectedViewModel = new tour_Details_VM(selected_Tour);
            }
            
        }
        private void OpenOpt(object obj)
        {
            log.Info("Changing to Options view");
            SelectedViewModel = new options_VM();
        }
        private void OpenAdd(object obj)
        {
            log.Info("Changing to Add Tour view");
            Tour t = new Tour();
            SelectedViewModel = new tour_Add_VM(t);
        }
        private void OpenDel(object obj)
        {
            dataAccessObject.DeleteTour(selected_Tour);
            tour_List.Remove(selected_Tour);
            selected_Tour = tour_List[0];
            
        }
        private void OpenLog(object obj)
        {
            log.Info("Changing to Log Details view");
            SelectedViewModel = new logs_VM(selected_Tour);
        }
        private void OpenAddLog(object obj)
        {
            log.Info("Changing to AddLog view");
            SelectedViewModel = new add_Log_VM(selected_Tour);
        }
        private void CreateRep(object obj)
        {
            var pdfGen = new PdfGenerator();
            pdfGen.CreateReport(Selected_Tour);
        }
        private void CreateSum(object obj)
        {
            var pdfGen = new PdfGenerator();
            pdfGen.CreateSummary(Tour_List);
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
