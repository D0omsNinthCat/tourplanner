using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tourplanner.Readers;
using tourplanner.Mappers;
using System.Collections.ObjectModel;
using tourplanner.Models;

namespace tourplanner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            object_Reader_Tour rdr = new object_Reader_Tour(); 
            Collection<Tour> test = rdr.Execute();


        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            object_Reader_Tour rdr = new object_Reader_Tour();
            Collection<Tour> test = rdr.Execute();
            foreach( Tour t in test)
            {
                lv1.Items.Add(t.tour_Name);
            }
        }
    }
}
