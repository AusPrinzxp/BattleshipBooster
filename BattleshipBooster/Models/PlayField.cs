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
		public int[] ColumnBoatCounts { get; set; }
		public int[] RowBoatCounts { get; set; }

		public PlayField(int size)
		{
			// generate id from current datetime
			string timeNow = DateTime.Now.ToBinary().ToString();
			Id = timeNow.Substring(timeNow.Length - 6);

			Size = size;
			Fields = new Field[size, size];
		}

		public void CalcBoatCounts()
		{
			ColumnBoatCounts = new int[Size];
			RowBoatCounts = new int[Size];

			for (int x = 0; x < Size; x++)
			{
				for (int y = 0; y < Size; y++)
				{
					if (Fields[x, y].IsBoat)
					{
						ColumnBoatCounts[x]++;
						RowBoatCounts[y]++;
					}
				}
			}
		}
	}
}
