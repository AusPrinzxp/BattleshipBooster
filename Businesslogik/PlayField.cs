using System;

namespace Businesslogik
{
    public class PlayField
    {
        public Field[] Fields { get; set; }
        public int Size { get; set; }

        public PlayField(int size)
        {
            this.Size = size;
        }

        public void Generate()
        {
            int fieldsCount = Convert.ToInt32(Math.Pow(this.Size, 2));
            this.Fields = new Field[fieldsCount];

            for (int i = 0; i < fieldsCount; i++)
            {
                this.Fields[i] = new Field();
            }
        }
    }
}
