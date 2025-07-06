using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.UniControls;

namespace TobiTube_Offline.Pages
{
    public class AboutPage : Page
    {
        public Picture Logo = new Picture(TobiTube_Offline.Properties.Resources.Logo, 300, 80);
        public GradientButton MyChannel = new GradientButton("Developer's channel", 220, Color.LightBlue);
        public GradientButton MySite = new GradientButton("Developer's site", 220, Color.DeepSkyBlue);

        public string ABOUT = "About";
        public string ABOUT_DESCRIPTION = "TobiTube is a video hosting-style local video playback software. The program may suggest related videos when watching a video. There is a recommendation system on the main page.";

        public AboutPage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;

            MyChannel.OnClick += delegate { Process.Start("https://www.youtube.com/channel/UCoq78IjhT5Vg7LhKPadtnQQ"); };
            MySite.OnClick += delegate { Process.Start("https://www.sites.google.com/view/tobish-inc"); };

            Localization_OnLanguageChange();
            Localization.OnLanguageChange += Localization_OnLanguageChange;
        }

        private void Localization_OnLanguageChange()
        {
            ABOUT = Localization.GetTranslate("About");
            ABOUT_DESCRIPTION = Localization.GetTranslate("TobiTube is a video hosting-style local video playback software. The program may suggest related videos when watching a video. There is a recommendation system on the main page.");

            MyChannel.Value = Localization.GetTranslate("Developer's channel");
            MySite.Value = Localization.GetTranslate("Developer's site");

            MyChannel.ApplyControl();
            MySite.ApplyControl();

            MainPage.Invalidate();
        }

        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;
            TargetedVideo = null;

            int x = 5, y = 5 - MainPage_ScrollBar.Value;
            e.DrawString(ABOUT, new Font("Segoe UI", 18), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;
            y += 36;

            DrawControl(Target, Logo, e, MainPage.Width / 2 - Logo.Scale.Width / 2, y, Click, ref targeted);
            y += Logo.Scale.Height;

            var t = e.MeasureString("TobiTube Release V1.2", new Font("Segoe UI", 12));
            e.DrawString("TobiTube Release V1.2", new Font("Segoe UI", 12), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), MainPage.Width / 2 - t.Width / 2, y);
            y += 50;

            e.DrawString(ABOUT_DESCRIPTION, new Font("Segoe UI", 12), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), new RectangleF(MainPage.Width / 2 - 150, y, 300, 250));
            y += 250;

            DrawControl(Target, MyChannel, e, MainPage.Width / 2 - MyChannel.Scale.Width / 2, y, Click, ref targeted);
            y += MyChannel.Scale.Height + 5;
            DrawControl(Target, MySite, e, MainPage.Width / 2 - MySite.Scale.Width / 2, y, Click, ref targeted);
            y += MySite.Scale.Height + 5;
            ApplyResult(targeted);

            if (y + MainPage_ScrollBar.Value < MainPage.Height) { MainPage_ScrollBar.Visible = false; } else { MainPage_ScrollBar.Visible = true; }
            if (MainPage_ScrollBar.Visible) { MainPage_ScrollBar.Maximum = y + MainPage_ScrollBar.Value - MainPage.Height + 50; } else { MainPage_ScrollBar.Maximum = 0; }
        }
    }
}
