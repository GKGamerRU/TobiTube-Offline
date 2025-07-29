using System.Collections.Generic;
using System.Windows.Forms;
using TobiTube_Offline.Interfaces;

namespace TobiTube_Offline.Pages
{
    public class PagesGenerator : IGenerator<Page>
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
            List<Page> pages = new List<Page>();
            pages.Add(new HomePage(Scroller, GraphicsTarget));
            pages.Add(new SettingsPage(Scroller, GraphicsTarget));
            pages.Add(new SearchPage(Scroller, GraphicsTarget));
            pages.Add(new VideoPage(VideoScroller, VideoGraphicsTarget));
            pages.Add(new HistoryPage(Scroller, GraphicsTarget));
            pages.Add(new AboutPage(Scroller, GraphicsTarget));
            pages.Add(new ChannelPage(Scroller, GraphicsTarget));

            return pages;
        }
    }
}
