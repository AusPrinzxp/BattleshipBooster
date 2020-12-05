using BattleshipBooster.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Models
{
	public class PlayField
	{
		public string Id { get; set; }
		public int Size { get; set; }
		public Field[,] Fields { get; set; }

		public PlayField(int size)
		{
			// generate id from current datetime
			string timeNow = DateTime.Now.ToBinary().ToString();
			Id = timeNow.Substring(timeNow.Length - 6);

			Size = size;
			Fields = new Field[size, size];
		}
	}
}
