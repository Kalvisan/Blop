using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using Mono.Nat;
using System.Resources;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace blop
{
    public partial class Login : MetroForm
    {
        public static int id = 0;
        public static string myConnection = "Server=87.110.119.80;Database=Prakse;UID=root;Password=123123;";
        public static List<string> friendsName = new List<string>();
        public static List<string> friendsIP = new List<string>();
        public static List<string> friendsPicture = new List<string>();
        public static List<int> friendsStatus = new List<int>();
        public static List<string> friendsSeen = new List<string>();
        public static List<string> friendFriends = new List<string>();
        public static List<string>[] friendsPendingsName = new List<string>[] { new List<string>(), new List<string>()};
        public static string[] myPending;
        bool reg_visible = false;

        public Login()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Blop is already running");
                this.Close();
                return;
            }
            InitializeComponent();
            this.KeyPress += (s, e) => this.tb_userName.Text += e.KeyChar.ToString();
            this.Shown += (s, e) => this.Activate();
            panel2.Left = this.Width + 5;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public static void RefreshLists()
        {
            friendsIP.Clear();
            friendsName.Clear();
            friendsPicture.Clear();
            friendsStatus.Clear(); 
            friendsSeen.Clear();
            friendsPendingsName.First().Clear();
            friendsPendingsName.Last().Clear();
            friendFriends.Clear();
        }

        public static void ClearLists()
        {
            RefreshLists();

            try
            {
                MySqlConnection connection = new MySqlConnection(Login.myConnection);
                connection.Open();
                MySqlCommand cmd2 = new MySqlCommand("SELECT username,pending FROM Users;", connection);
                MySqlDataReader read2 = cmd2.ExecuteReader();
                while (read2.Read())
                    if (read2.GetString("username") == Main.userName)
                        myPending = read2.GetString("pending").Split(',');
                connection.Close();

                MySqlConnection connection2 = new MySqlConnection(Login.myConnection);
                connection2.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users;", connection2);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    foreach (string st in GetFrends())
                        if (read.GetInt32("id") == Convert.ToInt32(st))
                        {
                            friendsIP.Add(read.GetString("ip"));
                            friendsStatus.Add(read.GetInt16("online"));
                            friendsName.Add(read.GetString("username"));
                            friendsPicture.Add(read.GetString("picture"));
                            friendsSeen.Add(read.GetString("lasttime"));
                        }

                    foreach (string st in myPending)
                        if(read.GetInt32("id") == Convert.ToInt32(st)) {
                            friendsPendingsName.First().Add(read.GetString("username"));
                            friendsPendingsName.Last().Add(read.GetString("id"));
                            friendFriends.Add(read.GetString("friends"));
                        }
                }
                connection2.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static string myfriends;

        private static string[] GetFrends()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE username='" + Main.userName + "';", connection);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    if (read.GetString("friends") != "")
                    {
                        myfriends = read.GetString("friends");
                        return read.GetString("friends").Split(',');
                    }
                    else
                    {
                        return null;
                    }
                }
                connection.Close();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public void fillLists(string[] s)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(Login.myConnection);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users;", connection);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    foreach(string st in s)
                    if (read.GetInt32("id") == Convert.ToInt32(st))
                    {
                        friendsIP.Add(read.GetString("ip"));
                        friendsStatus.Add(read.GetInt16("online"));
                        friendsName.Add(read.GetString("username"));
                        friendsPicture.Add(read.GetString("picture"));
                        friendsSeen.Add(read.GetString("lasttime"));
                    }

                    foreach (string st in myPending)
                        if (read.GetInt32("id") == Convert.ToInt32(st))
                        {
                            friendsPendingsName.First().Add(read.GetString("username"));
                            friendsPendingsName.Last().Add(read.GetString("id"));
                            friendFriends.Add(read.GetString("friends"));
                        }
                }
                connection.Close();
            }
            catch
            {
                AlertShow("Connection failed", Color.IndianRed);

            }
        }

        public static string CreateHD5(string s)
        {
            MD5 md5 = MD5.Create();
            byte[] input = System.Text.Encoding.UTF8.GetBytes(s);
            byte[] bytes = md5.ComputeHash(input);

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                str.Append(bytes[i].ToString("X2"));
            }
            return str.ToString();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Loading.Visible = true;
            Loading.Enabled = true;
            linkLabel1.Enabled = false;
            linkLabel1.Visible = false;
            timer = 0;
            timer2.Enabled = true;

            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE username='" + tb_userName.Text + "';", connection);
                MySqlDataReader read = cmd.ExecuteReader();
                int count = 0;
                while (read.Read())
                {
                    if (read.GetString("password") == CreateHD5(tb_password.Text))
                    {
                        id = read.GetInt32("id");
                        myPending = read.GetString("pending").Split(',');
                        myfriends = read.GetString("friends");

                        if (read.GetString("friends") != "0")
                        {
                            fillLists(read.GetString("friends").Split(','));
                        }
                        Main.picture = read.GetString("picture");

                        Main.userName = tb_userName.Text;
                        UpdateDB(true);
                        Main m = new Main();
                        this.Visible = false;
                        m.ShowDialog();
                        m.Focus();
                        AlertShow("Connected", Color.Green);
                        count++;
                        break;
                    }
                }
                connection.Close();
                if (count == 0) AlertShow("The username or password is incorrect", Color.IndianRed);
            }
            catch
            {
                AlertShow("Connection failed", Color.IndianRed);
            }
        }

        public void RegistrationPanel()
        {
            if (!reg_visible)
            {
                reg_visible = true;
                reg_userName.Focus();
            }
            else
            {
                reg_visible = false;
                tb_userName.Focus();
            }
            panel2.Top = 25;
            panel2.BackColor = Color.FromArgb(124, 65, 153);
            timer3.Enabled = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (reg_visible)
            {
                if (panel2.Left > 0)
                    panel2.Left -= 20;
                else timer3.Enabled = false;
            }
            else
                if (panel2.Left < this.Width)
                    panel2.Left += 20;
                else timer3.Enabled = false;
        }

        public void AlertShow(string s, Color c)
        {
            panel1.Top = this.Height;
            LB_Alert.Text = s;
            panel1.BackColor = c;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Top > this.Height - panel1.Height)
                panel1.Top -= 1;
            else timer1.Enabled = false;
        }

        private void EnterPressButton(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                metroButton1_Click(metroButton1, EventArgs.Empty);
        }

        private void tb_userName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateDB(false);
            Main m = new Main();
            m.notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void LB_Alert_Click(object sender, EventArgs e)
        {
            panel1.Top = this.Height;
        }

        private void MouseEnterEvent(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            string name = p.Tag.ToString();
            ResourceManager rm = Properties.Resources.ResourceManager;
            Bitmap im = (Bitmap)rm.GetObject(name + "_focused");
            p.Tag += "_focused";
            p.Image = im;
        }

        private void MouseLeaveEvent(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            string name = p.Tag.ToString().Remove(p.Tag.ToString().Length - 8);
            ResourceManager rm = Properties.Resources.ResourceManager;
            Bitmap im = (Bitmap)rm.GetObject(name);
            p.Tag = name;
            p.Image = im;
        }

        int timer = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer++;
            if (timer >= 5)
            {
                Loading.Visible = false;
                linkLabel1.Enabled = true;
                linkLabel1.Visible = true;
                timer2.Enabled = false;
            }
        }

        public void UpdateDB(bool isOnline)
        {
            string online = "1";
    
            if (!isOnline) online = "0";
            WebClient webClient = new WebClient();
            string ip = webClient.DownloadString("http://icanhazip.com/");
            try
            {
                string query = "UPDATE Users SET online=" + online + ",ip='" + ip + "'  WHERE id = " + id.ToString() + ";";
                DateTime theDate = DateTime.UtcNow;
                if (!isOnline) query = "UPDATE Users SET online=" + online + ",ip='" + ip + "',lasttime='" + theDate.ToString("d").Replace('.', '/') + "'  WHERE id = " + id.ToString() + ";";
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                AlertShow("Connection failed", Color.IndianRed);
            }

        }

        private void Username_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationPanel();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RegistrationPanel();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (reg_password.Text != reg_password2.Text)
            {
                AlertShow("Passwords doesn't match", Color.IndianRed);
                reg_password.Clear();
                reg_password2.Clear();
                return;
            }
            if ((!string.IsNullOrEmpty(reg_userName.Text)) && (!string.IsNullOrEmpty(reg_password.Text))
                && (!string.IsNullOrEmpty(reg_password2.Text)))
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    connection.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT username FROM Users;", connection);
                    MySqlDataReader read = cmd1.ExecuteReader();
                    bool userNameExists = false;
                    while (read.Read())
                        if (reg_userName.Text.ToLower() == read.GetString("username").ToLower())
                        {
                            userNameExists = true;
                            AlertShow("The username is already in use", Color.IndianRed);
                        }
                    if (userNameExists)
                        return;
                    read.Close();
                    DateTime theDate = DateTime.UtcNow;
                    WebClient webClient = new WebClient();
                    string ip = webClient.DownloadString("http://icanhazip.com/");
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Users (username,password,picture,lasttime,ip) VALUES ('"+reg_userName.Text+"','"+CreateHD5(reg_password.Text)+"','"+reg_picturelink.Text+"','"+theDate.ToString("d").Replace('.','/')+"','"+ip+"');", connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    AlertShow("Signed up succesfully", Color.LightGreen);
                    RegistrationPanel();
                    regClear();
                }
                catch
                {
                    AlertShow("Connection failed", Color.IndianRed);
                }
            }
            else
                AlertShow("Please enter valid information", Color.IndianRed);
        }

        private void regClear()
        {
            reg_userName.Clear();
            reg_password.Clear();
            reg_password2.Clear();
            reg_picturelink.Clear();
        }

        private void reg_userName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}