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

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace WpfFramePasCore.UserControl
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2
    {
        public UserControl2()
        {
            InitializeComponent();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MyViewModel1 model1 = new MyViewModel1();
            MyViewModel2 model2 = new MyViewModel2();

            model2.IsShown = false;
            model2.visibility = Visibility.Hidden;

            model1.IsShown = true;
            model1.visibility = Visibility.Visible;

            ViewModel viewModel = new ViewModel();
            viewModel.DataLoad(model1, model2);

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.DataContext = viewModel;
        }
        private void textChangedEventHandler(object sender, RoutedEventArgs e)
        {
            string server = "localhost";
            string database = "cookies";
            string user = "root";
            string password = "ABCD1234";
            //string port = "3306";
            string connectionString = "server=" + server + ";" +
                                        "uid=" + user + ";" +
                                        "pwd=" + password + ";" +
                                         "database=" + database + ";";

            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                string command = "Select Id,name,main_adress,tel,email from clients where name = 'test2ofofo';";
                MySqlCommand cmd = new MySqlCommand(command, conn);

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                ///////
                //MessageBox.Show(cmd.ToString());
                //adp.SelectCommand = cmd;

                //var build = new MySqlCommandBuilder(adp);
                DataSet ds = new DataSet();

                adp.Fill(ds, "LoadDataBinding");
                //MessageBox.Show(ds.ToString());

                dataGridCustomers.DataContext = ds;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
    }
}
