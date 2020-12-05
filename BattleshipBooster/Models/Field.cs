using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Models
{
	public class Field
	{
		public bool IsBoat { get; set; }

		public Field(bool isBoat = false)
		{
			IsBoat = isBoat;
		}
	}
}
