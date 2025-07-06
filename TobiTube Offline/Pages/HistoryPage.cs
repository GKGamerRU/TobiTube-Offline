using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.UniControls;
using Button = TobiTube_Offline.UniControls.Button;

namespace TobiTube_Offline.Pages
{
    public class HistoryPage : Page
    {
        public List<Video> Result = new List<Video>();
        public string Value;

        public string BROWSING_HISTORY = "Browsing history";
        public Button ClearAll = new Button("Clear All", 200);

        public HistoryPage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;

            ClearAll.OnClick += delegate
            {
                if (MessageBox.Show(ClearAll.Value + "?", "TobiTube", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Result.Clear();
                    SearchedWorlds.words = new List<string>();
                    for (int i = SearchedWorlds.words.Count; i < 12; i++)
                    {
                        SearchedWorlds.words.Add("[DO_NOT_DELETE]");
                    }
                }
            };

            Localization_OnLanguageChange();
            Localization.OnLanguageChange += Localization_OnLanguageChange;
        }

        private void Localization_OnLanguageChange()
        {
            BROWSING_HISTORY = Localization.GetTranslate("Browsing history");
            ClearAll.Value = Localization.GetTranslate("Clear All");

            MainPage.Invalidate();
        }

        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;
            TargetedVideo = null;

            int x = 5, y = 5 - MainPage_ScrollBar.Value;
            e.DrawString(BROWSING_HISTORY, new Font("Segoe UI", 18), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            DrawControl(Target, ClearAll, e, MainPage.Width - 5 - ClearAll.Scale.Width, y, Click, ref targeted);
            y += 36;
            y += 36;
            List<Video> temp = DrawVideos(Result, e, Target, ref x, ref y, ref targeted, ref Click, Count);
            ApplyResult(targeted);

            if (y + MainPage_ScrollBar.Value < MainPage.Height) { MainPage_ScrollBar.Visible = false; } else { MainPage_ScrollBar.Visible = true; }
            if (MainPage_ScrollBar.Visible) { MainPage_ScrollBar.Maximum = y + MainPage_ScrollBar.Value - MainPage.Height + 50; } else { MainPage_ScrollBar.Maximum = 0; }

            bool inited = true;
            List<Video> UnlogotypedVideos = new List<Video>();

            foreach (var video in temp)
            {
                if (!video.Inited) { UnlogotypedVideos.Add(video); inited = false; }
            }

            if (UnlogotypedVideos.Count != 0) { TobiTubeAPI.LoadLogotype(UnlogotypedVideos, delegate { if (TobiTubeAPI.CurrentPage == this) MainPage.Invalidate(); }); }
            if (MainPage_ScrollBar.Maximum <= MainPage_ScrollBar.Value + 50)
            {
                if (inited) { Count += 20; }
            }
        }
    }
}
