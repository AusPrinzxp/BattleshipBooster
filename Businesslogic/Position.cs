using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslogic
{
    public class StartPosition
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool isHorizontal { get; set; }

        public StartPosition(int x, int y, bool isHorizontal)
        {
            this.x = x;
            this.y = y;
            this.isHorizontal = isHorizontal;
        }
    }
}
