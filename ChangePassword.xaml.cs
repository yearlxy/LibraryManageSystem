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
using MySql.Data.MySqlClient;

namespace LibraryManageSystem
{
    /// <summary>
    /// ChangePassword.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
            this.enter.IsDefault = true;
            this.cancel.IsCancel = true;
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            bool mark = false;
            string pwd = getPassWord();
            MySqlConnection con = Connect.Connection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select * from login where PassWord = '" + pwd + "' and UserId = '" + Connect.GetUser() + "';";
            command.Connection = con;
            con.Open();  
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                mark = true;
            }
            reader.Close();
            con.Close();
            if (mark)
            {
                ResetPassword reset = new ResetPassword();
                reset.WindowStyle = WindowStyle.None;
                reset.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                reset.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("密码输入不正确！");
            }
        }
        private string getPassWord()
        {
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(this.password.SecurePassword);
            string password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
            return password;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
