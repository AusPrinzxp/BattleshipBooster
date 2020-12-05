using BattleshipBooster.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Models
{
	public class PlayField
	{
		public int Size { get; set; }
		public Field[,] Fields { get; set; }

		public PlayField(int size)
		{
			Size = size;
			Fields = new Field[size, size];
		}
	}
}
