
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;



namespace Project_Draft_1                       //https://flightclub.io/
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        DataTable dt = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
            Mode_Selector.Items.Add("Rocket Selector");
            Mode_Selector.Items.Add("Rocket Builder");
            Mode_Selector.Items.Add("See Previous Rockets");
            Datafr.Opacity = 0.0;
        }

        private void Mode_Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        public void Run_Click(object sender, RoutedEventArgs e)
        {
            switch (Mode_Selector.Text)
            {
                case "Rocket Selector":
                    Selector p = new Selector();
                    p.Show();
                    break;

                case "Rocket Builder":
                    Builder o = new Builder();
                    o.Show();
                    break;

                case "See Previous Rockets":
                    {
                        string CSVDataBase = @"\\svr-pfs-02\16RandelL$\My Documents\Y13 Project - Copy - Copy\Project-Draft-1bb-master\Output.csv";
                        //Create Collection for DataGrid Source
                        ICollection CreateDataSource()
                        {
                            //Create new DataTables and Rows
                            DataTable dt = new DataTable();
                            DataRow dr;
                            //Create Column Headers
                            dt.Columns.Add(new DataColumn("Name", typeof(string)));
                            dt.Columns.Add(new DataColumn("Thrust", typeof(string)));
                            dt.Columns.Add(new DataColumn("TWR", typeof(string)));
                            dt.Columns.Add(new DataColumn("Dry Mass", typeof(string)));
                            dt.Columns.Add(new DataColumn("Wet Mass", typeof(string)));

                            //For each line in the File
                            foreach (string Line in File.ReadLines(CSVDataBase))
                            {
                                //Split lines at delimiter ';''

                                //Create new Row
                                dr = dt.NewRow();

                                //ID=
                                dr[0] = Line.Split(',').ElementAt(0);

                                //Name =
                                dr[1] = Line.Split(',').ElementAt(1);

                                //Age=
                                dr[2] = Line.Split(',').ElementAt(2);

                                //Gender= 
                                dr[3] = Line.Split(',').ElementAt(3);

                                dr[4] = Line.Split(',').ElementAt(4);

                                //Add the row we created
                                dt.Rows.Add(dr);
                            }

                            //Return Dataview 
                            DataView dv = new DataView(dt);
                            return dv;
                        }
                        Datafr.ItemsSource = CreateDataSource();
                        Datafr.Opacity = 100;
                        Datafr.IsReadOnly = true;

                    }
                    break;
            }
        }

        private void Changes_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to make changes?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        Datafr.IsReadOnly = false;
                    }
                    break;
                case MessageBoxResult.No:
                    {
                        //cancel request
                    }
                    break;
            }
        }
    }
}






    

