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
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            this.btn1.IsDefault = true;
            this.btn2.IsCancel = true;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)//注册按钮
        {
            string user = this.Id.Text.Trim();
            string pwd1 = GetPassword1();
            string pwd2 = GetPassword2();
            if(user == ""||pwd1 == ""||pwd2 == "")
            {
                MessageBox.Show("不能为空！");
            }
            else if(user == "midpic"||user == "default"||user == "select" ||user == "insert"||user =="update"||user =="user"||user == "delete"||user == "drop")
            {
                MessageBox.Show("不能以系统关键字为用户名！");
            }
            else if (pwd1 != pwd2)
            {
                MessageBox.Show("两次输入密码不同！");
                Pwd1.Password = "";
                Pwd2.Password = "";
            }
            else if (pwd1.Length < 8)
            { 
                MessageBox.Show("输入密码过短！");
                Pwd1.Password = "";
                Pwd2.Password = "";

            }
            else if (JudgeEasility(pwd1))
            { 
                MessageBox.Show("密码太简单！");
                Pwd1.Password = "";
                Pwd2.Password = "";
            }
            else
            {
                MySqlConnection con = Connect.Connection();
                try
                {
                    con.Open();
                    string check = string.Format("select * from login where UserId = '{0}';", user);
                    MySqlCommand command = new MySqlCommand(check, con);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("已存在该用户名！");
                        Id.Text = "";
                        Pwd1.Password = "";
                        Pwd2.Password = "";
                        con.Close();
                    }
                    else
                    {
                        reader.Close();

                        string cmd = string.Format("insert into login values ('{0}','{1}');", user, pwd1);
                        command = new MySqlCommand(cmd, con);
                        command.ExecuteNonQuery();
                        string sel = string.Format("select * from login where UserId = '{0}' and Password = '{1}';", user, pwd1);
                        command = new MySqlCommand(sel, con);
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            this.DialogResult = true;
                            MessageBox.Show("注册成功！");
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("注册失败！");
                            con.Close();
                        }
                    }
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show("数据库连接失败" + ex.Message);
                    con.Close();
                }
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)//返回按钮
        {
            this.DialogResult = true;
        }

         private string GetPassword1()
        {
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(this.Pwd1.SecurePassword);
            string password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
            return password;
        }
        private string GetPassword2()
        {
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(this.Pwd2.SecurePassword);
            string password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
            return password;
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
    }
}
