using System;
using System.Collections.Generic;
using System.IO;
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
    /// Onlineview.xaml 的交互逻辑
    /// </summary>
    public partial class Onlineview : Page
    {
        public Onlineview()
        {
            InitializeComponent();
            Init();
        }
        //点击事件
        //==================================================================================================================================================

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            text.Clear();
            if(list1.SelectedItem == null)
            {
                return;
            }
            if(list1.SelectedIndex == 0)
            {
                return;
            }
            string str = list1.SelectedItem.ToString().TrimEnd('|');
            string[] array = str.Split('|');
            string bookname = array[0];
            string writer = array[1];


            MySqlConnection con = Connect.Connection();
            MySqlDataReader reader;
            MySqlCommand command;
            string cmd = "select Path from bookpath where BookName = '" + bookname + "' and Writer = '" + writer + "';";
            try
            {
                con.Open();
                command = new MySqlCommand(cmd, con);
                reader = command.ExecuteReader();
                reader.Read();
                if (!reader.HasRows)
                {
                    MessageBox.Show("未找到文件！");
                    return;
                }
                string  path = reader["Path"].ToString();
                //if (reader.HasRows)
                //{
                //    path = reader["Path"].ToString();
                //}
                //else
                //{
                //    MessageBox.Show("暂时没有相关书籍！");
                //}
                reader.Close();

                string temp = string.Empty;
                StreamReader streamReader = new StreamReader(@path,Encoding.UTF8);
                while (!streamReader.EndOfStream)
                {
                    temp = streamReader.ReadLine().ToString();
                    this.text.Text = text.Text+"    " + temp + "\n"+"\n";
                    
                }

            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();



        }


        //功能函数
        //==================================================================================================================================================

        private void Init()
        {
            List1AddHead();
            MySqlConnection con = Connect.Connection();
            MySqlDataReader reader;
            MySqlCommand command;
            string cmd = "select * from book;";
            try
            {
                con.Open();
                command = new MySqlCommand(cmd, con);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string bookname = reader["BookName"].ToString().Trim();
                    string writer = reader["Writer"].ToString().Trim();
                    bookname = Formatting(bookname, 15);
                    writer = Formatting(writer, 15);
                    string str = bookname + writer;
                    this.list1.Items.Add(str);
                }
                reader.Close();
            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
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
        private void List1AddHead()
        {
            string h1 = "书名";
            string h2 = "作者";

            string h11 = Formatting(h1, 15);
            string h22 = Formatting(h2, 15);

            string head1 = h11 + h22;
            list1.Items.Add(head1);
        }

    }
}
