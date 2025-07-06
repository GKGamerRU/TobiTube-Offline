using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiTube_Offline.UniControls
{
    public class Toogle : Text
    {
        public bool Checked = false;
        public Toogle(string text, int size) : base(text, 16)
        {
            StringFormat.Alignment = StringAlignment.Near;
            StringFormat.LineAlignment = StringAlignment.Near;
            Scale = new Size(size, 32);
            NextLine = false;
            OnClick += delegate { Checked = !Checked; };
        }
        public override void Draw(int x, int y, Graphics e)
        {
            e.FillEllipse(new SolidBrush(ThemeSystem.CurrentTheme["VideoColor"]), x + 5, y + 5, 20, 20);
            if (isTargeted) e.DrawEllipse(new Pen(ThemeSystem.CurrentTheme["TextColor"]), x + 5, y + 5, 19, 19);
            if (Checked) e.FillEllipse(Brushes.LimeGreen, x + 7, y + 7, 15, 15);
            base.Draw(x + 25, y, e);
        }
    }
}
