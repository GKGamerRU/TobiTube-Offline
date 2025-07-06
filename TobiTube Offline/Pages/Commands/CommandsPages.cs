using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.Interfaces;

namespace TobiTube_Offline.Pages
{
    class CommandsPages : IGenerator<Page>
    {
        private PictureBox GraphicsTarget;
        private VScrollBar Scroller;

        private PictureBox VideoGraphicsTarget;
        private VScrollBar VideoScroller;

        public void SetPagesOptions(VScrollBar scroll, PictureBox rect)
        {
            Scroller = scroll;
            GraphicsTarget = rect;
        }
        public void SetVideoPagesOptions(VScrollBar scroll, PictureBox rect)
        {
            VideoScroller = scroll;
            VideoGraphicsTarget = rect;
        }
        public IEnumerable<Page> Generate()
        {
            Pages.Add("/process", new ProcessesPage(Scroller, GraphicsTarget));
            Pages.Add("/info", new InfoPage(Scroller, GraphicsTarget));
            Pages.Add("/help", new HelpPage(Scroller, GraphicsTarget));

            return Pages.Values;
        }
        public bool TryExecuteCommand(string command, ref Page result)
        {
            if (command == null) return false;
            switch (command.ToLower())
            {
                case "/legacysettings": new Settings().Show(Form1.Instance); return true;
                default: break;
            }

            if (Pages.ContainsKey(command))
            {
                result = Pages[command];
                result.MainPage?.Invalidate();
                return true;
            }
            return false;
        }

        public readonly List<string> OtherCommands = new List<string> { "/legacysettings" };
        public Dictionary<string, Page> Pages = new Dictionary<string, Page>();
    }
}
