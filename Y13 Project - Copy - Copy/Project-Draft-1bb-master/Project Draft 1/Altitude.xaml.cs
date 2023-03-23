
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using static Project_Draft_1.Selector;

namespace Project_Draft_1
{
    /// <summary>
    /// Interaction logic for Altitude.xaml
    /// </summary>
    public partial class Altitude : UserControl
    {
        public Altitude()
        {
            InitializeComponent();
            double alt = 0;
            double seconds = 0;
            if (myRocket.GetTWR() <= 1)
            {
                MessageBox.Show("Rocket has a TWR of 1 or less, it will not lift off. The Following Graphs may be incorrect.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { alt },
                    PointGeometry = null
                },
            };

            Labels = new[] { "160", "140", "120", "100", "80", "60", "40", "20", "0" };
            YFormatter = value => value.ToString("C");

            SeriesCollection.Add(new LineSeries
            {
                Title = "Series 1",
                Values = new ChartValues<double> { alt },
                PointGeometry = null
            });

            SeriesCollection[1].Values.Clear(); // fixed a non zero error 
            for (seconds = 0; seconds < 161; seconds++)
            {

                alt = 0.5 * ((myRocket.GetThrust() / myRocket.GetWetMass()) - Gravity.g) * seconds * seconds * 2; //Calcuations for Max thrust no drag https://www.nakka-rocketry.net/articles/altcalc.pdf
                if (alt <= 0)
                {
                    alt = 0;
                }
                // first stage termination point
                SeriesCollection[1].Values.Add(alt);

            }
            if (alt <= 80000)
            {
                MessageBox.Show("Rocket Will not escape earth's Atmosphere", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}

        
    

