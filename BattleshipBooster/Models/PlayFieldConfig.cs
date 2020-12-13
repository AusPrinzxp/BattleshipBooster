using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Models
{
	public class PlayFieldConfig
	{
		public Boat[] Boats { get; set; }

		// How many tiles should be visible in the riddle
		public int BoatTileShowCount { get; }
		public int WaterTileShowCount { get; }

		public PlayFieldConfig(Boat[] boats, int boatTileShowCount, int waterTileShowCount)
		{
			Boats = boats;
			BoatTileShowCount = boatTileShowCount;
			WaterTileShowCount = waterTileShowCount;
		}
	}
}
