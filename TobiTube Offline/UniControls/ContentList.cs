using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiTube_Offline.UniControls
{
    public class ContentList : UniControl
    {
        public List<UniControl> Elements = new List<UniControl>();
        public VerticalScrollRect rect = new VerticalScrollRect(100);

        GraphicsPath form;
        Bitmap Content;
        Graphics drawer;

        public Point Target = Point.Empty;
        public bool Click = false;

        public string Title = "";

        public ContentList(string text, int x, int y)
        {
            Scale = new Size(x, y);
            rect.Scale.Height = y - 30;
            NextLine = false;
            Title = text;
            ApplyControl();
        }
        public override bool OnScroll(int value)
        {
            rect.OnScroll(value);
            return true;
        }
        public override void Draw(int x, int y, Graphics e)
        {
            int oldX = x, oldY = y;
            e.SmoothingMode = SmoothingMode.AntiAlias;
            if (isWhite != ThemeSystem.IsBlack) { isWhite = ThemeSystem.IsBlack; ApplyControl(); }
            e.DrawImageUnscaled(Content, x, y);

            var temp = e.Clip;
            e.Clip = new Region(new Rectangle(x, y + 30, Scale.Width, Scale.Height - 30));

            if (Elements.Count > 0)
                rect.Max = Elements.Count * (Elements[0].Scale.Height + 3) - Scale.Height + 50;
            else
                rect.Max = 0;

            if (rect.Max < 0) rect.Max = 0;

            x += 5;
            y += 35 - (int)rect.Value;
            int minPosition = oldY + 30, maxPosition = oldY + Scale.Height;

            foreach (var element in Elements)
            {
                if (y >= minPosition && y < maxPosition - element.Scale.Height)
                    TobiTubeAPI.CurrentPage.DrawControl(Target, element, e, x, y, Click, ref isTargeted);
                else
                    element.Draw(x, y, e);

                if (element.isTargeted)
                {
                    e.FillRectangle(new SolidBrush(Color.FromArgb(80, Color.IndianRed)), x, y, element.Scale.Width, element.Scale.Height);
                }
                y += element.Scale.Height + 3;
            }
            rect.Target = Target;

            TobiTubeAPI.CurrentPage.DrawControl(Target, rect, e, oldX + Scale.Width - rect.Scale.Width, oldY + 30, rect.Click, ref rect.isTargeted);

            x = oldX; y = oldY;
            e.Clip = temp;
        }
        public GraphicsPath GetPath(int x, int y, int width, int height, float radius = 10 * 2F)
        {
            width--;
            height--;
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius;
            path.StartFigure();

            path.AddArc(x, y, curveSize, curveSize, 180, 90);
            path.AddArc(x + width - curveSize, y, curveSize, curveSize, 270, 90);
            path.AddArc(x + width - curveSize, y + height - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(x, y + height - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        public void ApplyControl()
        {
            form = GetPath(0, 0, Scale.Width, Scale.Height);
            Content = new Bitmap(Scale.Width, Scale.Height);
            drawer = Graphics.FromImage(Content);
            drawer.SmoothingMode = SmoothingMode.AntiAlias;

            drawer.DrawPath(new Pen(ThemeSystem.CurrentTheme["VideoColor"]), form);

            drawer.Clip = new Region(form);
            drawer.FillRectangle(new SolidBrush(ThemeSystem.CurrentTheme["VideoColor"]), 0, 0, Scale.Width, 30);
            drawer.DrawString(Title, new Font("Segoe UI", 12), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), 10, 5);
        }
    }
}
