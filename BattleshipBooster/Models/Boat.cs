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
			for (int i = 0; i < this.Length; i++)
			{
				Field field = placePosition.IsHorizontal ?
					fields[placePosition.X + i, placePosition.Y] : fields[placePosition.X, placePosition.Y + i];

				field.IsBoat = true;
				SetFieldIcon(field, placePosition.IsHorizontal, i == 0, i == (this.Length - 1));
			}
		}

		private void SetFieldIcon(Field field, bool isHorizontal, bool isBoatStart, bool isBoatEnd)
		{
			if (this.Length == 1)
			{
				field.Icon = "BoatSingle";
			}
			else if (isBoatStart) {
				field.Icon = isHorizontal ? "BoatEndLeft" : "BoatEndUp";
			}
			else if (isBoatEnd)
			{
				field.Icon = isHorizontal ? "BoatEndRight" : "BoatEndDown";
			}
			else {
				field.Icon = "BoatMiddle";
			}
		}
	}
}
