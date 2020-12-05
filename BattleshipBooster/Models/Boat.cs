using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Models
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
				Field field = fields[placePosition.X + i, placePosition.Y];
				field.IsBoat = true;

				SetFieldIcon(field, false, i == 0, i == (this.Length - 1));
			}
		}

		private void PlaceVertically(Field[,] fields, StartPosition placePosition)
		{
			for (int i = 0; i < this.Length; i++)
			{
				Field field = fields[placePosition.X, placePosition.Y + i];
				field.IsBoat = true;

				SetFieldIcon(field, true, i == 0, i == (this.Length -1));
			}
		}

		private void SetFieldIcon(Field field, bool isVertical, bool isBoatStart, bool isBoatEnd)
		{
			if (this.Length == 1)
			{
				field.Icon = "BoatSingle";
			}
			else if (isBoatStart) {
				field.Icon = isVertical ? "BoatEndUp" : "BoatEndLeft";
			}
			else if (isBoatEnd)
			{
				field.Icon = isVertical ? "BoatEndDown" : "BoatEndRight";
			}
			else {
				field.Icon = "BoatMiddle";
			}
		}
	}
}
