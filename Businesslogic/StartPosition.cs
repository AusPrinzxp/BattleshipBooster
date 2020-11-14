using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslogic
{
    public class StartPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHorizontal { get; set; }

        public StartPosition(int x, int y, bool isHorizontal)
        {
            this.X = x;
            this.Y = y;
            this.IsHorizontal = isHorizontal;
        }
    }
}
