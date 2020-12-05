using BattleshipBooster.Models;
using System;
using System.Collections.Generic;

namespace BattleshipBooster.Services
{
	public class Generator
	{
        private const int maxGenerateTryIterations = 10;
        private int generateTryIterations = 0;

        private Field[,] fields;
        private int size;

        public Field[,] Generate(int size)
        {
            this.size = size;
            fields = InitPlayField(size);

            foreach (Boat boat in GetBoatsFromConfig(size))
            {
                StartPosition[] possibleStartPositions = GetPossibleBoatStartPositions(boat.Length);

                if (possibleStartPositions.Length == 0)
                {
                    if (generateTryIterations < maxGenerateTryIterations)
					{
                        generateTryIterations++;
                        Generate(size);
					} else
					{
                        generateTryIterations = 0;
                        return fields;
					}
                }
                else
                {
                    StartPosition placePosition = possibleStartPositions[new Random().Next(0, possibleStartPositions.Length)];
                    boat.Place(fields, placePosition);
                }
            }

            generateTryIterations = 0;
            return fields;
        }

        private Field[,] InitPlayField(int size)
        {
            Field[,] fields = new Field[size, size];

            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row < size; row++)
                {
                    bool isWave = new Random().Next(4) == 0;
                    string icon = isWave ? "Wave" : "Water";
                    bool isVisible = new Random().Next(6) == 0;

                    fields[col, row] = new Field(icon, isVisible, false);
                }
            }

            return fields;
        }

        private Boat[] GetBoatsFromConfig(int size)
        {
			return size switch
			{
				5 => new Boat[] { new Boat(3), new Boat(2), new Boat(1), new Boat(1) },
				6 => new Boat[] { new Boat(3), new Boat(2), new Boat(2), new Boat(1), new Boat(1) },
				7 => new Boat[] { new Boat(3), new Boat(2), new Boat(2), new Boat(2), new Boat(1), new Boat(1), new Boat(1) },
				_ => new Boat[0],
			};
		}

        private StartPosition[] GetPossibleBoatStartPositions(int boatLength)
        {
            List<StartPosition> positions = new List<StartPosition>();

            // all horizontal positions
            for (int col = 0; col < size - boatLength + 1; col++)
            {
                for (int row = 0; row < size; row++)
                {
                    StartPosition startPosition = new StartPosition(col, row, true);
                    if (IsPossiblePosition(startPosition, boatLength))
                    {
                        positions.Add(startPosition);
                    }
                }
            }

            // all vertical positions
            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row < size - boatLength + 1; row++)
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

                if (fields[xPos, yPos].IsBoat)
                    return false;

                if (!(checkPosition.IsHorizontal ? HasGapsHorizontally(xPos, yPos, i == 0, i == boatLength - 1) : HasGapsVertically(xPos, yPos, i == 0, i == boatLength - 1)))
                    return false;
            }

            return true;
        }

        private bool HasGapsHorizontally(int x, int y, bool isStart, bool isEnd)
        {
            if (y < size - 1 && fields[x, y + 1].IsBoat || y > 0 && fields[x, y - 1].IsBoat)
                return false;

            if (x > 0 && isStart && (fields[x - 1, y].IsBoat || y < size - 1 && fields[x - 1, y + 1].IsBoat || y > 0 && fields[x - 1, y - 1].IsBoat))
                return false;

            if (x < size - 1 && isEnd && (fields[x + 1, y].IsBoat || y < size - 1 && fields[x + 1, y + 1].IsBoat || y > 0 && fields[x + 1, y - 1].IsBoat))
                return false;

            return true;
        }

        private bool HasGapsVertically(int x, int y, bool isStart, bool isEnd)
        {
            if (x < size - 1 && fields[x + 1, y].IsBoat || x > 0 && fields[x - 1, y].IsBoat)
                return false;

            if (y > 0 && isStart && (fields[x, y - 1].IsBoat || x < size - 1 && fields[x + 1, y - 1].IsBoat || x > 0 && fields[x - 1, y - 1].IsBoat))
                return false;

            if (y < size - 1 && isEnd && (fields[x, y + 1].IsBoat || x < size - 1 && fields[x + 1, y + 1].IsBoat || x > 0 && fields[x - 1, y + 1].IsBoat))
                return false;

            return true;
        }
    }
}
