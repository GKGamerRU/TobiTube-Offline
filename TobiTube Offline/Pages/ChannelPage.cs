using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TobiTube_Offline.UniControls;
using Button = TobiTube_Offline.UniControls.Button;

namespace TobiTube_Offline.Pages
{
    public class ChannelPage : Page
    {
        public Button NoFilter = new Button("No filters", 150);
        public Button SortNew = new Button("New first", 175);
        public Button SortOld = new Button("Old first", 180);
        public Button SortShort = new Button("First short", 190);
        public Button SortLong = new Button("First long", 180);

        public string SEARCH = "Search";
        public string FOUND = "found";
        public string VIDEOS = "videos";
        string Title => $"{SEARCH} \"{Value}\" {FOUND} {Result.Count} {VIDEOS}.";

        public List<Video> Result = new List<Video>();
        public string Value;

        public ChannelPage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;

            NoFilter.OnClick += delegate { Result.Sort((Video a, Video b) => b.SearchRate - a.SearchRate); };
            SortNew.OnClick += delegate { Result.Sort((Video a, Video b) => DateTime.Compare(b.RawDate, a.RawDate)); };
            SortOld.OnClick += delegate { Result.Sort((Video a, Video b) => -DateTime.Compare(b.RawDate, a.RawDate)); };
            SortLong.OnClick += delegate { Result.Sort((Video a, Video b) => TimeSpan.Compare(b.Duration, a.Duration)); };
            SortShort.OnClick += delegate { Result.Sort((Video a, Video b) => -TimeSpan.Compare(b.Duration, a.Duration)); };

            Localization_OnLanguageChange();
            Localization.OnLanguageChange += Localization_OnLanguageChange;
        }

        private void Localization_OnLanguageChange()
        {
            SEARCH = Localization.GetTranslate("Search");
            FOUND = Localization.GetTranslate("found");
            VIDEOS = Localization.GetTranslate("videos");

            NoFilter.Value = Localization.GetTranslate("No filters");
            SortNew.Value = Localization.GetTranslate("New first");
            SortOld.Value = Localization.GetTranslate("Old first");
            SortShort.Value = Localization.GetTranslate("First short");
            SortLong.Value = Localization.GetTranslate("First long");

            MainPage.Invalidate();
        }

        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;
            TargetedVideo = null;

            int x = 0, y = 0 - MainPage_ScrollBar.Value;
            e.FillRectangle(Brushes.Silver, 0, y, e.ClipBounds.Width, 200);
            x = 5;
            y += 5;
            y += 200;

            e.DrawEllipse(Pens.Gray, 30, y + 30, 100, 100);
            e.DrawString(Value, new Font("Segoe UI Black", 24), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), 150, y+30);
            y += 160;

            DrawControl(Target, NoFilter, e, 9 + SortNew.Scale.Width + SortOld.Scale.Width, y, Click, ref targeted);
            DrawControl(Target, SortOld, e, 7 + SortNew.Scale.Width, y , Click, ref targeted);
            DrawControl(Target, SortNew, e, 5, y , Click, ref targeted);

            DrawControl(Target, SortLong, e, 5, y + 36  , Click, ref targeted);
            DrawControl(Target, SortShort, e, 7 + SortLong.Scale.Width, y + 36 , Click, ref targeted);

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
        public void ChangeTitle(string text) => Value = text;
    }
}
