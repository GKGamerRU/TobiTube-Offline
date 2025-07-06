using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TobiTube_Offline
{
    public static class ThemeSystem
    {
        public static Action<Dictionary<string, Color>> OnColorChange = delegate { };
        public static bool IsBlack { get; private set; }
        public static Dictionary<string, Color> CurrentTheme { get; set; }

        public static void ChangeColor(bool black)
        {
            IsBlack = black;
            if (black)
                CurrentTheme = BlackTheme;
            else
                CurrentTheme = WhiteTheme;

            OnColorChange(CurrentTheme);
        }

        public static Dictionary<string, Color> BlackTheme = new Dictionary<string, Color>
        {
            {"BackColor", Color.FromArgb(25,25,25)},
            {"HeadColor", Color.FromArgb(40,40,40)},
            {"BlockColor",Color.FromArgb(25,25,25)},
            {"VideoColor",Color.FromArgb(50,50,50)},
            {"TextColor", Color.LightGray},
        };

        public static Dictionary<string, Color> WhiteTheme = new Dictionary<string, Color>
        {
            {"BackColor", Color.WhiteSmoke},
            {"HeadColor", Color.White},
            {"BlockColor",Color.WhiteSmoke},
            {"VideoColor",Color.FromArgb(200,200,200)},
            {"TextColor", Color.FromArgb(100,100,100)},
        };
    }
}
