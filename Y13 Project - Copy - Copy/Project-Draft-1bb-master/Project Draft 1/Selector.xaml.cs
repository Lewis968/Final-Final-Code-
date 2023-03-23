using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace Project_Draft_1
{
    /// <summary>
    /// Interaction logic for Selector.xaml
    /// </summary>
    public partial class Selector : Window
    {
        public static class Gravity
        {
            public static double g;      //document public class
        }

        public List<Rocket> importedRockets = new List<Rocket>();
        public void m()
        {
            string CSVDataBase = @"\\svr-pfs-02\16RandelL$\My Documents\Y13 Project - Copy - Copy\Project-Draft-1bb-master\Output.csv";
            foreach (string Line in File.ReadLines(CSVDataBase))
            {
                bool success = int.TryParse(Line.Split(',').ElementAt(1), out int tempthrust);
                bool success2 = int.TryParse(Line.Split(',').ElementAt(2), out int tempdm);
                bool success3 = int.TryParse(Line.Split(',').ElementAt(3), out int tempwm);
                bool success4 = int.TryParse(Line.Split(',').ElementAt(4), out int tempsi);
                if (success && success2 && success3 && success4)
                {
                    string name = Line.Split(',').ElementAt(0);
                    int T = tempthrust;
                    double w = tempwm;
                    double W = w * 9.81;
                    double TW = T / W;
                    TW = Math.Round(TW);
                    myRocket = new Rocket(name, tempthrust, TW, tempdm, tempwm, tempsi, 0, 70000);
                    importedRockets.Add(myRocket);
                    Chooser.Items.Add(name);
                    //add more success //added
                }
                else
                {
                    MessageBox.Show("Ensure any added rockets have correct syntax");
                    return;
                }
            }
        }
        public static Rocket myRocket = null;
        public class Rocket
        {
            private string Name; // Name
            private int Thrust; //N;
            private double TWR; // N/KG
            private double WetMass; // kg
            private double DryMass; // kg
            private int SpecificImpulse; // s
            private double ExaustVelocity; // m/s
            private double ExitPressure; // nm/s2

            //public methods

            //get/set methods
            public void SetName(string n) { Name = n; }
            public string GetName() { return Name; }
            public void SetThrust(int t) { Thrust = t; }
            public int GetThrust() { return Thrust; }
            public void SetTWR(double w) { TWR = w; }
            public double GetTWR() { return TWR; }
            public void SetWetMass(double wm) { WetMass = wm; }
            public double GetWetMass() { return WetMass; }
            public void SetDryMass(double dm) { DryMass = dm; }
            public double GetDryMass() { return DryMass; }
            public void SetSpecificImpulse(int s) { SpecificImpulse = s; }
            public int GetSpecificImpulse() { return SpecificImpulse; }
            public void SetExaustVelocity(double e) { ExaustVelocity = e; }
            public double GetExaustVelocity() { return ExaustVelocity; }
            public void SetExitPressure(double ep) { ExitPressure = ep; }
            public double GetExitPressure() { return ExitPressure; }
            public Rocket(string name, int Thrust, double TWR, int WetMass, int DryMass, int SpecificImpulse, double ExaustVelocity, double ExitPressure)
            {
                this.SetName(name);
                this.SetThrust(Thrust);
                this.SetTWR(TWR);
                this.SetWetMass(WetMass);
                this.SetDryMass(DryMass);
                this.SetSpecificImpulse(SpecificImpulse);
                this.SetExaustVelocity(ExaustVelocity);
                this.SetExitPressure(ExitPressure);
            }
            public Rocket()
            {
                //default constructor no overloads
            }
        }
        public Selector()
        {
            InitializeComponent();
            m();
            Location.Items.Add("Earth");
            Location.Items.Add("Moon");
            Location.Items.Add("Mars");
        }
        private void Chooser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            if (this.Location.SelectedItem == null || this.Location.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter a launch location");
            }
            else
            {
                Hide(); // to close window
                Graph_Controller w = new Graph_Controller();
                w.Show();       //Document had to call user control as a window =--------= Now changed to graph controller
            }
        }

        public void Values_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            if (counter != 0)
                return;
            // to stop multiple clicks of value button creating infinite TWR
                if (this.Location.SelectedItem == null || this.Location.SelectedIndex == -1)
                {
                    MessageBox.Show("Please enter a launch location");
                }
                if (this.Chooser.SelectedItem == null || this.Chooser.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select a Rocket");
                }
                else
                {
                    if (Chooser.SelectedValue == null)
                    {
                        return;
                    }
                    string tempname = Chooser.SelectedValue.ToString();
                    Rocket myRocket = new Rocket();


                foreach (Rocket tempRocket in importedRockets)
                {
                    if (tempRocket.GetName() == tempname)
                    {
                        myRocket = tempRocket;
                        Thrusttxt.Text = myRocket.GetThrust().ToString();           //Crashes when rocket is not selected - Fixed Above using else statement
                        Drytxt.Text = myRocket.GetDryMass().ToString();
                        WetTxt.Text = myRocket.GetWetMass().ToString();
                        SItxt.Text = myRocket.GetSpecificImpulse().ToString();
                        break;
                    }
                }
                switch (Location.Text)
                {
                    case "Earth":
                        {
                            double g = 9.81;
                            Gravity.g = g;
                            int T = myRocket.GetThrust();
                            double w = myRocket.GetWetMass();
                            double W = w * g;
                            double TW = T / W;
                            T_W_txt.Text = TW.ToString();
                            myRocket.SetTWR(TW);
                            //double E = myRocket.GetExaustVelocity();
                            //double ep = myRocket.GetExitPressure(); // document the pain //app.diagrams.net
                            //E = (T - (ep - atm) * 2.086);
                            //E = E / 2960;
                            //myRocket.SetExaustVelocity(E);
                            //2776
                        }
                        break;


                    case "Moon": // Make sure to edit Thrust and Weight, etc... for all values of g 
                        {
                            double g = 1.625;
                            Gravity.g = g;
                            double drymass = myRocket.GetDryMass();
                            drymass = (drymass / 9.81) * g;
                            double wetmass = myRocket.GetWetMass();
                            wetmass = (wetmass / 9.81) * g;
                            wetmass = Math.Round(wetmass);
                            drymass = Math.Round(drymass);
                            myRocket.SetWetMass(wetmass);
                            myRocket.SetDryMass(drymass);
                            int T = myRocket.GetThrust();
                            double w = myRocket.GetWetMass();
                            double W = w * g;
                            double TW = T / W;
                            TW = Math.Round(TW);
                            T_W_txt.Text = TW.ToString();
                            Drytxt.Text = myRocket.GetDryMass().ToString();
                            WetTxt.Text = myRocket.GetWetMass().ToString();
                            myRocket.SetTWR(TW);

                            break;
                        }
                    case "Mars":
                        {
                            double g = 3.72;
                            Gravity.g = g;
                            double drymass = myRocket.GetDryMass();
                            drymass = (drymass / 9.81) * g;
                            double wetmass = myRocket.GetWetMass();
                            wetmass = (wetmass / 9.81) * g;
                            wetmass = Math.Round(wetmass);
                            drymass = Math.Round(drymass);
                            myRocket.SetWetMass(wetmass);
                            myRocket.SetDryMass(drymass);
                            int T = myRocket.GetThrust();
                            double w = myRocket.GetWetMass();
                            double W = w * g;
                            double TW = T / W;
                            TW = Math.Round(TW);
                            T_W_txt.Text = TW.ToString();
                            Drytxt.Text = myRocket.GetDryMass().ToString();
                            WetTxt.Text = myRocket.GetWetMass().ToString();
                            myRocket.SetTWR(TW);

                            break;
                        }
                    default:
                        break;
                
                    }
            counter++;
                }
            }
        }
    }














