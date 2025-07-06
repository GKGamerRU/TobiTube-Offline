using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiTube_Offline.UniControls
{
    public abstract class UniControl
    {
        public Size Scale = new Size();
        public bool NextLine = false, isWhite;
        public string Name;
        public Action OnClick = delegate { };
        public bool isTargeted = false;

        public virtual void Draw(int x, int y, Graphics e)
        {

        }
        public virtual void PostDraw(int x, int y, Graphics e)
        {

        }
        public virtual void BackDraw(int x, int y, Graphics e)
        {

        }
        public virtual bool OnScroll(int value)
        {
            return false;
        }
    }
}
