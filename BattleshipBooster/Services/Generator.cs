using BattleshipBooster.Interfaces;
using BattleshipBooster.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipBooster.Services
{
	public class Generator: IGeneratorService
	{
        private const int maxGenerateTryIterations = 10;
        private int generateTryIterations = 0;

        private Field[,] fields;
        private int size;

        // summary in interface
        public Field[,] Generate(int size, PlayFieldConfig config)
        {
            this.size = size;
            fields = InitPlayField(size);

            // place boats from config
            foreach (Boat boat in config.Boats)
            {
                StartPosition[] possibleStartPositions = GetPossibleBoatStartPositions(boat.Length);

                if (possibleStartPositions.Length == 0)
                {
                    if (generateTryIterations < maxGenerateTryIterations)
					{
                        generateTryIterations++;
                        Generate(size, config);
					} else
					{
                        generateTryIterations = 0;
					}

                    return fields;
                }
                else
                {
                    StartPosition placePosition = possibleStartPositions[new Random().Next(0, possibleStartPositions.Length)];
                    boat.Place(fields, placePosition);
                }
            }

            // make tiles visible from config
            MakeTilesVisible(config.BoatTileShowCount, true);
            MakeTilesVisible(config.WaterTileShowCount, false);

            generateTryIterations = 0;
            return fields;
        }

        /// <summary>
        /// Inits play field with invisible water fields
        /// </summary>
        /// <param name="size">Size of play field</param>
        /// <returns>Play field</returns>
        private Field[,] InitPlayField(int size)
        {
            Field[,] fields = new Field[size, size];

            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row < size; row++)
                {
                    bool isWave = new Random().Next(4) == 0;
                    string icon = isWave ? "Wave" : "Water";

                    fields[col, row] = new Field(icon, false, false);
                }
            }

            return fields;
        }

        /// <summary>
        /// Get all possible boat positions of the play field
        /// </summary>
        /// <param name="boatLength">Length of the boat to test possible positions</param>
        /// <returns>All possible start positions (start position and orientation of the boat)</returns>
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

        /// <summary>
        /// Checks if the startposition of the boat is possible
        /// </summary>
        /// <param name="checkPosition">Start position to check</param>
        /// <param name="boatLength">Boat length to check</param>
        /// <returns>If its possible to place boat on this position</returns>
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

        /// <summary>
        /// Checks if the field has no boats around itself (horitontally)
        /// </summary>
        /// <param name="x">Field x position</param>
        /// <param name="y">Fild y position</param>
        /// <param name="isStart">Is boat start tile</param>
        /// <param name="isEnd">Is boat end tile</param>
        /// <returns>Has gaps between boat tile</returns>
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

        /// <summary>
        /// Checks if the field has no boats around itself (vertically)
        /// </summary>
        /// <param name="x">Field x position</param>
        /// <param name="y">Fild y position</param>
        /// <param name="isStart">Is boat start tile</param>
        /// <param name="isEnd">Is boat end tile</param>
        /// <returns>Has gaps between boat tile</returns>
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

        /// <summary>
        /// Make random fields visible
        /// </summary>
        /// <param name="tileCount">How many tiles should be made visible</param>
        /// <param name="isBoat">Make boat tiles or water tiles visible</param>
        private void MakeTilesVisible(int tileCount, bool isBoat)
		{
            Field[] tiles = fields.Cast<Field>().Where(field => field.IsBoat == isBoat).ToArray();

            for (int i = 0; i < tileCount; i++)
            {
                Field[] hiddenTiles = tiles.Where(tile => !tile.IsVisible).ToArray();
				hiddenTiles[new Random().Next(hiddenTiles.Length)].IsVisible = true;
            }
        }
    }
}
