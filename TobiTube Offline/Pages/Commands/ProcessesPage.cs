using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TobiTube_Offline.Pages
{
    public class ProcessesPage : Page
    {
        public string SETTINGS = "Processes";

        public ProcessesPage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;
        }

        Font font = new Font("Segoe UI", 12);
        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;
            var processes = Process.GetProcesses();

            int x = 5, y = 5 - MainPage_ScrollBar.Value;
            e.DrawString($"{SETTINGS}: {processes.Length}", new Font("Segoe UI", 18), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;
            y += 36;

            var textBrush = new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]);
            foreach (var process in processes)
            {
                e.DrawString($"{process.ProcessName}: ID-{process.Id} Memory-{process.WorkingSet64 / 1024 / 1024} Args-{process.StartInfo.Arguments}", font, textBrush, x, y);
                y += 24;
            }
            y += 50;

            ApplyResult(targeted);

            if (y + MainPage_ScrollBar.Value < MainPage.Height) { MainPage_ScrollBar.Visible = false; } else { MainPage_ScrollBar.Visible = true; }
            if (MainPage_ScrollBar.Visible) { MainPage_ScrollBar.Maximum = y + MainPage_ScrollBar.Value - MainPage.Height + 50; } else { MainPage_ScrollBar.Maximum = 0; }
        }
    }
}
