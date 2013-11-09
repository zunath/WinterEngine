using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class PositionEventArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PositionEventArgs(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
