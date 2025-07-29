namespace TobiTube_Offline
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.VideoPage_Panel = new System.Windows.Forms.Panel();
            this.RandomFrame_Button = new System.Windows.Forms.Button();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.VideoBack = new System.Windows.Forms.Panel();
            this.VideoController = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.FullScreenVideo = new System.Windows.Forms.Button();
            this.StopVideoButton = new System.Windows.Forms.Button();
            this.PauseVideoButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.PlayVideoButton = new System.Windows.Forms.Button();
            this.vlcControl1 = new Vlc.DotNet.Forms.VlcControl();
            this.MainPage_ScrollBar = new System.Windows.Forms.VScrollBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MainPage = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.VideoPage_Panel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.VideoBack.SuspendLayout();
            this.VideoController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 41);
            this.panel1.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = global::TobiTube_Offline.Properties.Resources.Untitled1212;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(704, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(37, 35);
            this.button3.TabIndex = 4;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::TobiTube_Offline.Properties.Resources.LastAction;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(663, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 35);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(248, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(314, 22);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::TobiTube_Offline.Properties.Resources.e18156fb785603f3;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(745, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 35);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::TobiTube_Offline.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // VideoPage_Panel
            // 
            this.VideoPage_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VideoPage_Panel.AutoScroll = true;
            this.VideoPage_Panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.VideoPage_Panel.Controls.Add(this.RandomFrame_Button);
            this.VideoPage_Panel.Controls.Add(this.vScrollBar1);
            this.VideoPage_Panel.Controls.Add(this.panel2);
            this.VideoPage_Panel.Controls.Add(this.pictureBox2);
            this.VideoPage_Panel.Controls.Add(this.VideoBack);
            this.VideoPage_Panel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.VideoPage_Panel.Location = new System.Drawing.Point(0, 44);
            this.VideoPage_Panel.Name = "VideoPage_Panel";
            this.VideoPage_Panel.Size = new System.Drawing.Size(785, 418);
            this.VideoPage_Panel.TabIndex = 0;
            this.VideoPage_Panel.Visible = false;
            this.VideoPage_Panel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VideoPage_Panel_Scroll);
            // 
            // RandomFrame_Button
            // 
            this.RandomFrame_Button.BackColor = System.Drawing.Color.Silver;
            this.RandomFrame_Button.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.RandomFrame_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RandomFrame_Button.Location = new System.Drawing.Point(20, 10);
            this.RandomFrame_Button.Name = "RandomFrame_Button";
            this.RandomFrame_Button.Size = new System.Drawing.Size(35, 30);
            this.RandomFrame_Button.TabIndex = 7;
            this.RandomFrame_Button.Text = "Rnd";
            this.RandomFrame_Button.UseVisualStyleBackColor = false;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Enabled = false;
            this.vScrollBar1.LargeChange = 50;
            this.vScrollBar1.Location = new System.Drawing.Point(708, 12);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(12, 2685);
            this.vScrollBar1.SmallChange = 50;
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.AuthorLabel);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Location = new System.Drawing.Point(12, 288);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(477, 97);
            this.panel2.TabIndex = 1;
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AuthorLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AuthorLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AuthorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AuthorLabel.Location = new System.Drawing.Point(53, 60);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(57, 21);
            this.AuthorLabel.TabIndex = 2;
            this.AuthorLabel.Text = "label2";
            this.AuthorLabel.Click += new System.EventHandler(this.AuthorLabel_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox5.Image = global::TobiTube_Offline.Properties.Resources.StopButton;
            this.pictureBox5.Location = new System.Drawing.Point(-3, 50);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(50, 50);
            this.pictureBox5.TabIndex = 1;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.AuthorLabel_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkLabel1.LinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabel1.Location = new System.Drawing.Point(1, 1);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(471, 62);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.Text = "ОТЛИЧНОЕ НАЗВАНИЕ ВИДЕО - Пробуем зделать кое-что интересное =)";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(515, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(230, 2700);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseClick);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseMove);
            // 
            // VideoBack
            // 
            this.VideoBack.Controls.Add(this.VideoController);
            this.VideoBack.Controls.Add(this.vlcControl1);
            this.VideoBack.Location = new System.Drawing.Point(12, 12);
            this.VideoBack.Name = "VideoBack";
            this.VideoBack.Size = new System.Drawing.Size(477, 274);
            this.VideoBack.TabIndex = 7;
            // 
            // VideoController
            // 
            this.VideoController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VideoController.BackColor = System.Drawing.Color.DarkGray;
            this.VideoController.Controls.Add(this.button5);
            this.VideoController.Controls.Add(this.button4);
            this.VideoController.Controls.Add(this.pictureBox4);
            this.VideoController.Controls.Add(this.FullScreenVideo);
            this.VideoController.Controls.Add(this.StopVideoButton);
            this.VideoController.Controls.Add(this.PauseVideoButton);
            this.VideoController.Controls.Add(this.label1);
            this.VideoController.Controls.Add(this.pictureBox3);
            this.VideoController.Controls.Add(this.PlayVideoButton);
            this.VideoController.Location = new System.Drawing.Point(0, 227);
            this.VideoController.Name = "VideoController";
            this.VideoController.Size = new System.Drawing.Size(477, 47);
            this.VideoController.TabIndex = 6;
            this.VideoController.VisibleChanged += new System.EventHandler(this.VideoController_VisibleChanged);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(304, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(45, 25);
            this.button5.TabIndex = 8;
            this.button5.TabStop = false;
            this.button5.Text = "1,0x";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button5_MouseDown);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = global::TobiTube_Offline.Properties.Resources.VolumeFull;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.button4.Location = new System.Drawing.Point(353, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(28, 25);
            this.button4.TabIndex = 7;
            this.button4.TabStop = false;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Location = new System.Drawing.Point(382, 22);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(62, 20);
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // FullScreenVideo
            // 
            this.FullScreenVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FullScreenVideo.BackColor = System.Drawing.Color.Transparent;
            this.FullScreenVideo.BackgroundImage = global::TobiTube_Offline.Properties.Resources.dadara4;
            this.FullScreenVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FullScreenVideo.FlatAppearance.BorderSize = 0;
            this.FullScreenVideo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.FullScreenVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FullScreenVideo.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.FullScreenVideo.Location = new System.Drawing.Point(446, 19);
            this.FullScreenVideo.Name = "FullScreenVideo";
            this.FullScreenVideo.Size = new System.Drawing.Size(28, 26);
            this.FullScreenVideo.TabIndex = 5;
            this.FullScreenVideo.TabStop = false;
            this.FullScreenVideo.UseVisualStyleBackColor = false;
            // 
            // StopVideoButton
            // 
            this.StopVideoButton.BackColor = System.Drawing.Color.Transparent;
            this.StopVideoButton.BackgroundImage = global::TobiTube_Offline.Properties.Resources.StopButton;
            this.StopVideoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StopVideoButton.FlatAppearance.BorderSize = 0;
            this.StopVideoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.StopVideoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopVideoButton.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.StopVideoButton.Location = new System.Drawing.Point(61, 18);
            this.StopVideoButton.Name = "StopVideoButton";
            this.StopVideoButton.Size = new System.Drawing.Size(28, 26);
            this.StopVideoButton.TabIndex = 4;
            this.StopVideoButton.TabStop = false;
            this.StopVideoButton.UseVisualStyleBackColor = false;
            // 
            // PauseVideoButton
            // 
            this.PauseVideoButton.BackColor = System.Drawing.Color.Transparent;
            this.PauseVideoButton.BackgroundImage = global::TobiTube_Offline.Properties.Resources.PauseButton;
            this.PauseVideoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PauseVideoButton.FlatAppearance.BorderSize = 0;
            this.PauseVideoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.PauseVideoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PauseVideoButton.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.PauseVideoButton.Location = new System.Drawing.Point(32, 18);
            this.PauseVideoButton.Name = "PauseVideoButton";
            this.PauseVideoButton.Size = new System.Drawing.Size(28, 26);
            this.PauseVideoButton.TabIndex = 3;
            this.PauseVideoButton.TabStop = false;
            this.PauseVideoButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(96, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Time";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Location = new System.Drawing.Point(3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(471, 13);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // PlayVideoButton
            // 
            this.PlayVideoButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayVideoButton.BackgroundImage = global::TobiTube_Offline.Properties.Resources.PlayButton;
            this.PlayVideoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayVideoButton.FlatAppearance.BorderSize = 0;
            this.PlayVideoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.PlayVideoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayVideoButton.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.PlayVideoButton.Location = new System.Drawing.Point(3, 18);
            this.PlayVideoButton.Name = "PlayVideoButton";
            this.PlayVideoButton.Size = new System.Drawing.Size(28, 26);
            this.PlayVideoButton.TabIndex = 0;
            this.PlayVideoButton.TabStop = false;
            this.PlayVideoButton.UseVisualStyleBackColor = false;
            // 
            // vlcControl1
            // 
            this.vlcControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vlcControl1.BackColor = System.Drawing.Color.Silver;
            this.vlcControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.vlcControl1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.vlcControl1.Location = new System.Drawing.Point(0, 0);
            this.vlcControl1.Margin = new System.Windows.Forms.Padding(0);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(477, 274);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 0;
            this.vlcControl1.TabStop = false;
            this.vlcControl1.Text = "vlcControl1";
            this.vlcControl1.VlcLibDirectory = ((System.IO.DirectoryInfo)(resources.GetObject("vlcControl1.VlcLibDirectory")));
            this.vlcControl1.VlcMediaplayerOptions = null;
            this.vlcControl1.Layout += new System.Windows.Forms.LayoutEventHandler(this.vlcControl1_Layout);
            // 
            // MainPage_ScrollBar
            // 
            this.MainPage_ScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPage_ScrollBar.LargeChange = 50;
            this.MainPage_ScrollBar.Location = new System.Drawing.Point(771, 44);
            this.MainPage_ScrollBar.Name = "MainPage_ScrollBar";
            this.MainPage_ScrollBar.Size = new System.Drawing.Size(17, 418);
            this.MainPage_ScrollBar.SmallChange = 30;
            this.MainPage_ScrollBar.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainPage
            // 
            this.MainPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPage.Location = new System.Drawing.Point(0, 44);
            this.MainPage.Name = "MainPage";
            this.MainPage.Size = new System.Drawing.Size(772, 418);
            this.MainPage.TabIndex = 3;
            this.MainPage.TabStop = false;
            this.MainPage.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPage_Paint);
            this.MainPage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseClick);
            this.MainPage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPage_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.VideoPage_Panel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainPage_ScrollBar);
            this.Controls.Add(this.MainPage);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.axWindowsMediaPlayer1_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.axWindowsMediaPlayer1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.VideoPage_Panel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.VideoBack.ResumeLayout(false);
            this.VideoController.ResumeLayout(false);
            this.VideoController.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel VideoPage_Panel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox MainPage;
        private System.Windows.Forms.VScrollBar MainPage_ScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private Vlc.DotNet.Forms.VlcControl vlcControl1;
        private System.Windows.Forms.Panel VideoController;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button PlayVideoButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button StopVideoButton;
        private System.Windows.Forms.Button PauseVideoButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FullScreenVideo;
        private System.Windows.Forms.Panel VideoBack;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.PictureBox pictureBox5;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button RandomFrame_Button;
    }
}

