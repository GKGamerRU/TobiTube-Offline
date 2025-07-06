using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.UniControls;

namespace TobiTube_Offline.Pages
{
    public abstract class Page
    {
        public VScrollBar MainPage_ScrollBar;
        public PictureBox MainPage;
        public UniControl TargetedObject;
        public Video TargetedVideo;
        public int Count = 20;
        public bool isMouseDOwn = false;

        Point lastPoint;
        public void DrawControl(Point Target, UniControl test, Graphics e, int x, int y, bool Click, ref bool targeted)
        {
            CheckTarget(Target, test, x, y, Click, ref targeted);
            test.BackDraw(x, y, e);
            test.Draw(x, y, e);
        }
        public void CheckTarget(Point Target, UniControl test, int x, int y, bool Click, ref bool targeted)
        {
            if (Target.X >= x && Target.X <= x + test.Scale.Width && Target.Y >= y && Target.Y <= y + test.Scale.Height)
            {
                test.isTargeted = true;
                targeted = true;
                TargetedObject = test;
                lastPoint = new Point(x, y);
                if (Click) { test.OnClick(); }
            }
            else
            {
                test.isTargeted = false;
            }
        }
        public void ApplyResult(bool targeted)
        {
            if (targeted)
            {
                MainPage.Cursor = Cursors.Hand;
            }
            else
            {
                MainPage.Cursor = Cursors.Default;
                TargetedObject = null;
            }
        }
        public List<Video> DrawVideos(List<Video> videos, Graphics e, Point Target, ref int x, ref int y, ref bool targeted, ref bool Click)
        {
            return DrawVideos(videos, e, Target, ref x, ref y, ref targeted, ref Click, videos.Count);
        }
        
        public List<Video> DrawVideos(List<Video> videos, Graphics e, Point Target, ref int x, ref int y, ref bool targeted, ref bool Click, int Count)
        {
            if (Count > videos.Count) Count = videos.Count;
            e.SmoothingMode = SmoothingMode.HighSpeed;
            
            int temp = 0;
            List<Video> DrawedVideos = new List<Video>();

            for (int i = 0; i < Count; i++)
            {
                if (y + videos[i].Scale.Height >= 0 && y < MainPage.Height)
                {
                    DrawControl(Target, videos[i], e, x, y, Click, ref targeted);
                    DrawedVideos.Add(videos[i]);
                }
                temp = y + videos[i].Scale.Height + 5;
                x += videos[i].Scale.Width + 5;
                if (x + videos[i].Scale.Width > MainPage.Width) { x = 5; y += videos[i].Scale.Height + 5; }
            }
            
            y = temp;
            return DrawedVideos;
        }

        public virtual void Redraw(Graphics e, Point Target, bool Click)
        {

        }
        public void PostDraw(Graphics e)
        {
            TargetedObject?.PostDraw(lastPoint.X, lastPoint.Y, e);
        }
    }
}
