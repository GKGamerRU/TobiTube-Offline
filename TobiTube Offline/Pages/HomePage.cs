using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TobiTube_Offline.UniControls;

namespace TobiTube_Offline.Pages
{
    public class HomePage : Page
    {
        public List<Video> BestVideos = new List<Video>();

        GradientButton first = new GradientButton("View Random Video", new Size(250,50), new LinearGradientBrush(Point.Empty, new Point(250,50), Color.DarkOrange, Color.OrangeRed));

        public string BEST_VIDEOS = "Best videos";
        public string ALL_VIDEOS = "All videos";
        public string NOT_VIDEOS = "Videos not found!\nPlease, Add Folder with videos in settings!";

        public HomePage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;

            first.ForeColor = new SolidBrush(Color.White);
            first.OnClick = async () =>
            {
                var Result = TobiTubeAPI.AllVideos.Where(video => Algorithms.SearchPattern(video.Path, Form1.Instance.textBox1.Text, video) != 0).ToList();
                foreach (var vidos in Result)
                {
                    vidos.SearchRate = Algorithms.SearchPattern(vidos.Path, Form1.Instance.textBox1.Text, vidos);
                    vidos.RecalculateData();
                }
                Result.Sort((Video a, Video b) => b.SearchRate - a.SearchRate);
                var videos = Result.Count > 1 ? Result : TobiTubeAPI.AllVideos;

                var s = Utils.RandomNumberGenerator.GetRandomInt(0, videos.Count - 1);
                var p = TobiTubeAPI.GetPage<VideoPage>();
                for(int i = 0; i < p.Result.Count; i++)
                {
                    p.Result[i] = null;
                }
                TobiTubeAPI.CurrentPage = p;
                await videos[s].GenerateIcon();
                Form1.Instance.PlayVideo(videos[s]);
            };
            first.ApplyControl();

            Localization_OnLanguageChange();
            Localization.OnLanguageChange += Localization_OnLanguageChange;
        }

        private void Localization_OnLanguageChange()
        {
            BEST_VIDEOS = Localization.GetTranslate("Best videos");
            ALL_VIDEOS = Localization.GetTranslate("All videos");
            NOT_VIDEOS = Localization.GetTranslate("Videos not found!Please, Add Folder with videos in settings!");

            MainPage.Invalidate();
        }

        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;
            TargetedVideo = null;

            int x = 5, y = 5 - MainPage_ScrollBar.Value;
            if (TobiTubeAPI.AllVideosPath.Count == 0)
            {
                e.DrawString(NOT_VIDEOS, new Font("Segoe UI", 18), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
                return;
            }
            DrawControl(Target, first, e, x, y, Click, ref targeted);
            y += first.Scale.Height;
            y += 18;

            e.DrawString(BEST_VIDEOS, new Font("Segoe UI", 18), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;

            DrawVideos(BestVideos, e, Target, ref x, ref y, ref targeted, ref Click);
            y += 36;
            x = 5;
            e.DrawString(ALL_VIDEOS, new Font("Segoe UI", 18), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;

            DrawVideos(TobiTubeAPI.AllVideos, e, Target, ref x, ref y, ref targeted, ref Click, Count);
            ApplyResult(targeted);

            if (y + MainPage_ScrollBar.Value < MainPage.Height) { MainPage_ScrollBar.Visible = false; } else { MainPage_ScrollBar.Visible = true; }
            if (MainPage_ScrollBar.Visible) { MainPage_ScrollBar.Maximum = y + MainPage_ScrollBar.Value - MainPage.Height + 50; } else { MainPage_ScrollBar.Maximum = 0; }


            bool inited = true;
            List<Video> temp = new List<Video>();

            if (Count > TobiTubeAPI.AllVideos.Count) { Count = TobiTubeAPI.AllVideos.Count; }
            for (int i = Count - 20; i < Count; i++)
            {
                if (!TobiTubeAPI.AllVideos[i].Inited) { temp.Add(TobiTubeAPI.AllVideos[i]); inited = false; }
            }
            if (temp.Count != 0) { TobiTubeAPI.LoadLogotype(temp, delegate { if (TobiTubeAPI.CurrentPage == this) MainPage.Invalidate(); }); }

            if (MainPage_ScrollBar.Maximum <= MainPage_ScrollBar.Value + 50)
            {
                if (inited) { Count += 20; }
            }
        }
    }
}
