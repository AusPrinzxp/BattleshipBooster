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
            this.Size = size;
            this.Fields = new Field[size, size];

			Generate();
		}

        public void Generate()
        {
            Reset();

			foreach (Boat boat in GetBoatsFromConfig())
			{
				StartPosition[] possibleStartPositions = GetPossibleBoatStartPositions(boat.Length);

				if (possibleStartPositions.Length == 0)
				{
                    return;
			    } else
				{
                    StartPosition placePosition = possibleStartPositions[new Random().Next(0, possibleStartPositions.Length)];

                    boat.Place(Fields, placePosition);
				}
			}
		}

        private void Reset()
		{
            for (int col = 0; col < Size; col++)
            {
                for (int row = 0; row < Size; row++)
                {
                    Fields[col, row] = new Field(false);
                }
            }
        }

        private Boat[] GetBoatsFromConfig()
		{
			// placeholder: later read config out of json file depending on the PlayFieldSize
            if (Size == 5)
			{
                return new Boat[] { new Boat(3), new Boat(2), new Boat(1), new Boat(1) };
            } else if (Size == 6)
			{
			    return new Boat[] { new Boat(3), new Boat(2), new Boat(2), new Boat(1), new Boat(1) };
			} else
			{
                return new Boat[] { new Boat(3), new Boat(2), new Boat(2), new Boat(2), new Boat(1), new Boat(1), new Boat(1) };
            }
		}

		private StartPosition[] GetPossibleBoatStartPositions(int boatLength)
        {
            List<StartPosition> positions = new List<StartPosition>();

            // all horizontal positions
            for (int col = 0; col < Size - boatLength + 1; col++)
            {
                for (int row = 0; row < Size; row++)
                {
                    StartPosition startPosition = new StartPosition(col, row, true);
                    if (IsPossiblePosition(startPosition, boatLength))
					{
                        positions.Add(startPosition);
					}
                }
            }

            // all vertical positions
            for (int col = 0; col < Size; col++)
            {
                for (int row = 0; row < Size - boatLength + 1; row++)
                {
                    StartPosition startPosition = new StartPosition(col, row, false);
                    if (IsPossiblePosition(startPosition, boatLength))
                    {
                        positions.Add(startPosition);
                    }
                }
            }

            return positions.ToArray();
        }

        private bool IsPossiblePosition(StartPosition checkPosition, int boatLength)
        {
            for (int i = 0; i < boatLength; i++)
			{
                int xPos = checkPosition.X + i * Convert.ToInt32(checkPosition.IsHorizontal);
                int yPos = checkPosition.Y + i * Convert.ToInt32(!checkPosition.IsHorizontal);

                if (Fields[xPos, yPos].IsBoat)
				{
                    return false;
				}

                if (!(checkPosition.IsHorizontal ? HasGapsHorizontally(xPos, yPos, i == 0, i == boatLength - 1) : HasGapsVertically(xPos, yPos, i == 0, i == boatLength - 1)))
				{
                    return false;
				}
			}

            return true;
        }

        private bool HasGapsHorizontally(int x, int y, bool isStart, bool isEnd)
        {
            if (y < Size -1 && Fields[x, y + 1].IsBoat || y > 0 && Fields[x, y - 1].IsBoat)
			{
                return false;
			}

            if (x > 0 && isStart && (Fields[x - 1, y].IsBoat || y < Size -1 && Fields[x - 1, y + 1].IsBoat || y > 0 && Fields[x - 1, y - 1].IsBoat))
			{
                return false;
			}

            if (x < Size -1 && isEnd && (Fields[x + 1, y].IsBoat || y < Size -1 && Fields[x + 1, y + 1].IsBoat || y > 0 && Fields[x + 1, y - 1].IsBoat))
            {
                return false;
            }

            return true;
        }

        private bool HasGapsVertically(int x, int y, bool isStart, bool isEnd)
        {
            if (x < Size -1 && Fields[x + 1, y].IsBoat || x > 0 && Fields[x - 1, y].IsBoat)
            {
                return false;
            }

            if (y > 0 && isStart && (Fields[x, y - 1].IsBoat || x < Size -1 && Fields[x + 1, y - 1].IsBoat || x > 0 && Fields[x - 1, y - 1].IsBoat))
            {
                return false;
            }

            if (y < Size -1 && isEnd && (Fields[x, y + 1].IsBoat || x < Size -1 && Fields[x + 1, y + 1].IsBoat || x > 0 && Fields[x - 1, y + 1].IsBoat))
            {
                return false;
            }

            return true;
        }
    }
}
