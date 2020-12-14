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
    /// SeatBook.xaml 的交互逻辑
    /// </summary>
    public partial class SeatBook : Page
    {
        public SeatBook()
        {
            InitializeComponent();
            ButtonBrush();
        }
      
        //点击事件区域
        //===================================================================================================================
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if(button.Background == System.Windows.Media.Brushes.Red)
            {
                MessageBox.Show("该座位已被预约！");
                return;
            }
            if (!CanBook())
            {
                MessageBox.Show("你已预约另一位置，不能再次预约！");
                return;
            }
            string name = button.Name;
            string[] array = name.Split('_');
            string row = array[1];
            string column = array[2];
            MySqlConnection con = Connect.Connection();
            MySqlDataReader reader;
            MySqlCommand command;
            try
            {
                con.Open();
                string cmd = "insert into seatbook values('" + row + "','" + column + "','" + Connect.GetUser() + "');";

                command = new MySqlCommand(cmd, con);
                command.ExecuteNonQuery();
               
                cmd = "select * from seatbook where x = '" + row + "' and y = '" + column + "'and UserId = '"+Connect.GetUser()+"';";
                command = new MySqlCommand(cmd, con);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("预约成功！");
                }
                reader.Close();

            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();

            ButtonBrush();
        }


        private void Cancel(object sender, MouseButtonEventArgs e)//右键点击取消
        {
            var button = sender as Button;
            if (button.Background == System.Windows.Media.Brushes.Green)
            {
                return;
            }
            if (CanBook())
            {
                MessageBox.Show("你无权取消该位置的预约！");
                return;
            }
            string name = button.Name;
            string[] array = name.Split('_');
            string row = array[1];
            string column = array[2];

            bool mark = false;
            MySqlConnection con = Connect.Connection();
            MySqlDataReader reader;
            MySqlCommand command;
            try
            {
                con.Open();
                string cmd = "select * from seatbook where x = '" + row + "' and y = '" + column + "'and UserId = '" + Connect.GetUser() + "';";
                command = new MySqlCommand(cmd, con);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    mark = true;
                }
                else
                {
                    MessageBox.Show("你无权取消该位置的预约！");
                }
                reader.Close();
                if (mark)
                {
                    cmd = "delete from seatbook where UserId = '" + Connect.GetUser() + "';";
                    command = new MySqlCommand(cmd, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("取消预约成功！");
                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
            ButtonBrush();

        }
        //功能函数区域
        //================================================================================================================
        private void ButtonBrush()
        {
            MySqlConnection con = Connect.Connection();
            MySqlDataReader reader;
            MySqlCommand command;
            try
            {
                con.Open();
                string cmd = "";

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        cmd = "select * from seatbook where x = '" + (i + 1) + "' and y = '" + (j + 1) + "';";
                        string name = "G_" + (i + 1) + "_" + (j + 1);
                        Button btn = this.FindName(name) as Button;

                        command = new MySqlCommand(cmd, con);
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            btn.Background = System.Windows.Media.Brushes.Red;
                        }
                        else
                        {
                            btn.Background = System.Windows.Media.Brushes.Green;
                        }
                        reader.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//绘制Button的背景色

        private bool CanBook()
        {
            MySqlConnection con = Connect.Connection();
            MySqlDataReader reader;
            MySqlCommand command;
            bool result = true;
            try
            {
                con.Open();
                string cmd = "select * from seatbook where UserId = '" + Connect.GetUser() + "';";
                command = new MySqlCommand(cmd, con);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result = false;
                }
                reader.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
            return result;
        }
   //类结尾=====================================================================================================================================        
    }
}
