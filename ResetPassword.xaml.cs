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
    /// ResetPassword.xaml 的交互逻辑
    /// </summary>
    public partial class ResetPassword : Window
    {
        public ResetPassword()
        {
            InitializeComponent();
            this.enter.IsDefault = true;
            this.cancel.IsCancel = true;
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            string pwd1 = getPassWord(this.pb1);
            string pwd2 = getPassWord(this.pb2);
            if ( pwd1 == "" || pwd2 == "")
            {
                MessageBox.Show("不能为空！");
            }
            else if (pwd1 != pwd2)
            {
                MessageBox.Show("两次输入密码不同！");
                pb1.Password = "";
                pb2.Password = "";
            }
            else if (pwd1.Length < 8)
            {
                MessageBox.Show("输入密码过短！");
                pb1.Password = "";
                pb2.Password = "";

            }
            else if (JudgeEasility(pwd1))
            {
                MessageBox.Show("密码太简单！");
                pb1.Password = "";
                pb2.Password = "";
            }
            else
            {
                MySqlConnection con = Connect.Connection();
                MySqlCommand command = new MySqlCommand();
                command.Connection = con;
                command.CommandText = "update login set PassWord = '" + pwd1 + "' where UserId = '" + Connect.GetUser() + "';";
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("更改成功！");
                this.Close();
            }

        }

        private string getPassWord(PasswordBox password)
        {
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password.SecurePassword);
            string password1 = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
            return password1;
        }

        private bool JudgeEasility(string str)
        {
            string[] strs = {
            "123456",
            "abcdef"
            };
            int cnt = 0;
            foreach (var s in strs)
            {
                if (str.Contains(s))
                {
                    cnt++;
                }
            }
            if (cnt > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
