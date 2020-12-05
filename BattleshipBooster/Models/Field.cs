using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Models
{
	public class Field
	{
		public bool IsBoat { get; set; }
		public string Icon { get; set; }

		public Field(string icon, bool isBoat = false)
		{
			Icon = icon;
			IsBoat = isBoat;
		}
	}
}
