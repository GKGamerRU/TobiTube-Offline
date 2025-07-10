using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.Configs;
using TobiTube_Offline.Pages;
using TobiTube_Offline.UniControls;
using TobiTube_Offline.VideoModules;
using Vlc.DotNet.Core;

namespace TobiTube_Offline
{
    public partial class Form1 : Form
    {
        public static Form1 Instance { get; private set; }

        TimeBar timeBar;
        PercentBar percentBar;
        VideoPlayer videoPlayer;

        VideoPage videoPage;
        HomePage homePage;
        HistoryPage historyPage;
        SearchPage searchPage;

        public Form1()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            percentBar = new PercentBar(pictureBox4);
            percentBar.OnValueChanged += delegate (int volume)
            {
                vlcControl1.Audio.Volume = volume;
                if (volume <= 0) button4.BackgroundImage = Properties.Resources.VolumeZero;
                if (volume >= 1) button4.BackgroundImage = Properties.Resources.VolumeLow;
                if (volume >= 33) button4.BackgroundImage = Properties.Resources.VolumeHalf;
                if (volume >= 75) button4.BackgroundImage = Properties.Resources.VolumeFull;
            };

            videoPlayer = new VideoPlayer(this, vlcControl1, VideoBack, VideoPage_Panel, percentBar);

            PlayVideoButton.Click += delegate { vlcControl1.Play(); };
            PauseVideoButton.Click += delegate { vlcControl1.Pause(); };
            StopVideoButton.Click += delegate { vlcControl1.Stop(); };
            FullScreenVideo.Click += delegate { videoPlayer.IsFullScreen = !videoPlayer.IsFullScreen; };
            vlcControl1.MouseDoubleClick += delegate { videoPlayer.IsFullScreen = !videoPlayer.IsFullScreen; };

            MainPage_ScrollBar.Scroll += delegate { MainPage.Invalidate(); };
            MainPage.SizeChanged += delegate { MainPage.Invalidate(); };
            MainPage.MouseLeave += delegate { Target = new Point(); MainPage.Invalidate(); };

            MainPage.MouseDown += delegate { TobiTubeAPI.CurrentPage.isMouseDOwn = true; };
            MainPage.MouseUp += delegate { TobiTubeAPI.CurrentPage.isMouseDOwn = false; };

            vScrollBar1.Scroll += delegate { pictureBox2.Invalidate(); };
            pictureBox2.SizeChanged += delegate { pictureBox2.Invalidate(); };
            pictureBox2.MouseLeave += delegate { Target = new Point(); pictureBox2.Invalidate(); };

            TobiTubeAPI.Init(MainPage_ScrollBar,MainPage,vScrollBar1, pictureBox2);
            videoPage = TobiTubeAPI.GetPage<VideoPage>();
            homePage = TobiTubeAPI.GetPage<HomePage>();
            historyPage = TobiTubeAPI.GetPage<HistoryPage>();
            searchPage = TobiTubeAPI.GetPage<SearchPage>();

            ThemeSystem.OnColorChange += OnThemeChanged;
            OnThemeChanged(ThemeSystem.CurrentTheme);
            
            timeBar = new TimeBar(pictureBox3);
            timeBar.OnTimeChanged += delegate (float time) { vlcControl1.Time = (long)time; };
            RandomFrame_Button.Click += delegate {
                timeBar.Value = Utils.RandomNumberGenerator.GetRandomInt(0, (int)timeBar.MaxValue);
                timeBar.Draw();
                vlcControl1.Time = (long)timeBar.Value;
            };
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.X >= MainPage.Location.X && e.X <= MainPage.Location.X + MainPage.Width
                &&
                e.Y >= MainPage.Location.Y && e.Y <= MainPage.Location.Y + MainPage.Height)
            {
                if (MainPage.Visible)
                {
                    if(TobiTubeAPI.CurrentPage.TargetedObject != null)
                    {
                        if (TobiTubeAPI.CurrentPage.TargetedObject.OnScroll(-e.Delta))
                        { MainPage.Invalidate(); return; }
                    }

                    int preResult = MainPage_ScrollBar.Value + -e.Delta;
                    int result = preResult <= MainPage_ScrollBar.Minimum ? MainPage_ScrollBar.Value = 0 : (preResult >= MainPage_ScrollBar.Maximum ? MainPage_ScrollBar.Value = MainPage_ScrollBar.Maximum : preResult);
                    MainPage_ScrollBar.Value = result;
                    MainPage.Invalidate();
                }
                if (VideoPage_Panel.Visible)
                {
                    ////    int preResult = VideoPage_Panel.VerticalScroll.Value + -e.Delta;
                    ////    int result = preResult <= VideoPage_Panel.VerticalScroll.Minimum ? VideoPage_Panel.VerticalScroll.Value = 0 : (preResult >= VideoPage_Panel.VerticalScroll.Maximum ? VideoPage_Panel.VerticalScroll.Value = VideoPage_Panel.VerticalScroll.Maximum : preResult);
                    ////    VideoPage_Panel.VerticalScroll.Value = result;
                    ////    VideoPage_Panel.Invalidate();

                    //    VideoPage_Panel.PerformLayout();
                }
            }
            base.OnMouseWheel(e);
        }

        private void axWindowsMediaPlayer1_Resize(object sender, EventArgs e)
        {
            if (videoPlayer.IsFullScreen) return;
            panel2.Top = VideoBack.Height + 15 - VideoPage_Panel.VerticalScroll.Value;
            panel2.Left = 60;
            panel2.Width = VideoBack.Width;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            VideoPage_Panel.Visible = false;
            vlcControl1.Stop();
            textBox1.Text = "";
            MainPage_ScrollBar.Value = 0;
            TobiTubeAPI.CurrentPage = homePage;
            MainPage.Invalidate();
            if (TobiTubeAPI.AllVideosPath.Count != 0) TobiTubeAPI.RegenerateBestVideos();
        }

        public void PlayVideo(Video obj)
        {
            VideoPage_Panel.Visible = true;
            linkLabel1.Text = obj.Name;
            vScrollBar1.Value = 0;
            SearchedWorlds.AddWord(obj.Name);
            historyPage.Result.Insert(0,obj);
            if (searchPage.Value != "") SearchedWorlds.AddWord(textBox1.Text);

            TobiTubeAPI.CurrentPage = videoPage;
            List<Video> vid = new List<Video>(TobiTubeAPI.AllVideos).Where(video => video.FullName != obj.FullName).ToList();
            TobiTubeAPI.GenerateVideos_VideoPage(vid, obj.Name);

            timeBar.MaxValue = (float)obj.Duration.TotalMilliseconds;
            vlcControl1.VlcMediaPlayer.Play(new Uri(obj.Path));
            AuthorLabel.Text = obj.Channel;
            this.Focus();
            vlcControl1.Select();
        }
        
        private void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            Target = e.Location;
            MainPage.Invalidate();
        }

        private void MainPage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (TobiTubeAPI.CurrentPage.TargetedObject is Video)
                    PlayVideo((Video)TobiTubeAPI.CurrentPage.TargetedObject);
                else
                    TobiTubeAPI.CurrentPage.TargetedObject?.OnClick();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (TobiTubeAPI.CurrentPage.TargetedObject is Video video) {
                    if (VideoPage_Panel.Visible)
                    {
                        VideoPage_Panel.Visible = false;
                        vlcControl1.Stop();
                        MainPage_ScrollBar.Value = 0;
                    }

                    TobiTubeAPI.ChangeChannel(video.Channel, video.Channel);
                    searchPage.ChangeTitle(video.Channel);
                    MainPage.Invalidate();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (VideoPage_Panel.Visible)
            {
                VideoPage_Panel.Visible = false;
                vlcControl1.Stop();
                MainPage_ScrollBar.Value = 0;
                MainPage.Invalidate();
            }
            TobiTubeAPI.CurrentPage = TobiTubeAPI.GetPage<SettingsPage>();
            MainPage.Invalidate();
        }
        
        Pen HighLite_prefab = new Pen(Color.DarkOrange,2);
        Point Target = new Point();

        public int FPS { get; protected set; } = 0;
        int RealTimeFPS, RealTime = 0; public static bool ShowFPS = false;
        private void MainPage_Paint(object sender, PaintEventArgs e)
        {
            if(TobiTubeAPI.CurrentPage != null && !VideoPage_Panel.Visible)
            {
                TobiTubeAPI.CurrentPage.Redraw(e.Graphics, Target, false);
                TobiTubeAPI.CurrentPage.PostDraw(e.Graphics);
            }
            if (DateTime.UtcNow.Millisecond <=0 || RealTime == DateTime.UtcNow.Second) { RealTimeFPS++; } else { FPS = RealTimeFPS; RealTimeFPS = 1; RealTime = DateTime.UtcNow.Second; }
            if (ShowFPS) e.Graphics.DrawString(FPS.ToString() + " FPS", new Font("Segoe UI Black", 16), Brushes.Blue, 0, 0);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            videoPage.Redraw(e.Graphics, Target, false);
            videoPage.PostDraw(e.Graphics);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            Target = e.Location;
            pictureBox2.Invalidate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ThemeSystem.OnColorChange -= OnThemeChanged;
            SearchedWorlds.SaveWords();
            SettingsConfig.Save();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textBox1.Text = "";
                if (VideoPage_Panel.Visible) return;
                TobiTubeAPI.ChangeSearch(textBox1.Text.ToLower());
                MainPage.Invalidate();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (VideoPage_Panel.Visible && TobiTubeAPI.commandsPages.OtherCommands.Contains(textBox1.Text.ToLower()) == false)
                {
                    VideoPage_Panel.Visible = false;
                    vlcControl1.Stop();
                    MainPage_ScrollBar.Value = 0;
                    MainPage.Invalidate();
                }
                searchPage.Count = 20;
                TobiTubeAPI.ChangeSearch(textBox1.Text.ToLower());
                MainPage.Invalidate();
            }
        }

        public static bool AutoNext = false;
        
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                if (VideoPage_Panel.Visible) {
                    return;
                }
                TobiTubeAPI.ChangeSearch(textBox1.Text.ToLower());
                MainPage.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (VideoPage_Panel.Visible)
            {
                VideoPage_Panel.Visible = false;
                vlcControl1.Stop();
                MainPage_ScrollBar.Value = 0;
                MainPage.Invalidate();
            }
            historyPage.Count = 20;
            TobiTubeAPI.CurrentPage = historyPage;
            MainPage.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (VideoPage_Panel.Visible)
            {
                VideoPage_Panel.Visible = false;
                vlcControl1.Stop();
                MainPage_ScrollBar.Value = 0;
                MainPage.Invalidate();
            }
            TobiTubeAPI.CurrentPage = TobiTubeAPI.GetPage<AboutPage>();
            MainPage.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           //if(MouseButtons == MouseButtons.Middle) { MessageBox.Show("DD"); }; - Этот код заменит логику обработки нажатий
            if (vlcControl1.IsPlaying) // ContainsFocus
            {
                timeBar.Update(vlcControl1.Time);
                label1.Text = $"{timeBar.Minutes}:{string.Format("{0:D2}", timeBar.Seconds)}";
            }
            else if (AutoNext && vlcControl1.State == Vlc.DotNet.Core.Interops.Signatures.MediaStates.Ended)
            {
                PlayVideo(videoPage.Result[0]);
            }
            if (VideoPage_Panel.Visible)
            {
                if (!videoPlayer.IsFullScreen)
                {
                    Point loc = this.PointToScreen(vlcControl1.Location);
                    loc.Y += VideoPage_Panel.Top + VideoBack.Top;
                    loc.X += VideoBack.Left;

                    bool val = WindowState != FormWindowState.Minimized && loc.X <= Cursor.Position.X && loc.Y <= Cursor.Position.Y &&
                        loc.X + vlcControl1.Width > Cursor.Position.X && loc.Y + vlcControl1.Height > Cursor.Position.Y;

                    if (val != VideoController.Visible)
                        VideoController.Visible = val;

                    //videoPlayer.ChangeAttach();
                }
                else
                {
                    VideoController.Visible = Cursor.Position.Y >= Screen.PrimaryScreen.Bounds.Height - VideoController.Height;
                }
            }
        }

        bool VLCInited = false;
        private void vlcControl1_Layout(object sender, LayoutEventArgs e)
        {
            if (!VLCInited)
            {
                if (!Directory.Exists(@"C:\Program Files\VideoLAN\VLC"))
                {
                    MessageBox.Show("Please, install VLC media player!", "VLC is not Installed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Process.GetCurrentProcess().Kill();
                }
                vlcControl1.VlcLibDirectory = new DirectoryInfo(@"C:\Program Files\VideoLAN\VLC");
                VLCInited = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ActiveControl is TextBox && e.KeyCode == Keys.Escape)
            {
                TobiTubeAPI.CurrentPage.MainPage.Select();
            }
            if (VideoPage_Panel.Visible && !(ActiveControl is TextBox))
            {
                vlcControl1.Select();
                videoPlayer.KeyDown(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (vlcControl1.Audio.Volume == 0)
            {
                percentBar.Update(100);
                button4.BackgroundImage = Properties.Resources.VolumeFull;
            }
            else
            {
                percentBar.Update(0);
                button4.BackgroundImage = Properties.Resources.VolumeZero;
            }
        }
        private void vlcControl1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void VideoController_VisibleChanged(object sender, EventArgs e)
        {
            if (VideoController.Visible)
                videoPlayer.ControllerSize = VideoController.Height;
            else
                videoPlayer.ControllerSize = 0;
        }

        private void AuthorLabel_Click(object sender, EventArgs e)
        {
            if (VideoPage_Panel.Visible)
            {
                VideoPage_Panel.Visible = false;
                vlcControl1.Stop();
                MainPage_ScrollBar.Value = 0;
            }

            TobiTubeAPI.ChangeChannel(AuthorLabel.Text, AuthorLabel.Text);
            searchPage.ChangeTitle(AuthorLabel.Text);
            MainPage.Invalidate();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void VideoPage_Panel_Scroll(object sender, ScrollEventArgs e)
        {
            videoPlayer.ChangeAttach();
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            string value = button5.Text.TrimEnd('x');
            float speed = Convert.ToSingle(value);

            if (speed >= 2)
            {
                speed = 0;
            }

            if (e.Button == MouseButtons.Left)
                speed += 0.25f;
            else if (e.Button == MouseButtons.Right)
                speed -= 0.25f;

            if (speed < 0.25f)
            {
                speed = 0.25f;
            }

            button5.Text = speed + "x";
            vlcControl1.Rate = speed;
        }

        void OnThemeChanged(Dictionary<string, Color> e)
        {
            VideoPage_Panel.BackColor = e["BackColor"];
            panel2.BackColor = e["VideoColor"];
            panel1.BackColor = e["HeadColor"];
            this.BackColor = e["BackColor"];
            textBox1.BackColor = e["BlockColor"];
            textBox1.ForeColor = e["TextColor"];
            linkLabel1.ForeColor = e["TextColor"];
            AuthorLabel.ForeColor = e["TextColor"];
        }
    }
}
