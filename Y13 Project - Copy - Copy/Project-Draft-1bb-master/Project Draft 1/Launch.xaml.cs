
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using static Project_Draft_1.Selector;


namespace Project_Draft_1
{
    public partial class Launch : UserControl
    {
        public Launch()
        {
            InitializeComponent();
            double seconds = 0;
            double Velocity = 0;
            int thrust = myRocket.GetThrust();
            double Ev = myRocket.GetExaustVelocity();
            double wetmass = myRocket.GetWetMass();
            double drymass = myRocket.GetDryMass();
            double g = Gravity.g;
            if (myRocket.GetTWR() <= 1)
            {
                MessageBox.Show("Rocket has a TWR of 1 or less, it will not lift off", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { Velocity },
                    PointGeometry = null
                },
            };

            Labels = new[] { "0", "20", "40", "60", "80", "100", "120", "140", "160" };
            YFormatter = value => value.ToString("C");

            SeriesCollection.Add(new LineSeries
            {
                Title = "Series 1",

                Values = new ChartValues<double> { Velocity },
                PointGeometry = null
            });

            SeriesCollection[1].Values.Clear();
            for (seconds = 0; seconds <= 160; seconds++)
            {
                double T = myRocket.GetThrust();
                double atm = 101324;
                double Temp = T / (3.25 * ((0.7 * atm) - atm) * 2.086);
                Velocity = Temp * (Math.Log(drymass) - Math.Log(wetmass) - (g * seconds));
                Math.Round(Velocity);
                SeriesCollection[1].Values.Add(Velocity);
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
