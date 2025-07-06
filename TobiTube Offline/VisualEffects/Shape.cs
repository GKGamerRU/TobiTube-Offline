using System.Drawing.Drawing2D;

namespace TobiTube_Offline.VisualEffects
{
    public class Shape
    {
        public static GraphicsPath GetRoundedRectagle(int x, int y, int width, int height, float radius = 10 * 2F)
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
    }
}
