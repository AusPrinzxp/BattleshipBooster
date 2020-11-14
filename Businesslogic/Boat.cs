using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslogic
{
	public class Boat
	{
		public int Length { get; set; }

		public Boat(int length)
		{
			this.Length = length;
		}

		public void Place(Field[,] fields, StartPosition placePosition)
		{
			if (placePosition.IsHorizontal)
			{
				PlaceHorizontally(fields, placePosition);
			} else
			{
				PlaceVertically(fields, placePosition);
			}
		}

		private void PlaceHorizontally(Field[,] fields, StartPosition placePosition)
		{
			for (int i = 0; i < this.Length; i++)
			{
				fields[placePosition.X + i, placePosition.Y].IsBoat = true;
			}
		}

		private void PlaceVertically(Field[,] fields, StartPosition placePosition)
		{
			for (int i = 0; i < this.Length; i++)
			{
				fields[placePosition.X, placePosition.Y + i].IsBoat = true;
			}
		}
	}
}
