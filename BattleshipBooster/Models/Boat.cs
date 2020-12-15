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

		/// <summary>
		/// Places the boat (itself) on the play field
		/// </summary>
		/// <param name="fields">Play field to place on</param>
		/// <param name="placePosition">Start position of boat with x, y and direction (horizontal or vertical)</param>
		public void Place(Field[,] fields, StartPosition placePosition)
		{
			for (int i = 0; i < this.Length; i++)
			{
				Field field = placePosition.IsHorizontal ?
					fields[placePosition.X + i, placePosition.Y] : fields[placePosition.X, placePosition.Y + i];

				field.IsBoat = true;
				field.Icon = GetFieldIcon(placePosition.IsHorizontal, i == 0, i == (this.Length - 1));
			}
		}

		/// <summary>
		/// Sets the name of the correct image to display
		/// </summary>
		/// <param name="field">Field to set image</param>
		/// <param name="isHorizontal">Is the boat oriented horizontally</param>
		/// <param name="isBoatStart">Is field start tile of the boat</param>
		/// <param name="isBoatEnd">Is field end tile of the boat</param>
		private string GetFieldIcon(bool isHorizontal, bool isBoatStart, bool isBoatEnd)
		{
			if (this.Length == 1)
			{
				return "BoatSingle";
			}
			else if (isBoatStart) {
				return isHorizontal ? "BoatEndLeft" : "BoatEndUp";
			}
			else if (isBoatEnd)
			{
				return isHorizontal ? "BoatEndRight" : "BoatEndDown";
			}
			else {
				return "BoatMiddle";
			}
		}
	}
}
