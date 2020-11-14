using System;
using System.Collections.Generic;

namespace Businesslogic
{
    public class PlayField
    {
        public Field[,] fields { get; set; }
        public int size { get; set; }

        public PlayField(int size)
        {
            this.size = size;
            this.fields = new Field[size, size];

			Generate();
		}

        public void Generate()
        {
            ResetPlayField();

			foreach (Boat boat in GetBoatsFromConfig())
			{
				StartPosition[] possibleStartPositions = GetPossibleBoatStartPositions(boat.length);

				if (possibleStartPositions.Length == 0)
				{
					throw new Exception("Generation failed");
			    } else
				{
                    StartPosition placePosition = possibleStartPositions[new Random().Next(0, possibleStartPositions.Length)];

                    // place boat => extract into boat class: boat.Place(fields)

                    if (placePosition.isHorizontal)
					{
                        for (int i = 0; i < boat.length; i++)
					    {
                            fields[placePosition.x + i, placePosition.y].isBoat = true;
					    }
                    } else
					{
                        for (int i = 0; i < boat.length; i++)
                        {
                            fields[placePosition.x, placePosition.y + i].isBoat = true;
                        }
                    }
				}
			}
		}

        private Boat[] GetBoatsFromConfig()
		{
			// placeholder: later read config out of json file depending on the PlayFieldSize
			return new Boat[] { new Boat(3), new Boat(2), new Boat(2), new Boat(1), new Boat(1) };
		}

        private void ResetPlayField()
		{
            for (int col = 0; col < fields.GetLength(0); col++)
            {
                for (int row = 0; row < fields.GetLength(1); row++)
                {
                    fields[col, row] = new Field(false);
                }
            }
        }

		private StartPosition[] GetPossibleBoatStartPositions(int boatLength)
        {
            List<StartPosition> positions = new List<StartPosition>();

            // all horizontal positions
            for (int col = 0; col < fields.GetLength(0) - boatLength + 1; col++)
            {
                for (int row = 0; row < fields.GetLength(1); row++)
                {
                    StartPosition startPosition = new StartPosition(col, row, true);
                    if (IsPossiblePosition(startPosition, boatLength))
					{
                        positions.Add(startPosition);
					}
                }
            }

            // !!! Doubled code => extract into seperate method !!!

            // all vertical positions
            for (int col = 0; col < fields.GetLength(0); col++)
            {
                for (int row = 0; row < fields.GetLength(1) - boatLength + 1; row++)
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
            bool isPossible = true;
            // for boatlenght check pos
            // if horizontal or not
            // check if is not boat
            for (int i = 0; i < boatLength; i++)
			{
                if (checkPosition.isHorizontal)
				{
                    if (fields[checkPosition.x + i, checkPosition.y].isBoat)
					{
                        return false;
					}
				} else
				{
                    if (fields[checkPosition.x, checkPosition.y + i].isBoat)
					{
                        return false;
					}
				}
			}

            return isPossible;
        }
    }
}
