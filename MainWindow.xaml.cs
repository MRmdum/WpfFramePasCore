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

using System.Data;
using System.Configuration;
using System.ComponentModel;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace WpfFramePasCore
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool gridVisibility = false;

        public MainWindow()
        {
            
            InitializeComponent();
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Window1 win = new Window1();
            Visibility = Visibility.Hidden;
            win.Show();
        }
        
        private void btnloaddata_Click(object sender, RoutedEventArgs e) {

            string server = "localhost";
            string database = "cookies";
            string user = "root";
            string password = "ABCD1234";
            //string port = "3306";
            string connectionString = "server=" + server + ";" +
                                        "uid=" + user + ";" + 
                                        "pwd=" + password + ";"+
                                         "database=" + database + ";" ;
            
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from clients;", conn);

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                ///////
                MessageBox.Show(cmd.ToString());
                adp.SelectCommand = cmd;
                
                //adp.Fill(table);
                //////
                //var build = new MySqlCommandBuilder(adp);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                adp.Fill(dt);
                //MessageBox.Show(ds.ToString());

                dataGridCustomers.DataContext = ds;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
                conn.Close();
            
        }

        private void HideButtonOnClick(object sender, RoutedEventArgs e)
        {
            GridVisibility = true;
        }
        public bool GridVisibility
        {
            get
            {
                return gridVisibility;
            }
            set
            {
                gridVisibility = value;

                NotifyPropertyChanged("GridVisibility");
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    
    public partial class Window1 : Window
    {
        public Window1()
        {

        }
    }
}
