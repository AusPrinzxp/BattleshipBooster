using BattleshipBooster.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Interfaces
{
	public interface IGeneratorService
	{
		/// <summary>
		/// Generates a play field
		/// </summary>
		/// <param name="size">Affects the size of the field</param>
		/// <param name="config">Config to generate boats and visible fields</param>
		/// <returns>Generated play field as two dimensional array of Field</returns>
		Field[,] Generate(int size, PlayFieldConfig config);
	}
}
