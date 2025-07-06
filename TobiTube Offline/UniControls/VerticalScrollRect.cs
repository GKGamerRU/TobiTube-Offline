using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiTube_Offline.UniControls
{
    public class VerticalScrollRect : UniControl
    {
        public float Max = 100;

        public float Value = 0;
        float pointerHeight = 50;
        float scrollPosition = -1;

        public Point Target = Point.Empty;
        public bool Click = false;

        public VerticalScrollRect(int height)
        {
            Scale = new Size(20, height);
            NextLine = false;
        }
        public override void Draw(int x, int y, Graphics e)
        {
            e.FillRectangle(new SolidBrush(ThemeSystem.CurrentTheme["VideoColor"]), x, y, Scale.Width, Scale.Height);

            if (TobiTubeAPI.CurrentPage.isMouseDOwn && isTargeted)
            {
                float top = Target.Y - pointerHeight / 2;
                if (top < y) top = y;
                if (top > y + Scale.Height - pointerHeight) top = y + Scale.Height - pointerHeight;

                scrollPosition = top;
                top -= y;
                float result = Max / (Scale.Height - pointerHeight) * top;
                Value = (int)result;
            }
            if (scrollPosition == -1) scrollPosition = y;
            e.FillRectangle(Brushes.Gray, x + 3, scrollPosition, Scale.Width - 6, pointerHeight);
        }
        void ChangeScrollPosition()
        {

        }
        public override bool OnScroll(int value)
        {
            Value += value;
            if (Value > Max) Value = Max;
            if (Value < 0) Value = 0;

            return true;
        }
    }
}
