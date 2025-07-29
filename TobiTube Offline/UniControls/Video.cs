using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using ThumbnailGenerator;
using TobiTube_Offline.VisualEffects;

namespace TobiTube_Offline.UniControls
{
    public class Video : UniControl
    {
        public int SearchRate = 0;
        public TimeSpan Duration = new TimeSpan(0, 0, 0);
        public float FrameRate = 0;

        public bool Inited = false, isInitialising = false;

        public string Path, FullName, Date = "Unknown", Channel = null;
        public DateTime RawDate;
        private Bitmap Content;

        private Image DrawBox;
        private Graphics BoxDrawer;

        public Video(string name, string path, string fullName, DateTime rawDate)
        {
            Name = name;
            Path = path;
            FullName = fullName;
            isWhite = ThemeSystem.IsBlack;

            Scale = new Size(200, 200);
            DrawBox = new Bitmap(Scale.Width, Scale.Height, PixelFormat.Format32bppPArgb);
            BoxDrawer = Graphics.FromImage(DrawBox);
            BoxDrawer.SmoothingMode = SmoothingMode.HighQuality;

            Bitmap content = new Bitmap(190, 100, PixelFormat.Format32bppPArgb);
            Graphics gfx = Graphics.FromImage(content);
            gfx.SmoothingMode = SmoothingMode.HighQuality;

            gfx.FillPath(Brushes.Black, Shape.GetRoundedRectagle(0, 0, 190, 100));

            Content = content;

            RawDate = rawDate;
            RecalculateData();

            RepaintBox();
        }
        public static string GetDurationString(TimeSpan duration)
        {
            string hh = duration.Hours != 0 ? duration.Hours + ":" : "";
            string ss = string.Format("{0:D2}", duration.Seconds);
            string temp = hh + duration.Minutes + ":" + ss;
            return temp;
        }

        public async Task GenerateIcon()
        {
            if (Inited || isInitialising) return;
            isInitialising = true;
            Bitmap shellThumb = new Bitmap(1, 1);

            await Task.Run(() =>
            {
                try
                {
                    using (ShellFile shellFile = ShellFile.FromFilePath(Path))
                    {
                        Duration = TimeSpan.FromTicks((long)(ulong)shellFile.Properties.System.Media.Duration.ValueAsObject);
                        FrameRate = (int)(uint)shellFile.Properties.System.Video.FrameRate.ValueAsObject;
                        FrameRate /= 1000;
                        shellThumb = shellFile.Thumbnail.LargeBitmap;
                    }
                    shellThumb = WindowsThumbnailProvider.GetThumbnail(Path, 190, 100, ThumbnailOptions.ThumbnailOnly);

                }
                catch { shellThumb = TobiTube_Offline.Properties.Resources.NoLogotype; isInitialising = false; }
            });

            Bitmap content = new Bitmap(190, 100, PixelFormat.Format32bppPArgb);
            Graphics gfx = Graphics.FromImage(content);
            gfx.SmoothingMode = SmoothingMode.HighQuality;

            gfx.FillPath(new TextureBrush(new Bitmap(shellThumb, 190, 100)), Shape.GetRoundedRectagle(0, 0, 190, 100));
            Content = content;
            isInitialising = false;
            Inited = true;

            await Task.Run(() => { isWhite = ThemeSystem.IsBlack; RepaintBox(); });
        }

        static protected Bitmap highlight = null;
        public override void Draw(int x, int y, Graphics e)
        {
            if (isTargeted) return;
            e.SmoothingMode = SmoothingMode.HighSpeed;
            
            if (isWhite != ThemeSystem.IsBlack) { isWhite = ThemeSystem.IsBlack; RepaintBox(); }
            e.DrawImageUnscaled(DrawBox, x, y);

            if (TobiTubeAPI.GlobalExperimentOptions["showSearchRate"])
                e.DrawString($"{SearchRate} - SearchRate", new Font("Segoe UI", 12), new SolidBrush(Color.Red), new RectangleF(x + 5, y + 160, 190, 90));
        }
        public override void PostDraw(int x, int y, Graphics e)
        {
            if (highlight == null)
            {
                highlight = new Bitmap(Scale.Width + 60, Scale.Height + 60);
                var gfx = Graphics.FromImage(highlight);
                gfx.SmoothingMode = SmoothingMode.AntiAlias;

                var pen = new Pen(new SolidBrush(Color.FromArgb(30, Color.Silver)), 20);
                for (int j = 0; j < 20; j += 1)
                {
                    gfx.DrawPath(pen, Shape.GetRoundedRectagle(30, 30, Scale.Width, Scale.Height));
                    pen.Width /= 1.25f;
                }
                gfx.FillPath(Brushes.Transparent, Shape.GetRoundedRectagle(30, 30, Scale.Width, Scale.Height));
            }
            e.DrawImageUnscaled(highlight, x - 30, y - 30);

            isTargeted = false;
            Draw(x, y, e);
            isTargeted = true;
        }
        public void RecalculateData()
        {
            Date = RawDate.ToShortDateString();
            
            var dur = DateTime.UtcNow - RawDate.ToUniversalTime();

            int years = dur.Days / 365;
            int months = dur.Days / 30;
            int days = dur.Days;
            int hours = dur.Hours;
            int minutes = dur.Minutes;
            int seconds = dur.Seconds;

            string resultTime = " ";

            if (years > 0) resultTime += $"- {years} years ago";
            else if (months > 0) resultTime += $"- {months} months ago";
            else if (days > 0) resultTime += $"- {days} days ago";
            else if (hours > 0) resultTime += $"- {hours} hours ago";
            else if (minutes > 0) resultTime += $"- {minutes} minutes ago";
            else if (seconds > 0) resultTime += $"- {seconds} seconds ago";
            else resultTime += $"- {RawDate.ToShortTimeString()}";

            Date += resultTime;
        }
        public void RepaintBox()
        {
            BoxDrawer.Clear(Color.Transparent);

            var color = Content.GetPixel(Content.Width / 2, Content.Height / 2);
            color = Color.FromArgb(color.R / 2, color.G / 2, color.B / 2);
            if (ThemeSystem.IsBlack)
            {
                int lowestColor = 300;
                if (color.R < lowestColor) lowestColor = color.R;
                if (color.G < lowestColor) lowestColor = color.G;
                if (color.B < lowestColor) lowestColor = color.B;
                color = Color.FromArgb(Math.Max(color.R - lowestColor, 20), Math.Max(color.G - lowestColor, 20), Math.Max(color.B - lowestColor, 20));
            }
            else
            {
                int lowestColor = 300;
                if (255 - color.R < lowestColor) lowestColor = 255 - color.R;
                if (255 - color.G < lowestColor) lowestColor = 255 - color.G;
                if (255 - color.B < lowestColor) lowestColor = 255 - color.B;
                color = Color.FromArgb(color.R + lowestColor, color.G + lowestColor, color.B + lowestColor);
            }
            var gradient = new LinearGradientBrush(Point.Empty, new Point(0, Scale.Height), ThemeSystem.CurrentTheme["VideoColor"], ThemeSystem.IsBlack ? color : color);
            BoxDrawer.FillPath(gradient, Shape.GetRoundedRectagle(0, 0, Scale.Width, Scale.Height));
            BoxDrawer.DrawPath(new Pen(ThemeSystem.CurrentTheme["VideoColor"]), Shape.GetRoundedRectagle(0, 0, Scale.Width, Scale.Height));

            BoxDrawer.DrawString(Name, new Font("Segoe UI", 10.5f), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), new RectangleF(5, 110, 190, 70));
            BoxDrawer.DrawString(Date, new Font("Segoe UI", 10), ThemeSystem.IsBlack ? Brushes.SkyBlue : Brushes.DarkBlue, new RectangleF(5, 110 + 70, 190, 20));
            BoxDrawer.DrawImageUnscaled(Content, 5, 5);

            string time = GetDurationString(Duration);
            SizeF dur_size = BoxDrawer.MeasureString(time, new Font("Segoe UI", 10));

            if (FrameRate != 0)
            {
                BoxDrawer.FillPath(FrameRate >= 50 ? Brushes.DarkBlue : Brushes.DimGray, Shape.GetRoundedRectagle(Scale.Width - (int)dur_size.Width - 8, 108 - 25, (int)dur_size.Width, (int)dur_size.Height, 5));
                BoxDrawer.DrawString(time, new Font("Segoe UI", 9), Brushes.White, new RectangleF(Scale.Width - (int)dur_size.Width - 8, 108 - 25, (int)dur_size.Width, (int)dur_size.Height));
            }
        }
    }
}
