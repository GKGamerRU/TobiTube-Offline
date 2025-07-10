using System;
using System.Drawing;
using System.Windows.Forms;
using Vlc.DotNet.Forms;

namespace TobiTube_Offline.VideoModules
{
    public class VideoPlayer
    {
        public Form Attach = new Form();
        Form senderForm;

        public VideoPlayer(Form _sender, VlcControl _videoPlayer, Control videoOwner, Control area, PercentBar _percentBar)
        {
            senderForm = _sender;
            videoPlayer = _videoPlayer;
            Area = area;
            VideoOwner = videoOwner;
            percentBar = _percentBar;

            Area.Resize += Parent_Resize;

            senderForm.LocationChanged += Sender_LocationChanged;
            area.VisibleChanged += Area_VisibleChanged;

            senderForm.Deactivate += SenderForm_LostFocus;
            senderForm.Activated += SenderForm_GotFocus;

            Attach.Activated += delegate { Attach.Visible = true; };
            Attach.LostFocus += delegate { if ((senderForm.ContainsFocus == false && form?.ContainsFocus == false) || (senderForm.ContainsFocus == false && form == null))Attach.Visible = false; else ActiForm(); };
            VideoOwner.LocationChanged += delegate { ChangeAttach(); };

            Attach.ShowInTaskbar = false;
            Attach.Opacity = 0.01;
            Attach.BackColor = Color.Black;
            Attach.FormBorderStyle = FormBorderStyle.None;
            Attach.DoubleClick += delegate { IsFullScreen = !IsFullScreen; ActiForm(); };
            Attach.Click += delegate { ActiForm(); };
            Attach.TopMost = true;
            Attach.Show(senderForm);
            Attach.KeyPreview = true;
            Attach.KeyDown += KeyDown;
            ChangeAttach();
        }

        void ActiForm()
        {
            if (form != null) form.Activate();
            else
                senderForm.Activate();
        }
        private void SenderForm_GotFocus(object sender, EventArgs e)
        {
            Attach.Visible = Area.Visible || form != null;
        }

        private void SenderForm_LostFocus(object sender, EventArgs e)
        {
            Attach.Visible = false;
        }

        private void Area_VisibleChanged(object sender, EventArgs e)
        {
            Attach.Visible = Area.Visible || form != null;
        }

        private void Sender_LocationChanged(object sender, EventArgs e)
        {
            Point p = new Point(VideoOwner.Parent.Left + VideoOwner.Left + senderForm.Left, senderForm.Top + VideoOwner.Parent.Top + VideoOwner.Top);
            p = senderForm.PointToScreen(VideoOwner.Location);
            p.Y += Area.Top;
            if (VideoOwner.Top < 0)
            {
                p.Y += -VideoOwner.Top;
            }
            Attach.Location = p;
        }

        public void ChangeAttach()
        {
            if (!IsFullScreen)
            {
                Point p = new Point(VideoOwner.Parent.Left + VideoOwner.Left + senderForm.Left, senderForm.Top + VideoOwner.Parent.Top + VideoOwner.Top ); //- ((Panel)Area).VerticalScroll.Value
                p = senderForm.PointToScreen(VideoOwner.Location);
                p.Y += Area.Top;// + ((Panel)Area).VerticalScroll.Value;
                if(VideoOwner.Top < 0)
                {
                    p.Y += -VideoOwner.Top;
                }
                Attach.Location = p;

                Attach.Width = VideoOwner.Width;
                Attach.Height = Math.Min(VideoOwner.Top < 0 ? VideoOwner.Height - controllerSize + VideoOwner.Top : VideoOwner.Height - controllerSize, Area.Height); //- ((Panel)Area).VerticalScroll.Value;
            }
            else
            {
                Attach.Location = new Point();
                Attach.Size = new Size(VideoOwner.Width, VideoOwner.Height - controllerSize);
            }
        }

        private void Parent_Resize(object sender, System.EventArgs e)
        {
            if (IsFullScreen) return;
            VideoOwner.Top = 12 - ((Panel)Area).VerticalScroll.Value;
            VideoOwner.Left = 60;

            VideoOwner.Width = Area.Width - 330 - 60;
            VideoOwner.Height = VideoOwner.Width / 16 * 9;

            ChangeAttach();
        }

        private Control Area, VideoOwner;
        private VlcControl videoPlayer;
        public Form form;
        private PercentBar percentBar;

        bool fullScreen = false;
        int controllerSize = 0;
        public bool IsFullScreen
        {
            get
            {
                return fullScreen;
            }
            set
            {
                fullScreen = value;
                CreateForm();
            }
        }
        public int ControllerSize
        {
            get { return controllerSize; }
            set { controllerSize = value; ChangeAttach(); }
        }

        private void CreateForm()
        {
            if (IsFullScreen)
            {
                form = new Form();
                form.FormBorderStyle = FormBorderStyle.None;
                form.Size = Screen.PrimaryScreen.Bounds.Size;
                form.KeyPreview = true;
                form.KeyDown += KeyDown;
                form.ShowInTaskbar = false;

                form.Deactivate += SenderForm_LostFocus;
                form.Activated += SenderForm_GotFocus;

                form.Controls.Add(VideoOwner);
                VideoOwner.Size = Screen.PrimaryScreen.Bounds.Size;
                VideoOwner.Location = Point.Empty;
                VideoOwner.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                form.FormClosing += delegate { form.Visible = false; IsFullScreen = false; };
                Form owner = Area.FindForm();
                form.Show(owner);
                //Attach.Owner = form;
            }
            else
            {
                Area.Controls.Add(VideoOwner);
                Area.PerformLayout();
                VideoOwner.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                if (form.Visible) form.Close();

                VideoOwner.Top = 12 - ((Panel)Area).VerticalScroll.Value;
                VideoOwner.Left = 60;

                VideoOwner.Width = Area.Width - 330 - 60;
                VideoOwner.Height = VideoOwner.Width / 16 * 9;

                Area.FindForm().Activate();
                form = null;
                //Attach.Owner = senderForm;
            }
            ChangeAttach();
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) if (videoPlayer.Time - 5000 >= 0) videoPlayer.Time -= 5000; else videoPlayer.Time = 0;
            if (e.KeyCode == Keys.D) videoPlayer.Time += 5000;

            if (e.KeyCode == Keys.S) percentBar.Update(videoPlayer.Audio.Volume - 5);
            if (e.KeyCode == Keys.W) percentBar.Update(videoPlayer.Audio.Volume + 5);

            if (e.KeyCode == Keys.Space) videoPlayer.Pause();
            if (e.KeyCode == Keys.Escape && IsFullScreen) IsFullScreen = false;
        }

        public void Play() { videoPlayer.Play(); }
        public void Play(string file) { videoPlayer.Play(file); }
        public void Pause() { videoPlayer.Pause(); }
        public void Stop() { videoPlayer.Stop(); }
    }
}
