namespace blop
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LB_Alert = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.metroButton1 = new System.Windows.Forms.PictureBox();
            this.Loading = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRegister = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reg_picturelink = new System.Windows.Forms.TextBox();
            this.reg_password2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.Label();
            this.reg_password = new System.Windows.Forms.TextBox();
            this.reg_userName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(37, 192);
            this.tb_userName.MaxLength = 16;
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(176, 20);
            this.tb_userName.TabIndex = 0;
            this.tb_userName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnterPressButton);
            this.tb_userName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_userName_KeyPress);
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(37, 235);
            this.tb_password.MaxLength = 60;
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '●';
            this.tb_password.Size = new System.Drawing.Size(176, 20);
            this.tb_password.TabIndex = 1;
            this.tb_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnterPressButton);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(37, 216);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(64, 19);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Password";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(37, 173);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(68, 19);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "Username";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.IndianRed;
            this.panel1.Controls.Add(this.LB_Alert);
            this.panel1.Location = new System.Drawing.Point(0, 384);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 28);
            this.panel1.TabIndex = 7;
            // 
            // LB_Alert
            // 
            this.LB_Alert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LB_Alert.Font = new System.Drawing.Font("Calibri Light", 11F);
            this.LB_Alert.Location = new System.Drawing.Point(-2, 4);
            this.LB_Alert.Name = "LB_Alert";
            this.LB_Alert.Size = new System.Drawing.Size(253, 22);
            this.LB_Alert.TabIndex = 8;
            this.LB_Alert.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LB_Alert.Click += new System.EventHandler(this.LB_Alert_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 3;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // metroButton1
            // 
            this.metroButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroButton1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("metroButton1.ErrorImage")));
            this.metroButton1.Image = ((System.Drawing.Image)(resources.GetObject("metroButton1.Image")));
            this.metroButton1.Location = new System.Drawing.Point(37, 282);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(176, 33);
            this.metroButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.metroButton1.TabIndex = 9;
            this.metroButton1.TabStop = false;
            this.metroButton1.Tag = "sign_in";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            this.metroButton1.MouseEnter += new System.EventHandler(this.MouseEnterEvent);
            this.metroButton1.MouseLeave += new System.EventHandler(this.MouseLeaveEvent);
            // 
            // Loading
            // 
            this.Loading.Image = global::blop.Properties.Resources.loading_new;
            this.Loading.Location = new System.Drawing.Point(87, 322);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(75, 15);
            this.Loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Loading.TabIndex = 8;
            this.Loading.TabStop = false;
            this.Loading.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(65, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(141, 335);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(76, 14);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "New Account";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // timer3
            // 
            this.timer3.Interval = 1;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Purple;
            this.panel2.Controls.Add(this.btnRegister);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.reg_picturelink);
            this.panel2.Controls.Add(this.reg_password2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Username);
            this.panel2.Controls.Add(this.reg_password);
            this.panel2.Controls.Add(this.reg_userName);
            this.panel2.Location = new System.Drawing.Point(219, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 368);
            this.panel2.TabIndex = 11;
            // 
            // btnRegister
            // 
            this.btnRegister.Image = global::blop.Properties.Resources.accept;
            this.btnRegister.Location = new System.Drawing.Point(42, 296);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(176, 25);
            this.btnRegister.TabIndex = 17;
            this.btnRegister.TabStop = false;
            this.btnRegister.Tag = "accept";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            this.btnRegister.MouseEnter += new System.EventHandler(this.MouseEnterEvent);
            this.btnRegister.MouseLeave += new System.EventHandler(this.MouseLeaveEvent);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(15, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 34);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "back";
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.MouseEnterEvent);
            this.pictureBox2.MouseLeave += new System.EventHandler(this.MouseLeaveEvent);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(39, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "Profile picture URL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(39, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "Repeat password";
            // 
            // reg_picturelink
            // 
            this.reg_picturelink.Location = new System.Drawing.Point(42, 226);
            this.reg_picturelink.Multiline = true;
            this.reg_picturelink.Name = "reg_picturelink";
            this.reg_picturelink.Size = new System.Drawing.Size(176, 60);
            this.reg_picturelink.TabIndex = 13;
            // 
            // reg_password2
            // 
            this.reg_password2.Location = new System.Drawing.Point(42, 178);
            this.reg_password2.MaxLength = 60;
            this.reg_password2.Name = "reg_password2";
            this.reg_password2.PasswordChar = '●';
            this.reg_password2.Size = new System.Drawing.Size(176, 20);
            this.reg_password2.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(39, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(39, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "Username";
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.ForeColor = System.Drawing.Color.White;
            this.Username.Location = new System.Drawing.Point(130, 29);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(73, 24);
            this.Username.TabIndex = 9;
            this.Username.Text = "Sign Up";
            this.Username.Click += new System.EventHandler(this.Username_Click);
            // 
            // reg_password
            // 
            this.reg_password.Location = new System.Drawing.Point(42, 129);
            this.reg_password.MaxLength = 60;
            this.reg_password.Name = "reg_password";
            this.reg_password.PasswordChar = '●';
            this.reg_password.Size = new System.Drawing.Size(176, 20);
            this.reg_password.TabIndex = 6;
            // 
            // reg_userName
            // 
            this.reg_userName.Location = new System.Drawing.Point(42, 81);
            this.reg_userName.MaxLength = 16;
            this.reg_userName.Name = "reg_userName";
            this.reg_userName.Size = new System.Drawing.Size(176, 20);
            this.reg_userName.TabIndex = 5;
            this.reg_userName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.reg_userName_KeyPress);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 382);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.Loading);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_userName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.TextBox tb_password;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LB_Alert;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox Loading;
        private System.Windows.Forms.PictureBox metroButton1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.TextBox reg_password;
        private System.Windows.Forms.TextBox reg_userName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox reg_picturelink;
        private System.Windows.Forms.TextBox reg_password2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox btnRegister;

    }
}

