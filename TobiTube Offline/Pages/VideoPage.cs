using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.UniControls;

namespace TobiTube_Offline.Pages
{
    public class VideoPage : Page
    {
        public Toogle AutoNext = new Toogle("Auto-transition", 180);

        public List<Video> Result = new List<Video>();

        public string RELATED_VIDEOS = "Related videos";

        public VideoPage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;

            AutoNext.OnClick += delegate { Form1.AutoNext = AutoNext.Checked; };

            Localization_OnLanguageChange();
            Localization.OnLanguageChange += Localization_OnLanguageChange;
        }

        private void Localization_OnLanguageChange()
        {
            RELATED_VIDEOS = Localization.GetTranslate("Related videos");
            AutoNext.Value = Localization.GetTranslate("Auto-transition");

            MainPage.Invalidate();
        }

        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;
            TargetedVideo = null;
            Point oldPos = Point.Empty;

            int y = 5 /*- MainPage_ScrollBar.Value*/;
            int x = 10;
            y += 36;
            e.DrawString(RELATED_VIDEOS, new Font("Segoe UI", 16), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 32;

            foreach (var video in Result)
            {
                if (video == null) continue;
                if (y + video.Scale.Height >= 0 && y < MainPage.Height)
                {
                    DrawControl(Target, video, e, x, y, Click, ref targeted);
                }
                y += video.Scale.Height + x;
            }

            DrawControl(Target, AutoNext, e, x, 5 /*- MainPage_ScrollBar.Value*/, Click, ref targeted);
            ApplyResult(targeted);

            //MainPage_ScrollBar.Maximum = y + MainPage_ScrollBar.Value - MainPage.Height + 50;
        }
    }
}
