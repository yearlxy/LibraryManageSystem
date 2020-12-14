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

namespace LibraryManageSystem
{
    /// <summary>
    /// Return.xaml 的交互逻辑
    /// </summary>
    public partial class Return : Page
    {
        public Return()
        {
            InitializeComponent();
            Init();
           
        }
        //点击事件
        //==========================================================================================================

        private void ReturnBook(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name;
            string[] array = name.Split('_');
            name = array[1];
            TextBox tb = this.FindName(name) as TextBox;
            array = tb.Text.Split('|');
            string bookname = array[0].Trim();
            string writer = array[1].Trim();

            MySqlConnection con = Connect.Connection();
            MySqlCommand command;
            MySqlDataReader reader;
            string cmd = "delete from borrow where BookName = '" + bookname + "' and Writer = '" + writer + "' and UserId = '" + Connect.GetUser() + "';";

            try
            {
                con.Open();
                command = new MySqlCommand(cmd, con);
                command.ExecuteNonQuery();

                cmd ="select * from borrow where BookName = '" + bookname + "' and Writer = '" + writer + "' and UserId = '" + Connect.GetUser() + "'; ";
                command = new MySqlCommand(cmd, con);

                reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("成功还书！");
                }
                Init();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //功能函数
        //========================================================================================================

        private void Init()
        {
            this.no1.Text = "";
            this.no2.Text = "";
            this.no3.Text = "";
            MySqlConnection con = Connect.Connection();
            MySqlDataReader reader;
            MySqlCommand command;
            string cmd = "select * from borrow where UserId = '" + Connect.GetUser() + "';";
            int i = 0;

            try
            {
                con.Open();
                command = new MySqlCommand(cmd, con);
                reader = command.ExecuteReader();
                string bookname, writer;
                TextBox tb;
                
                while (reader.Read())
                {
                    i++;
                    bookname = reader["BookName"].ToString().Trim();
                    writer = reader["Writer"].ToString().Trim();
                    string str1 = Formatting(bookname,15);
                    string str2 = Formatting(writer, 15);
                    str2 = str2.TrimEnd('|').Trim();                    
                    string str = str1 + str2;
                    string name = "no" + i;
                    tb = this.FindName(name) as TextBox;
                    tb.Text = str;
                }
                i++;

                for (; i <= 3; i++)
                {
                    Button btn = this.FindName("btn_no" + i) as Button;
                    btn.IsEnabled = false;
                }
                reader.Close();
                con.Close();


            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void SetButtonColor()
        {
            Button btn;
            for(int i = 1; i <= 3; i++)
            {
                string name = "btn" + i;
                btn = this.FindName(name) as Button;
                if(btn.IsEnabled == false)
                {
                    btn.Background = System.Windows.Media.Brushes.Black;
                }
                if(btn.IsEnabled == true)
                {
                    btn.Background = System.Windows.Media.Brushes.White;
                }
            }
        }

        //private void Try()
        //{

        //    while (true)
        //    {
        //        string name = "btn" + i;
        //        Button btn = this.FindName(name) as Button;
        //        btn.IsEnabled = false;
        //    }
        //}
        public string Formatting(string s, int num)
        {
            int i = GetLength(s);
            while (i < num)
            {
                s += " ";
                i++;
            }
            return s + "|";
        }//格式化得到的数据，用于制作表格;
        public static int GetLength(string str)
        {
            if (str == null || str.Length == 0) { return 0; }

            int l = str.Length;
            int realLen = l;

            #region 计算长度
            int clen = 0;//当前长度
            while (clen < l)
            {
                //每遇到一个中文，则将实际长度加一。
                if ((int)str[clen] > 128)
                {
                    realLen++;

                }
                clen++;

            }
            #endregion

            return realLen;
        }//获得字符串实际长度

    }
}
