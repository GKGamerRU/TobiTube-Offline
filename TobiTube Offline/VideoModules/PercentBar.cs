using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TobiTube_Offline.VideoModules
{
    public class PercentBar
    {
        public event Action<int> OnValueChanged = delegate { };
        PictureBox control;

        public float Value = 100;
        public float MaxValue = 100;

        float percent => control.Width / (float)MaxValue * Value;
        
        Pen backLine = new Pen(Brushes.Gray, 5);
        Pen line = new Pen(Brushes.GhostWhite, 5);
        Point cursorPosition = Point.Empty;

        bool isHover = false;

        public PercentBar(PictureBox area)
        {
            control = area;
            control.Paint += Control_Paint;

            control.MouseDown += Control_MouseDown;
            control.MouseUp += Control_MouseUp;
            control.MouseMove += Control_MouseMove;
            control.MouseLeave += Control_MouseLeave;
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            cursorPosition = Point.Empty;
            isHover = false;
            Draw();
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown) Value = MaxValue / control.Width * e.X; cursorPosition = e.Location;
            if (Value < 0) Value = 0; if (Value > MaxValue) Value = MaxValue;
            OnValueChanged((int)Value);

            isHover = true;
            cursorPosition = e.Location;
            Draw();
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            cursorPosition = Point.Empty;
            OnValueChanged((int)Value);
            Draw();
        }

        bool isDown = false;
        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            Value = MaxValue / control.Width * e.X;
            Draw();
        }

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            float pos = percent - (control.Height / control.Width * percent);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.DrawLine(backLine, 0, control.Height / 2, control.Width, control.Height / 2);
            e.Graphics.DrawLine(line, 0, control.Height / 2, pos, control.Height / 2);

            e.Graphics.FillRectangle(Brushes.GhostWhite, pos - control.Height / 4, 3, control.Height / 2, control.Height - 6);
        }

        public void Draw()
        {
            control.Invalidate();
        }
        public void Update(int value)
        {
            if (!isDown)
            {
                Value = value;
                if (Value < 0) Value = 0; if (Value > MaxValue) Value = MaxValue;

                OnValueChanged((int)Value);
            }
            Draw();
        }
    }
}
