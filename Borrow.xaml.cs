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
using System.Data;

namespace LibraryManageSystem
{
    /// <summary>
    /// Borrow.xaml 的交互逻辑
    /// </summary>
    public partial class Borrow : Page
    {

        static string[] HaveAddedName = new string[3];
        static string[] HaveAddedWri = new string[3];
        static int num = 0;

        public Borrow()
        {
            InitializeComponent();
            Show();
            List1AddHead();
            ArrayInit();
            inquery.IsDefault = true;
        }

        private void addBook_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem == null)
            {
                return;
            }

            if (num>2)
            {
                MessageBox.Show("已达到最大选择上限！");
                return;
            }

            if (list.SelectedIndex == 0 || list.SelectedIndex == 1)
            {
                MessageBox.Show("不能选择该行！");
                return;
            }
            

            bool m = true;
            string oringin = list.SelectedItem.ToString();
            string[] array = oringin.Split('|');
            string bookname = array[0].Trim();
            string writer = array[1].Trim();
            
            for(int i = 0; i < 3; i++)
            {
                if(HaveAddedName[i] == bookname && HaveAddedWri[i] == writer)
                {
                    m = false;
                }
            }

            if (m == false) { 
                MessageBox.Show("已选择该项！");
                return;
            }

            for(int i = 0; i < 3; i++)
            {
                if(HaveAddedName[i] == "" && HaveAddedWri[i] == "")
                {
                    HaveAddedName[i] = bookname;
                    HaveAddedWri[i] = writer;
                    break;
                }
            }
            if (!IsBorrowed(bookname, writer))
            {
                string str1 = Formatting(bookname, 15);
                string str2 = Formatting(writer, 15);
                string str = str1 + str2;
                list1.Items.Add(str);
                num++;
            }
            else
            {
                MessageBox.Show("该书已被借走！");
            }
        }//向list1中加入图书数据


        private void inquery_Click(object sender, RoutedEventArgs e)
        {
            string bookname = this.Bookname.Text.ToString().Trim();
            string writer = this.Writer.Text.ToString().Trim();
            string cmd = "";
            MySqlConnection con = Connect.Connection();
            if(bookname == "" &&writer == "")
            {
                MessageBox.Show("请输入内容！");
                return;

            }else if(bookname == "" && writer != "")
            {
                cmd = "select * from book where Writer like '%" + writer +"%";
            }
            else if(writer == "" && bookname != "")
            {
                cmd = "select * from book where BookName like '%" + bookname +"%";
            }
            else
            {
                cmd = "select * from book where BookName like '%" + bookname + "%' and Writer like '%" + writer+"%" ;
            }
            cmd = cmd + "';";
            try
            {
                con.Open();
                MySqlCommand command = new MySqlCommand(cmd, con);
                MySqlDataReader reader = command.ExecuteReader();
                list.Items.Clear();
                AddHead();
                string[] str = new string[5];
                while (reader.Read())
                {
                    str[0] = reader["BookName"].ToString();

                    str[1] = reader["Writer"].ToString();

                    str[2] = reader["Page"].ToString();

                    str[3] = reader["Public"].ToString();

                    str[4] = reader["DefaultPrice"].ToString();


                    string sep1 = Formatting(str[0], 20);
                    string sep2 = Formatting(str[1], 20);
                    string sep3 = Formatting(str[2], 20);
                    string sep4 = Formatting(str[3], 20);
                    string sep5 = Formatting(str[4], 20);

                    string strsub = sep1 + sep2 + sep3 + sep4 + sep5;

                    this.list.Items.Add(strsub);
                }
                reader.Close();

            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void borrow_Click(object sender, RoutedEventArgs e)
        {
            if (!IsHaveQual(num))
            {
                MessageBox.Show("可借阅数目超过最大上限！");
                return;
            }
            if (num == 0)
            {
                return;
            }
            
            
            string[] gather = new string[num]; 
            for(int i = 0; i < num; i++)
            {
                gather[i] = this.list1.Items[i+1].ToString();
            }
            for(int i = 0; i < num; i++)
            {
                string[] array = gather[i].Split('|');
                array[0] = array[0].Trim();
                array[1] = array[1].Trim();

                MySqlConnection con = Connect.Connection();
                try
                {
                    con.Open();
                    string cmd = "insert into borrow values ('" + array[0] + "','" + array[1] + "','" + Connect.GetUser()+"');";
                    MySqlCommand command = new MySqlCommand(cmd, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("借阅成功！");
                    //string check = "select * from borrow where BookName = '" + array[0] + "' and Writer = '" + array[1] + "' and UserId = '" + Connect.GetUser() + "';";
                    //command = new MySqlCommand(check, con);
                    //MySqlDataReader reader = command.ExecuteReader();
                    //if (reader.HasRows)
                    //{
                    //    MessageBox.Show("借阅成功！");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("借阅失败！");
                    //    return;
                    //}
                    //reader.Close();
                    con.Close();
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            list1.Items.Clear();
            List1AddHead();
            num = 0;
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if(this.list1.SelectedItem == null)
            {
                return;
            }

            int index = list1.SelectedIndex;
            string s = list1.SelectedItem.ToString().Trim();
            if (index == 0)
            {
                MessageBox.Show("该行不能删除！");
                return;
            }
            string[] array = s.Split('|');
            array[0] = array[0].Trim();
            array[1] = array[1].Trim();

            for (int i = 0; i < 3; i++)
            {
                if (array[0] == HaveAddedName[i] && array[1] == HaveAddedWri[i])
                {
                    HaveAddedName[i] = "";
                    HaveAddedWri[i] = "";
                    break;
                }
            }


            list1.Items.RemoveAt(index);
            num--;
        }

        private void showall_Click(object sender, RoutedEventArgs e)
        {
            list.Items.Clear();
            Show();
        }
        //功能函数区域

        //=====================================================================================================================

        //================================================================================================================================

        private bool IsBorrowed(string bookname, string writter)
        {
            MySqlConnection con = Connect.Connection();
            try
            {
                con.Open();
                string cmd = "select * from borrow where BookName = '" + bookname + "' and Writer = '" + writter + "';";
                MySqlCommand command = new MySqlCommand(cmd, con);
                MySqlDataReader reader = command.ExecuteReader();
                bool result;
                if (reader.HasRows)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                con.Close();
                reader.Close();
                return result;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }//判断是否被借走


        private void canBorrow_Click(object sender, RoutedEventArgs e)//筛选未被借走的书籍
        {
            int index = list.Items.Count - 1;

            MySqlConnection con = Connect.Connection();

            try
            {
                MySqlDataReader reader;
                con.Open();
                while (true)
                {
                    if (index < 2)
                    {
                        break;
                    }
                    string str = list.Items[index].ToString();
                    string[] arrary = str.Split('|');

                    string cmd = "select * from borrow where BookName = '" + arrary[0].Trim() + "' and Writer = '" + arrary[1].Trim() + "';";
                    MySqlCommand command = new MySqlCommand(cmd, con);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        list.Items.RemoveAt(index);
                        index--;
                    }
                    else
                    {
                        index--;
                    }
                    reader.Close();

                }

                con.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void List1AddHead()
        {
            string h1 = "书名";
            string h2 = "作者";

            string h11 = Formatting(h1, 15);
            string h22 = Formatting(h2, 15);

            string head1 = h11 + h22;
            list1.Items.Add(head1);
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

        private void Show()
        {
            MySqlConnection con = Connect.Connection();

            try
            {
                con.Open();
                string sel = "select * from book;";
                MySqlCommand command = new MySqlCommand(sel, con);
                MySqlDataReader reader = command.ExecuteReader();

                AddHead();

                while (reader.Read())
                {

                    string[] str = new string[5];
                    str[0] = reader["BookName"].ToString();

                    str[1] = reader["Writer"].ToString();

                    str[2] = reader["Page"].ToString();

                    str[3] = reader["Public"].ToString();

                    str[4] = reader["DefaultPrice"].ToString();


                    string sep1 = Formatting(str[0], 20);
                    string sep2 = Formatting(str[1], 20);
                    string sep3 = Formatting(str[2], 20);
                    string sep4 = Formatting(str[3], 20);
                    string sep5 = Formatting(str[4], 20);

                    string strsub = sep1 + sep2 + sep3 + sep4 + sep5;

                    this.list.Items.Add(strsub);
                }

                con.Close();
                reader.Close();



            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//listbox初始化，显示数据库数据

        private void AddHead()
        {
            string h1 = "书名";
            string h2 = "作者";
            string h3 = "页数";
            string h4 = "出版社";
            string h5 = "定价";

            h1 = Formatting(h1, 20);
            h2 = Formatting(h2, 20);
            h3 = Formatting(h3, 20);
            h4 = Formatting(h4, 20);
            h5 = Formatting(h5, 20);

            string head = h1 + h2 + h3 + h4 + h5;
            this.list.Items.Add(head);
            string separator = "===================================================================================================";
            this.list.Items.Add(separator);
        }//为列表加入表头和分隔线;
        private void ArrayInit()
        {
            for(int i = 0; i < 3; i++)
            {
                HaveAddedName[i] = "";
                HaveAddedWri[i] = "";
            }
        }

        private bool IsHaveQual(int num)
        {
            MySqlConnection con = Connect.Connection();
            bool result = false;
            string cmd = "select count(UserId) as 'cnt' from borrow where UserId = '" + Connect.GetUser() + "';";
            try
            {
                con.Open();
                MySqlCommand command = new MySqlCommand(cmd, con);
                MySqlDataReader reader = command.ExecuteReader();
                int i = 0;
                if (reader.Read())
                {
                     i = Convert.ToInt32(reader["cnt"].ToString());
                }
               
                if (i + num <= 3)
                {
                    result = true;
                }
              
                con.Close();
                reader.Close();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;

        }


        //private void Show()//测试函数
        //{
        //    string s1 = "两个|";
        //    string s2 = "0000|";
        //    string s3 = "··|";
        //    list.Items.Add(s1);
        //    list.Items.Add(s2);
        //    list.Items.Add(s3);
        //}
    }
}
