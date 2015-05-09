using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using MetroFramework.Forms;
using System.Threading;
using Mono.Nat;
using System.Resources;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace blop
{
    public partial class Main : MetroForm
    {
        delegate void AddMessage(string message);
        public static string userName = "User", picture = "NULL";
        static int MessagePort = 2123, CallPort = 2125, CallReceivePort = 2126;
        public static string broadcastAddress = "10.0.0.210", myip = "127.0.0.1";
        UdpClient receivingClient;
        UdpClient sendingClient;
        Thread receivingThread;
        string filter;
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        
        public Main()
        {
            InitializeComponent();
            this.KeyPress += (s, e) => this.tbSend.Text += e.KeyChar.ToString();
            this.Shown += (s, e) => this.Activate();
            myip = GetLocalIP();
            label1.Text = userName;
            p_User.ImageLocation = picture;

        }


        int idControls;

        private void Main_Load(object sender, EventArgs e)
        {
            idControls = 0;
            for (idControls = 0; idControls < Login.friendsName.Count; idControls++)
                FillFrends(idControls, false);
            for (int j = idControls; j < Login.friendsPendingsName.First().Count + idControls; j++)
                FillFrends(j, true);
            tbSend.Focus();
            NatUtility.StartDiscovery();
            NatUtility.DeviceFound += DeviceFound;
            panel3.BackColor = Color.FromArgb(124, 65, 153);
            FriendsAddPanel.BackColor = Color.FromArgb(124, 65, 153);
            panel6.BackColor = Color.FromArgb(124, 65, 153);
            InitializeReceiver();
            metroPanel1.Enabled = false;
            FriendsAddPanel.Visible = false;
            FriendsAddPanel.Enabled = false;
        }

        public void FillFrends(int id, bool pender)
        {
            Panel p = new Panel();
            Label l = new Label();

            p.Width = 190;
            p.Height = 21;

            if (pender == false)
            {
                l.Tag = id.ToString();
                p.Tag = id.ToString();
            }
            else
            {
                l.Tag = (id).ToString();
                p.Tag = (id).ToString();
            }

            if (pender == false)
            {
                l.Click += new EventHandler(FriendsEvent);
                p.Click += new EventHandler(FriendsEvent);
            }
            else
            {
                l.Click += new EventHandler(PendingEvent);
                p.Click += new EventHandler(PendingEvent);
            }

            l.AutoSize = true;
            if (pender == false) {
                if (Login.friendsStatus[id] == 0)
                    l.Text = Login.friendsName[id] + " - " + Login.friendsSeen[id];
                else
                    l.Text = Login.friendsName[id];
            }
            else
            {
                l.Text = Login.friendsPendingsName.First()[id - idControls] + " (Pending friend request)";
                l.ForeColor = Color.Gray;
            }

            p.Location = new Point(3, 3 + (p.Height * id));
            p.Controls.Add(l);

            l.Location = new Point(22, 1);

            if (pender == false)
            {
                PictureBox pic = new PictureBox();
                pic.Width = 19;
                pic.Height = 19;

                pic.Tag = id.ToString();
                pic.Click += new EventHandler(FriendsEvent);
                if (Login.friendsStatus[id] == 1)
                    pic.Image = Properties.Resources.contact_online;
                else
                    pic.Image = Properties.Resources.contact_offline;

                p.Controls.Add(pic);

                pic.Location = new Point(1, 0);
            }

            panel1.Controls.Add(p);
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

        private void DeviceLost(object sender, DeviceEventArgs args)
        {
            INatDevice device = args.Device;
            device.DeletePortMap(new Mapping(Protocol.Udp, MessagePort, MessagePort));
            device.DeletePortMap(new Mapping(Protocol.Udp, CallPort, CallPort));
            device.DeletePortMap(new Mapping(Protocol.Udp, CallReceivePort, CallReceivePort));
        }

        private void DeviceFound(object sender, DeviceEventArgs args)
        {
            INatDevice device = args.Device;
            device.CreatePortMap(new Mapping(Protocol.Udp, MessagePort, MessagePort));
            device.CreatePortMap(new Mapping(Protocol.Udp, CallPort, CallPort));
            device.CreatePortMap(new Mapping(Protocol.Udp, CallReceivePort, CallReceivePort));
        }

        private string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            return "127.0.0.1";
        }

        private void InitializeReceiver()
        {
            receivingClient = new UdpClient(MessagePort);

            ThreadStart start = new ThreadStart(Receiver);
            receivingThread = new Thread(start);
            receivingThread.IsBackground = true;
            receivingThread.Start();

        }

        void btnSend_Click(object sender, EventArgs e)
        {
            tbSend.Text = tbSend.Text.TrimEnd();

            if (!string.IsNullOrEmpty(tbSend.Text))
            {
                sendingClient = new UdpClient("10.0.0.136", MessagePort);
                sendingClient.EnableBroadcast = true;

                string toSend = "[" + DateTime.Now.ToShortTimeString() + "] - " + userName + ":\r\n" + tbSend.Text;
                byte[] data = Encoding.UTF8.GetBytes(toSend);
                sendingClient.Send(data, data.Length);
                filter = "send";
                MessageReceived("[" + DateTime.Now.ToShortTimeString() + "] - " + "You:" + "\r\n" + tbSend.Text);
                tbSend.Text = "";
            }
            tbSend.Focus();
        }

        private void Receiver()
        {
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, MessagePort);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            AddMessage messageDelegate = MessageReceived;
            //if (!Directory.Exists("cache"))
            //{
            //    Directory.CreateDirectory("cache");
            //}
            //FileStream fs = new FileStream("cache/test.txt", FileMode.Create, FileAccess.Write);

            while (true)
            {
                //byte[] data = receivingClient.Receive(ref endPoint);
                if (isClosing)
                {

                    byte[] data = receivingClient.Receive(ref endPoint);
                    //fs.Write(data2, 0, data2.Length);
                    string message = Encoding.UTF8.GetString(data);
                    Invoke(messageDelegate, message);
                }
                else
                {
                    ;
                }
            }
        }

        private void MessageReceived(string message)
        {
            string senderName = "";
            char part = '*';
            int x = 0;

            while (part != '-')
            {
                part = message[x];
                x++;
            }
            x++;
            while (message[x] != ':')
            {
                senderName += message[x];
                x++;
            }
            if ((senderName == Friend_name.Text) || (filter == "send"))
                metroPanel1.Text += "\r\n" + message + "\r\n";
            string path;
            if (filter != "send")
                path = @"cache/" + senderName + ".txt";
            else
                path = @"cache/" + Friend_name.Text + ".txt";

            if (!Directory.Exists("cache"))
            {
                Directory.CreateDirectory("cache");
            }

            using (StreamWriter write = File.AppendText(path))
            {
                write.Write("\r\n" + message + "\r\n");
            }

            metroPanel1.SelectionStart = metroPanel1.Text.Length;
            metroPanel1.ScrollToCaret();
            metroPanel1.Refresh();
            filter = "receive";
        }

        private void tbSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnSend_Click(btnSend, EventArgs.Empty);
                tbSend.Focus();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (loggingOff)
            //Application.Exit();
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    this.WindowState = FormWindowState.Minimized;
            //    return;
            //}
        }

        public void AlertShow(string s, Color c)
        {
            Alert_panel.Top = -Alert_panel.Height;
            Alet_LB.Text = s;
            Alert_panel.BackColor = c;
            timer1.Enabled = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Alert_panel.Top = -Alert_panel.Height;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Alert_panel.Top < Alert_panel.Height - 23) Alert_panel.Top += 1;
            else timer1.Enabled = false;
        }

        private void FriendsEvent(Object sender, EventArgs e)
        {
            metroPanel1.Enabled = true;
            Control c = sender as Control;
            Friend_picture.Image = Properties.Resources.profile_default;
            Friend_call.Visible = true;
            pictureBox7.Enabled = true;
            tbSend.Enabled = true;
            btnSend.Enabled = true;
            Friend_call.Enabled = true;
            Friend_name.Visible = true;
            Friend_picture.Visible = true;
            Friend_pictureborder.Visible = true;
            Friend_status.Visible = true;

            int id = Convert.ToInt32(c.Tag.ToString());
            Friend_name.Text = Login.friendsName[id];

            metroPanel1.Clear();
            string path = @"cache/" + Friend_name.Text + ".txt";
            if (File.Exists(path))
                metroPanel1.Text = File.ReadAllText(path);

            Friend_picture.ImageLocation = Login.friendsPicture[id];
            if (Login.friendsStatus[id] == 1)
            {
                Friend_pictureborder.Image = Properties.Resources.border_online;
                Friend_status.Text = "Online";
            }
            else
            {
                Friend_pictureborder.Image = Properties.Resources.border_offline;
                Friend_status.Text = "Offline";
            }
            broadcastAddress = Login.friendsIP[id];

            metroPanel1.SelectionStart = metroPanel1.Text.Length;
            metroPanel1.ScrollToCaret();
            metroPanel1.Refresh();

            foreach (Control k in panel1.Controls)
            {
                if (k.Tag.ToString() == id.ToString())
                {
                    k.BackColor = Color.FromArgb(198, 172, 211);
                }
                else
                    k.BackColor = Color.White;
            }
            HideCaret(metroPanel1.Handle);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.ShowDialog();

            //sendingClient = new UdpClient("192.168.43.170", FilePort);
            //sendingClient.EnableBroadcast = true;

            //FileStream fs = new FileStream(f.FileName, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);
            //byte[] data = br.ReadBytes(Convert.ToInt32(fs.Length));
            //sendingClient.Send(data, data.Length);
        }

        private void Friend_call_Click(object sender, EventArgs e)
        {
            CallChat c = new CallChat();
            c.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            ThreadStart start = new ThreadStart(RefresThread);
            receivingThread = new Thread(start);
            receivingThread.IsBackground = true;
            receivingThread.Start();

        }

        private delegate void DELEGATE();

        private void RefresThread()
        {
            Delegate del = new DELEGATE(ThreadIsShit);
            this.Invoke(del);
        }

        private void ThreadIsShit()
        {
            Login.ClearLists();
            panel1.Controls.Clear();
            idControls = 0;
            for (idControls = 0; idControls < Login.friendsName.Count; idControls++)
                FillFrends(idControls, false);
            for (int j = idControls; j < Login.friendsPendingsName.First().Count + idControls; j++)
                FillFrends(j, true);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FriendsAddPanel.Enabled = true;
            FriendsAddPanel.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FriendsAddPanel.Enabled = false;
            FriendsAddPanel.Visible = false;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FriendsAddPanel.Enabled = false;
            FriendsAddPanel.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            foreach (string s in Login.friendsName)
                if (s == textBox1.Text.ToLower())
                {
                    textBox1.Text = "";
                    AlertShow("Friend already added", Color.IndianRed);
                    return;
                }

            int friendID = 0;
            string friendPendings = "0";

            try
            {
                MySqlConnection connection = new MySqlConnection(Login.myConnection);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT username,id,pending FROM Users;", connection);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    if (textBox1.Text.ToLower() == read.GetString("username").ToLower())
                    {
                        string[] pendings = read.GetString("pending").Split(',');
                        foreach (string s in pendings)
                            if (Login.id.ToString() == s)
                            {
                                AlertShow("Already pending", Color.IndianRed);
                                return;
                            }

                        friendID = read.GetInt32("id");
                        friendPendings = read.GetString("pending");
                    }
                }
                connection.Close();


                MySqlConnection connection2 = new MySqlConnection(Login.myConnection);
                connection2.Open();
                MySqlCommand cmd2 = new MySqlCommand("UPDATE Users SET pending='" + friendPendings + "," + Login.id + "'  WHERE id = " + friendID + ";", connection2);
                cmd2.ExecuteNonQuery();
                connection2.Close();
                AlertShow("Invite sent", Color.LightGreen);
            }
            catch
            {
                AlertShow("Connection failed", Color.IndianRed);
            }

            textBox1.Text = "";
            FriendsAddPanel.Enabled = false;
            FriendsAddPanel.Visible = false;
        }

        bool isClosing = true;
        private void btnLogOff_Click(object sender, EventArgs e)
        {
            this.Close();
            isClosing = false;
            receivingThread.Abort();
            receivingClient.Close();
            Login l = new Login();
            l.Visible = true;
            notifyIcon1.Visible = false;
            Login.RefreshLists();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        int idPender = 0;

        private void PendingEvent(Object sender, EventArgs e)
        {
            Control c = sender as Control;

            int id = Convert.ToInt32(c.Tag.ToString()) - idControls;

            label3.Text = "Would you like to add \n" + Login.friendsPendingsName.First()[id] + " to your contacts?";

            idPender = id;

            panel6.Visible = true;
            panel6.Enabled = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            panel6.Enabled = false;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            // yes
            try
            {
                string pendingg = "0";
                foreach (string s in Login.myPending)
                    if (s != Login.friendsPendingsName.Last()[idPender] && s != "0")
                        pendingg += "," + s;

                string myfriends = Login.myfriends + "," + Login.friendsPendingsName.Last()[idPender];
                string query = "UPDATE Users SET friends='" + myfriends + "',pending='" + pendingg + "'  WHERE id = " + Login.id.ToString() + ";";
                MySqlConnection connection = new MySqlConnection(Login.myConnection);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();

                MySqlConnection connection2 = new MySqlConnection(Login.myConnection);
                connection2.Open();
                string query2 = "UPDATE Users SET friends='" + Login.friendFriends[idPender] + "," + Login.id.ToString() + "' WHERE id = " + Login.friendsPendingsName.Last()[idPender] + ";";
                MySqlCommand cmd2 = new MySqlCommand(query2, connection2);
                cmd2.ExecuteNonQuery();
                connection2.Close();

                AlertShow("Friend request accepted", Color.LightGreen);
                panel6.Visible = false;
                panel6.Enabled = false;
            }
            catch
            {
                AlertShow("Connection failed", Color.IndianRed);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // no
            try
            {
                string pendingg = "0";
                foreach (string s in Login.myPending)
                    if (s != Login.friendsPendingsName.Last()[idPender] && s != "0")
                        pendingg += "," + s;

                string query = "UPDATE Users SET pending='" + pendingg + "'  WHERE id = " + Login.id.ToString() + ";";
                MySqlConnection connection = new MySqlConnection(Login.myConnection);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();

                AlertShow("Friend request declined", Color.LightGreen);
                panel6.Visible = false;
                panel6.Enabled = false;
            }
            catch
            {
                AlertShow("Connection failed", Color.IndianRed);
            }

            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Enabled = true;
            this.CenterToScreen();
        }

    }
}
