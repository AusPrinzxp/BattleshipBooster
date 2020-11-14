using System;
using System.Collections.Generic;

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
            /*
             *  for (let boat of boatsOfConfig) {
             *      StartPosition[] possibleStartPositions = GetPossibleBoatStartPositions(boat.length);
             *      
             *      if (possiblePositions.length == 0) {
             *          throw new Error("Bitte es gaaad ned");
             *          return;
             *      }
             *      
             *      
             *  }
             */
        }

        private StartPosition[] GetPossibleBoatStartPositions(int boatLength)
        {
            List<StartPosition> positions = new List<StartPosition>();

            /*
             *  // all horizontal boats
             *  for (let column of Fields.GetLength(0) - boatLength + 1) {
             *      for (let row of Fields.GetLength(1)) {
             *          IsPossiblePosition(new StartPosition(column, row, true));
             *      }
             *  }
             *  
             *  // all vertical boats
             *  for (let columns of Fields.GetLength(0)) {
             *      for (let field of Fields.GetLength(1) - boatLength + 1) {
             *  ...
             *  
             *  the same loop as above => extract into seperate method
             */

            return positions.ToArray();
        }

        private bool IsPossiblePosition(StartPosition checkPosition)
        {
            return false;
        }
    }
}
