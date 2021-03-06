﻿using BattleshipBooster.Interfaces;
using BattleshipBooster.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Services
{
	public class PlayFieldConfigService : IPlayFieldConfigService
	{
		// summary in interface
		public PlayFieldConfig GetPlayFieldConfig(int size)
		{
			return size switch
			{
				5 => new PlayFieldConfig(new Boat[] { new Boat(3), new Boat(2), new Boat(1), new Boat(1) }, 1, 2),
				6 => new PlayFieldConfig(new Boat[] { new Boat(3), new Boat(2), new Boat(2), new Boat(1), new Boat(1) }, 2, 3),
				7 => new PlayFieldConfig(new Boat[] { new Boat(3), new Boat(2), new Boat(2), new Boat(2), new Boat(1), new Boat(1), new Boat(1) }, 3, 4),
				_ => new PlayFieldConfig(new Boat[0], 0, 0),
			};
		}
	}
}
