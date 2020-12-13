using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace BattleshipBooster.Interfaces
{
	public interface IExportService
	{
		/// <summary>
		/// Saves the play field as an image file
		/// </summary>
		/// <param name="grid">Grid to draw from</param>
		/// <param name="isSolution">Should it export the solution or the riddle (only affects "Save as" dialog text)</param>
		/// <param name="playFieldId">Id to preset file name</param>
		void SaveAsPNG(Grid grid, bool isSolution, string playFieldId);
	}
}
