using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiTube_Offline.UniControls
{
    public class Text : UniControl
    {
        public StringFormat StringFormat { get; set; } = new StringFormat();
        public string Value;
        public int Size;
        public Brush ForeColor = null;

        public bool isRectangle = false;

        public Text(string text, int size)
        {
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;

            Scale = new Size(9999, size * 2);
            NextLine = true;
            Value = text;
            Size = size;
        }
        public override void Draw(int x, int y, Graphics e)
        {
            if (isRectangle)
            {
                var rectangle = new Rectangle(x, y, Scale.Width, Scale.Height);
                e.DrawString(Value, new Font("Segoe UI", Size), ForeColor != null ? ForeColor : new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), rectangle, StringFormat);
            }
            else
                e.DrawString(Value, new Font("Segoe UI", Size), ForeColor != null ? ForeColor : new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y, StringFormat);
        }
    }
}
