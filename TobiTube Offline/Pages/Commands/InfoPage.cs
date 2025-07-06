using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.Properties;

namespace TobiTube_Offline.Pages
{
    public class InfoPage : Page
    {
        public InfoPage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;
        }

        Font font = new Font("Segoe UI", 18);
        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;
            var processes = Process.GetProcesses();

            int x = 5, y = 5 - MainPage_ScrollBar.Value;
            e.DrawString($"Information", font, new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36; y += 36;
            e.DrawString($"TobiTube V{TobiTubeAPI.VERSION}", font, new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;
            e.DrawString($"Videos: {TobiTubeAPI.AllVideos.Count}", font, new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;
            e.DrawString($"History: {TobiTubeAPI.GetPage<HistoryPage>().Result.Count}", font, new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);

            ApplyResult(targeted);

            if (y + MainPage_ScrollBar.Value < MainPage.Height) { MainPage_ScrollBar.Visible = false; } else { MainPage_ScrollBar.Visible = true; }
            if (MainPage_ScrollBar.Visible) 
                { MainPage_ScrollBar.Maximum = y + MainPage_ScrollBar.Value - MainPage.Height + 50; } 
            else 
                { MainPage_ScrollBar.Maximum = 0; }
        }
    }
}
