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
using MySql.Data.MySqlClient;
using System.IO;

namespace LibraryManageSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            string path1 = "d:/LibraryResource/UserPicture/";
            string path2 = "d:/LibraryResource/Text/";
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
            }
            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
            }

            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Win_SourceInitialized(object sender, EventArgs e)
        {
            UserLogin login = new UserLogin();
            login.WindowStyle = WindowStyle.None;
            login.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            if (login.ShowDialog() == false)
            {
                this.Close();
            }
            else
            {
                Connect.SetUser(login.UserId);
                tip.Content = "当前用户：" + Connect.GetUser();
            }
            //tip.Content = "当前用户：1";
            //Connect.SetUser("1");


        }


        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new SeatBook());
        }

        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Borrow());
        }


        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Return());
        }

        private void Internetbook_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Onlineview());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new UserMessage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.ShowDialog();
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            cp.WindowStyle = WindowStyle.None;
            cp.ShowDialog();
        }
    }

    //功能类
    //====================================================================================================
    static public class Connect
    {
        private static string User = "";

        static public void SetUser(string s)
        {
            User = s;
        }
        static public string GetUser()
        {
            return User;
        }
        static public MySqlConnection Connection()
        {
            string constr = "server=localhost;port=3306;user=root;password=12345678;database=library;";
            MySqlConnection con = new MySqlConnection(constr);
            return con;
        }
    }
}
