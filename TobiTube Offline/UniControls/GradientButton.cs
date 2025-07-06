using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobiTube_Offline.VisualEffects;

namespace TobiTube_Offline.UniControls
{
    public class GradientButton : Text
    {
        LinearGradientBrush backColor;
        GraphicsPath form;

        Bitmap Content;
        Graphics drawer;

        bool isThemeColor = false;

        Func<Color> MainColor;
        public GradientButton(string text, int size, Color color) : base(text, 16)
        {
            Scale = new Size(size, 32);
            NextLine = false;
            isThemeColor = true;

            MainColor = delegate{ return color; };
            isRectangle = true;
            ApplyControl();
        }
        public GradientButton(string text, int size, Func<Color> color) : base(text, 16)
        {
            Scale = new Size(size, 32);
            NextLine = false;
            isThemeColor = true;

            MainColor = color;
            isRectangle = true;
            ApplyControl();
        }
        public GradientButton(string text, int size, LinearGradientBrush color) : base(text, 16)
        {
            Scale = new Size(size, 32);
            NextLine = false;

            backColor = color;
            isRectangle = true;
            ApplyControl();
        }
        public GradientButton(string text, Size size, LinearGradientBrush color) : base(text, 16)
        {
            Scale = size;
            NextLine = false;

            backColor = color;
            isRectangle = true;
            ApplyControl();
        }
        public override void Draw(int x, int y, Graphics e)
        {
            e.SmoothingMode = SmoothingMode.AntiAlias;
            if (isWhite != ThemeSystem.IsBlack) { isWhite = ThemeSystem.IsBlack; ApplyControl(); }
            e.DrawImageUnscaled(Content, x, y);

            if (isTargeted) e.FillPath(new SolidBrush(Color.FromArgb(100, Color.Silver)), Shape.GetRoundedRectagle(x, y, Scale.Width, Scale.Height));
        }
        public void ApplyControl()
        {
            form = Shape.GetRoundedRectagle(0, 0, Scale.Width, Scale.Height);
            Content = new Bitmap(Scale.Width, Scale.Height);
            drawer = Graphics.FromImage(Content);
            drawer.SmoothingMode = SmoothingMode.AntiAlias;

            if(isThemeColor) backColor = new LinearGradientBrush(Point.Empty, new PointF(0, Scale.Height * 2f), ThemeSystem.CurrentTheme["BackColor"], MainColor());
            
            drawer.FillPath(backColor, form);
            drawer.DrawPath(new Pen(ThemeSystem.CurrentTheme["BackColor"]), form);
            base.Draw(0, 0, drawer);
        }
    }
}
