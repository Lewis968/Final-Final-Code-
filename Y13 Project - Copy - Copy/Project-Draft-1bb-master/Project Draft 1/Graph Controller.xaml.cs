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
using System.Windows.Shapes;

namespace Project_Draft_1
{
    /// <summary>
    /// Interaction logic for Graph_Controller.xaml
    /// </summary>
    public partial class Graph_Controller : Window
    {
        public Graph_Controller()
        {
            InitializeComponent();
        }

        private void Velocity_Click(object sender, RoutedEventArgs e)
        {
            Window w = new Window();
            w.Content = new Launch();      //Document had to call user control as a window
            w.Show();
        }

        private void Alt_Click(object sender, RoutedEventArgs e)
        {
            Window w = new Window();
            w.Content = new Altitude();      //Document had to call user control as a window
            w.Show();
        }
    }
}
