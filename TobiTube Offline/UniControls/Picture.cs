using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiTube_Offline.UniControls
{
    public class Picture : UniControl
    {
        Bitmap texture;
        public Bitmap Texture
        {
            get { return texture; }
            set
            {
                texture = new Bitmap(Scale.Width, Scale.Height, PixelFormat.Format32bppPArgb);
                Graphics e = Graphics.FromImage(texture);
                e.SmoothingMode = SmoothingMode.HighQuality;
                e.DrawImage(value, 0, 0, Scale.Width, Scale.Height);
            }
        }

        public Picture(Bitmap source)
        {
            Scale = new Size(source.Width, source.Height);
            Texture = source;
        }
        public Picture(Bitmap source, int x, int y)
        {
            Scale = new Size(x, y);
            Texture = source;
        }
        public override void Draw(int x, int y, Graphics e)
        {
            if (texture != null) e.DrawImageUnscaled(texture, x, y);
            base.Draw(x, y, e);
        }
    }
}
