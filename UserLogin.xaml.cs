using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Input;
using System;

namespace LibraryManageSystem
{
    /// <summary>
    /// UserLogin.xaml 的交互逻辑
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
            this.btn1.IsCancel = true;
            this.btn2.IsDefault = true;
         
        }



        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public string UserId { get; set; }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            string User = this.user.Text.Trim();
            string Pwd = GetPassword();
            MySqlConnection con = Connect.Connection();


            if(User ==""||Pwd == "")
            {
                this.message.Content = "用户名或密码不能为空.";
            }
            else
            {
                try
                {
                    con.Open();
                    string cmdstr = "select * from login where UserId = '" + User + "' and Password = '" + Pwd + "';";
                    MySqlCommand command = new MySqlCommand(cmdstr, con);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("登录成功！");
                        UserId = User;
                        con.Close();
                        this.DialogResult = true;
                    }
                    else
                    {
                        this.message.Content = "用户名或密码错误.";
                        con.Close();
                    }


                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("数据库连接失败" + ex.Message);
                }
            }
        }
         private void Register_Click(object sender, RoutedEventArgs e)
         {
            Register register = new Register();
            register.WindowStyle = WindowStyle.None;
            register.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            if (register.ShowDialog() == true)
            {
                register.Close();
            }
         }
        private string GetPassword()
        {
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(this.password.SecurePassword);
            string password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
            return password;
        }

       
    }
}
