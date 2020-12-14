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
using Microsoft.Win32;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;

namespace LibraryManageSystem
{
    /// <summary>
    /// UserMessage.xaml 的交互逻辑
    /// </summary>
    public partial class UserMessage : Page
    {

        private string[] Havechanged = new string[6];
        private int ChangeNum;
        public UserMessage()
        {
            InitializeComponent();
            Init();
            SetEnabledFalse();
            SetValues();
        }
        //点击事件
        //==============================================================================================================================

        private void SelectPic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();//用来打开图片选择窗口
            openFile.Filter = "JPEG|*.jpg";
            openFile.DefaultExt = "jpg";
            openFile.Multiselect = false;//不能多选
           
            
            Image image = new Image();//将原本image的source储存起来;
            image.Source = this.Picture.Source;
            this.Picture.Source = null;//设置image的源文件为空;

            if (openFile.ShowDialog() == true)
            {
                
                Computer computer = new Computer();//computer类，先引入microsoft.visualBasic,再using device;
                string path = "d:/libraryresource/Userpicture/";
                bool ishave = false;//用来标记用户之前是否设置过图片;

                //获取原图片文件
                MySqlConnection con = Connect.Connection();
                string cmd = "select * from usermessage where UserId = '" + Connect.GetUser() + "';";
                MySqlCommand command = new MySqlCommand(cmd, con);
                MySqlDataReader reader;


                con.Open();
                reader = command.ExecuteReader();
                reader.Read();
                if (reader["Picture"].ToString() != null && reader["Picture"].ToString().Trim() != "")
                {
                    string pic = reader["Picture"].ToString().Trim();
                    computer.FileSystem.RenameFile(path + pic, "midpic.jpg");//把原文件名设置为   midpic.jpg
                    ishave = true;
                }
                con.Close();
                reader.Close();



                string type = System.IO.Path.GetExtension(openFile.FileName);
                string newname = Connect.GetUser() + type;//设置图片新的名字,以用户名命名

                System.IO.File.Copy(openFile.FileName, System.IO.Path.Combine(@path, System.IO.Path.GetFileName(openFile.FileName)));
                string oldname = System.IO.Path.GetFileName(openFile.FileName);

                ////string oldpath = path + openFile.FileName;
                ////File.Move(oldpath, path + name);
                string oldnamepath = path + oldname;

                if (oldname != newname)
                {
                    computer.FileSystem.RenameFile(oldnamepath, newname);
                }
                if(ishave == true)//如果之前有记录，不需要更新，图片的名字不变；如果没有，则向数据库插入对应的信息。
                {
                    computer.FileSystem.DeleteFile(path + "midpic.jpg");
                }
                else
                {
                    con.Open();
                    command.CommandText = "update usermessage set Picture = '" + newname + "' where UserId = '" + Connect.GetUser() + "';";
                    command.ExecuteNonQuery();
                    con.Close();
                }

                //pack://SiteOfOrigin:,,,/

                setImageSource("d:/libraryresource/Userpicture/" + newname);
                return;
            }
            else
            {

                this.Picture.Source = image.Source;
                return;
            }
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            this.NickName.IsEnabled = true;
            this.Age.IsEnabled = true;
            this.College.IsEnabled = true;
            this.Address.IsEnabled = true;
            this.Motto.IsEnabled = true;
            this.Introduce.IsEnabled = true;
            this.Picture.IsEnabled = true;

            this.SelectPic.IsEnabled = true;//按钮设置为可用
            this.Submit.IsEnabled = true;
            this.Change.IsEnabled = false;

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledFalse();
            MySqlConnection con = Connect.Connection();
            MySqlCommand command;
            TextBox textBox;
            string mark="";
            while (true)
            {
                if(ChangeNum == 0)
                {
                    MessageBox.Show("更新完成！");
                    break;
                }
                con.Open();
                for(int i = 0; i < 6; i++)
                {
                    if (Havechanged[i] != null && Havechanged[i] != "")
                    {
                        mark = Havechanged[i];
                        Havechanged[i] = null;
                        break;
                    }
                }
                textBox = this.FindName(mark) as TextBox;
                string cmd = "update usermessage set " + textBox.Name.Trim() + " = '" + textBox.Text.ToString().Trim() + "' where UserId = '" + Connect.GetUser() + "';";
                command = new MySqlCommand(cmd, con);
                command.ExecuteNonQuery();
                con.Close();
                ChangeNum--;

            }
            this.Change.IsEnabled = true;
            Havechanged = new string[6];
            ChangeNum = 0;
            SetValues();
        }


        //功能函数
        //=======================================================================================================================================
        private void Init()
        {
            MySqlConnection con = Connect.Connection();
            string cmd = "select * from UserMessage where UserId = '" + Connect.GetUser() + "';";
            MySqlCommand command = new MySqlCommand(cmd, con);
            MySqlDataReader reader;
            bool isHave = true;
            try
            {
                con.Open();
                reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    isHave = false;
                }

                reader.Close();
                
                if (isHave)
                {
                    command.CommandText = "insert into UserMessage (UserId) values('" + Connect.GetUser() + "');";
                    command.ExecuteNonQuery();
                }
                

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + "Init");
            }
            con.Close();
            return;
        }//检查是否有有关该用户的信息记录

        private void SetValues()
        {
            DeleteTextChange();
            string picturePath = "d:/LibraryResource/UserPicture/";//pack://SiteOfOrigin:,,,/
            MySqlConnection con = Connect.Connection();
            string cmd = string.Format("select * from usermessage where UserId = '{0}';", Connect.GetUser().ToString());
            
            MySqlDataReader reader;
            try
            {
                con.Open();
                MySqlCommand command = new MySqlCommand(cmd, con);
                reader = command.ExecuteReader();


                reader.Read();
                this.NickName.Text = reader["NickName"].ToString();
                this.Age.Text = reader["Age"].ToString();
                this.College.Text = reader["College"].ToString();
                this.Address.Text = reader["Address"].ToString();
                this.Motto.Text = reader["Motto"].ToString();
                this.Introduce.Text = reader["Introduce"].ToString();

                string pictureName = reader["Picture"].ToString().Trim();//图片设置
                if (pictureName != "" && pictureName != null)
                {
                    picturePath += pictureName;
                    //this.Picture.Source = new BitmapImage(new Uri(picturePath, UriKind.Absolute));
                    setImageSource(picturePath);
                } else
                {
                    this.Picture.Source = new BitmapImage(new Uri("pack://SiteOfOrigin:,,,/d:/libraryresource/Userpicture/defaultpic.jpg", UriKind.Absolute));
                    }

                reader.Close();
            } catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + "setvalues");
            }
            con.Close();
            AddTextChange();
            return;
        }//设置所有文本框中的信息

        private void SetEnabledFalse()
        {
            this.NickName.IsEnabled = false;
            this.Age.IsEnabled = false;
            this.College.IsEnabled = false;
            this.Address.IsEnabled = false;
            this.Motto.IsEnabled = false;
            this.Introduce.IsEnabled = false;
            this.Picture.IsEnabled = false;

            this.SelectPic.IsEnabled = false;//按钮设置为不可用
            this.Submit.IsEnabled = false;

        }


        private void TextChange(object sender, TextChangedEventArgs e)
        {
            TextBox text = sender as TextBox;
            string name = text.Name.Trim();
            bool ischanged = false;
            foreach(string s in Havechanged)
            {
                if(s == name)
                {
                    ischanged = true;
                }
            }
            if (!ischanged)
            {
                for(int i = 0; i < 6; i++)
                {
                    if (Havechanged[i] == null || Havechanged[i] == "")
                    {
                        Havechanged[i] = name;
                        ChangeNum++;
                        
                        break;
                    }
                }
            }
            //foreach(string s in Havechanged)
            //{
            //    if(s != "" && s != null)
            //    {
            //        MessageBox.Show(s);
            //    }
            //}
        }//检测哪些行有变化

        private void DeleteTextChange()
        {
            this.NickName.TextChanged -= TextChange;
            this.Age.TextChanged -= TextChange;
            this.College.TextChanged -= TextChange;
            this.Address.TextChanged -= TextChange;
            this.Motto.TextChanged -= TextChange;
            this.Introduce.TextChanged -= TextChange;
        }

        private void AddTextChange()
        {
            this.NickName.TextChanged += TextChange;
            this.Age.TextChanged += TextChange;
            this.College.TextChanged += TextChange;
            this.Address.TextChanged += TextChange;
            this.Motto.TextChanged += TextChange;
            this.Introduce.TextChanged += TextChange;
        }

        private void setImageSource(string filePath)//image 没有dispose方法，即使设置为NULL也无法将文件释放，造成目标文件一直被占用。通过流将图片二进制赋给bitmapimage，然后关闭流来达到解除占用的目的。
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))

            {
                FileInfo fi = new FileInfo(filePath);
                byte[] bytes = reader.ReadBytes((int)fi.Length);
                reader.Close();




                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(bytes);
                bitmapImage.EndInit();


                this.Picture.Source = bitmapImage;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

            }
        }

    }
}
