using System.Drawing;
using System.Drawing.Drawing2D;

namespace TobiTube_Offline.VisualEffects
{
    public static class Shadow
    {
        public static Bitmap Create(Size scale)
        {
            var highlight = new Bitmap(scale.Width + 60, scale.Height + 60);
            var gfx = Graphics.FromImage(highlight);
            gfx.SmoothingMode = SmoothingMode.AntiAlias;

            var pen = new Pen(new SolidBrush(Color.FromArgb(30, Color.Silver)), 20);
            for (int j = 0; j < 20; j += 1)
            {
                gfx.DrawPath(pen, Shape.GetRoundedRectagle(30, 30, scale.Width, scale.Height));
                pen.Width /= 1.25f;
            }

            gfx.FillPath(Brushes.Transparent, Shape.GetRoundedRectagle(30, 30, scale.Width, scale.Height));
            return highlight;
        }
    }
}
