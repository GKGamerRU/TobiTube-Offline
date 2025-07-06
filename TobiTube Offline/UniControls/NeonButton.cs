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
    public class NeonButton : Picture
    {
        public NeonButton(int x, int y) : base(new Bitmap(x, y, PixelFormat.Format32bppPArgb))
        {
            Graphics graphics = Graphics.FromImage(Texture);

            // задаем фоновый цвет и заливаем прямоугольник 
            graphics.Clear(Color.Black);
            graphics.FillRectangle(Brushes.Black, 0, 0, 200, 100);

            // создаем LinearGradientBrush для создания градиента 
            LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(200, 100), Color.FromArgb(255, 0, 234, 255), Color.FromArgb(255, 255, 0, 234));

            // задаем Blend для создания плавного перехода между цветами 
            Blend blend = new Blend();
            blend.Positions = new[] { 0f, 0.5f, 1f };
            blend.Factors = new[] { 0f, 1f, 0f };
            brush.Blend = blend;

            // рисуем прямоугольник с градиентом 
            graphics.FillRectangle(brush, 5, 5, 190, 90);

            // создаем новый Brush для рисования внутреннего свечения 
            SolidBrush glowBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
            // белый цвет с прозрачностью 
            Rectangle glowRect = new Rectangle(20, 20, 160, 60);

            // создаем новый PathGradientBrush для создания свечения внутри прямоугольника 
            PathGradientBrush pathGradientBrush = new PathGradientBrush(new[] { new Point(glowRect.Left, glowRect.Top), new Point(glowRect.Right, glowRect.Top), new Point(glowRect.Right, glowRect.Bottom), new Point(glowRect.Left, glowRect.Bottom) }, WrapMode.TileFlipX);
            pathGradientBrush.CenterPoint = new PointF(glowRect.Left + glowRect.Width / 2f, glowRect.Top + glowRect.Height / 2f);
            pathGradientBrush.CenterColor = Color.White;
            pathGradientBrush.SurroundColors = new[] { Color.Transparent };

            // рисуем свечение внутри прямоугольника 
            graphics.FillRectangle(pathGradientBrush, glowRect);
        }
    }
}
