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
    public class HelpPage: Page
    {
        public HelpPage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;
        }

        public void Init()
        {
            Page temp = null;
            foreach (var command in TobiTubeAPI.commandsPages.Pages.Keys)
            {
                var b = new TobiTube_Offline.UniControls.Button(command, 200);
                b.OnClick += delegate { TobiTubeAPI.commandsPages.TryExecuteCommand(command, ref temp); TobiTubeAPI.CurrentPage = temp; };
                buttons.Add(b);
            }
            foreach (var command in TobiTubeAPI.commandsPages.OtherCommands)
            {
                var b = new TobiTube_Offline.UniControls.Button(command, 200);
                b.OnClick += delegate { TobiTubeAPI.commandsPages.TryExecuteCommand(command, ref temp); };
                buttons.Add(b);
            }
        }

        List<TobiTube_Offline.UniControls.Button> buttons = new List<TobiTube_Offline.UniControls.Button>();

        Font font = new Font("Segoe UI", 18);
        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;
            var processes = Process.GetProcesses();

            int x = 5, y = 5 - MainPage_ScrollBar.Value;
            e.DrawString($"Help Page", font, new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;

            foreach(var but in buttons)
            {
                DrawControl(Target, but, e, x, y,Click, ref targeted);
                y += 36;
            }
            ApplyResult(targeted);

            if (y + MainPage_ScrollBar.Value < MainPage.Height) { MainPage_ScrollBar.Visible = false; } else { MainPage_ScrollBar.Visible = true; }
            if (MainPage_ScrollBar.Visible)
            { MainPage_ScrollBar.Maximum = y + MainPage_ScrollBar.Value - MainPage.Height + 50; }
            else
            { MainPage_ScrollBar.Maximum = 0; }
        }
    }
}
