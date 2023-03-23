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
using static Project_Draft_1.Selector;
using System.Windows.Shapes;
using System.IO;
using System.Text;
using System.Collections;
using System.Data;

namespace Project_Draft_1
{
    /// <summary>
    /// Interaction logic for Selector.xaml
    /// </summary>

    public partial class Builder : Window
    {
        public Builder()
        {
            InitializeComponent();
            Location.Items.Add("Earth");
            Location.Items.Add("Moon");
            Location.Items.Add("Mars");
            Options.Items.Add("Save");
            Options.Items.Add("Remove Last Save");
            Options.Items.Add("Clear all Saved");
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
        string name = null;
        int number = 0;
        int number2 = 0;
        int number3 = 0;
        int number4 = 0;
        public void Values_Click(object sender, RoutedEventArgs e)
        {
            if (this.Location.SelectedItem == null || this.Location.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter a launch location");
            }
            else
            {
                string a = null;
                a = Thrusttxt.Text;
                string b = null;
                b = Drytxt.Text;
                string c = null;
                c = WetTxt.Text;
                string d = null;
                d = SItxt.Text;

                bool success = int.TryParse(a, out number);
                bool success2 = int.TryParse(b, out number2);
                bool success3 = int.TryParse(c, out  number3);
                bool success4 = int.TryParse(d, out  number4);
                if (success && success2 && success3 && success4)
                {
                    myRocket = new Rocket();
                    name = Name.Text;
                    myRocket.SetThrust(number);
                    myRocket.SetDryMass(number2);
                    myRocket.SetWetMass(number3);
                    myRocket.SetSpecificImpulse(number4);
                    myRocket.SetName(name);
                   
                }
                else
                {
                    MessageBox.Show("Ensure all Values entered are integer and not letters");
                    return;
                }
            }

            //Crashes when rocket is not selected - Fixed Above using else statement
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
                    }
                    break;

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
                    }
                    break;
                default:
                    break;
            }
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            switch (Options.Text)
            {
                case "Save":
                    {
                        //string[] Data = { name, number.ToString(), number2.ToString(), number3.ToString(), number4.ToString() };
                        StringBuilder output = new StringBuilder();
                        String separator = ",";
                        String[] headings = { "Name", "Thrust", "DryMass", "WetMass", "Specific Impulse" };
                        String[] newLine = { name, number.ToString(), number2.ToString(), number3.ToString(), number4.ToString() };
                        output.AppendLine(string.Join(separator, newLine));
                        try
                        {
                            String file = @"\\svr-pfs-02\16RandelL$\My Documents\Y13 Project - Copy - Copy\Project-Draft-1bb-master\Output.csv";
                            File.AppendAllText(file, output.ToString());
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Data could not be written to the CSV file.");
                            return;
                        }
                    }
                    break;

                 case "Remove Last Save":
                    {
                        MessageBoxResult result = MessageBox.Show("are you sure you want to remove the last save? this cannot be undone, you'll be removing the following rocket:", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                {
                                    List<string> newLine = File.ReadAllLines(@"\\svr-pfs-02\16RandelL$\My Documents\Y13 Project - Copy - Copy\Project-Draft-1bb-master\Output.csv").ToList();

                                    File.WriteAllLines(@"\\svr-pfs-02\16RandelL$\My Documents\Y13 Project - Copy - Copy\Project-Draft-1bb-master\Output.csv", newLine.GetRange(0, newLine.Count - 1).ToArray());
                                }
                                break;
                            case MessageBoxResult.No:
                                {
                                    //cancel request
                                }
                                break;
                        }
                    }
                    break;

                case "Clear all Saved":
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to remove ALL Saved Rockets?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.Yes);

                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                {
                                    List<string> newLine = File.ReadAllLines(@"\\svr-pfs-02\16RandelL$\My Documents\Y13 Project - Copy - Copy\Project-Draft-1bb-master\Output.csv").ToList();

                                    File.WriteAllLines(@"\\svr-pfs-02\16RandelL$\My Documents\Y13 Project - Copy - Copy\Project-Draft-1bb-master\Output.csv", newLine.GetRange(0, 0).ToArray());
                                }
                                break;
                            case MessageBoxResult.No:
                                {
                                    //cancel request
                                }
                                break;
                        }
                    }
                    break;
            }      

        }

        private void Options_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}

        
        
                

            


    


        
    

       





    




