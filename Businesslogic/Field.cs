using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslogic
{
    public class Field
    {
        public bool IsBoat { get; set; }

        public Field(bool isBoat)
        {
            IsBoat = isBoat;
        }
    }
}
