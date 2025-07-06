using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiTube_Offline.UniControls
{
    public class Button : Text
    {
        public Button(string text, int size, int symbolSize = 16) : base(text, symbolSize)
        {
            Scale = new Size(size, symbolSize * 2);
            NextLine = false;
            StringFormat.Alignment = StringAlignment.Near;
            StringFormat.LineAlignment = StringAlignment.Near;
        }
        public override void Draw(int x, int y, Graphics e)
        {
            e.FillRectangle(new SolidBrush(ThemeSystem.CurrentTheme["VideoColor"]), x, y, Scale.Width, Scale.Height);
            if (isTargeted) e.DrawRectangle(ThemeSystem.IsBlack ? Pens.LimeGreen : Pens.Blue, x, y, Scale.Width - 1, Scale.Height - 1);
            base.Draw(x, y, e);
        }
    }
}
