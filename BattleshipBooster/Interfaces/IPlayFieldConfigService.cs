using BattleshipBooster.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Interfaces
{
	public interface IPlayFieldConfigService
	{
		/// <summary>
		/// Gets config for generating play fild
		/// </summary>
		/// <param name="size">config depends on the play field size</param>
		/// <returns></returns>
		PlayFieldConfig GetPlayFieldConfig(int size);
	}
}
