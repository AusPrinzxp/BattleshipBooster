using System;

namespace Businesslogic
{
    public class PlayField
    {
        public Field[,] Fields { get; set; }
        public int Size { get; set; }

        public PlayField(int size)
        {
            Size = size;
            Fields = new Field[Size, Size];
        }

        public void Generate()
        {
            for (int y = 0; y < Fields.GetLength(0); y++)
            {
                for (int x = 0; x < Fields.GetLength(1); x++)
                {
                    bool isBoat = new Random().Next(0, 2) == 1;

                    Fields[y, x] = new Field(isBoat);
                }
            }
        }
    }
}
