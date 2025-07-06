using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TobiTube_Offline.VideoModules
{
    public class TimeBar
    {
        public event Action<float> OnTimeChanged = delegate { };
        PictureBox control;

        public float Value = 0;
        public float MaxValue = 10;

        float percent => control.Width / MaxValue * Value;

        int seconds => (int)Value / 1000; 
        public int Minutes => seconds / 60;
        public int Hours => Minutes / 60;
        public int Seconds => seconds % 60;

        Pen backLine = new Pen(Brushes.Gray, 3);
        Pen line = new Pen(Brushes.GhostWhite, 3);
        Pen Selectedline = new Pen(Brushes.LightPink, 3);
        Point cursorPosition = Point.Empty;

        bool isHover = false;

        public TimeBar(PictureBox area)
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
            
            isHover = true;
            cursorPosition = e.Location;
            Draw();
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            cursorPosition = Point.Empty;
            OnTimeChanged(Value);
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
            if(isHover) e.Graphics.DrawLine(Selectedline, pos, control.Height / 2, cursorPosition.X, control.Height / 2);

            e.Graphics.FillEllipse(Brushes.GhostWhite, pos - control.Height / 2, 0, control.Height - 1, control.Height - 1);
        }

        public void Draw()
        {
            control.Invalidate();
        }
        public void Update(float value)
        {
           if(!isDown) Value = value;
           Draw();
        }
    }
}
