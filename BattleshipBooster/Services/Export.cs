using BattleshipBooster.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BattleshipBooster.Services
{
	public class Export: IExportService
	{
        public void SaveAsPNG(Grid grid, bool isSolution, string playFieldId)
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)Math.Ceiling(grid.ActualWidth), (int)Math.Ceiling(grid.ActualHeight), 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(DrawGrid(grid));

            PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (FileStream fs = (FileStream)PrepareSaveFileDialog(isSolution, playFieldId).OpenFile())
            {
				bitmapEncoder.Save(fs);
			}
		}

        /// <summary>
        /// Draws play field
        /// </summary>
        /// <param name="grid">Grid to draw from</param>
        /// <returns>Drawn play field</returns>
        private DrawingVisual DrawGrid(Grid grid)
		{
            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
				drawingContext.DrawRectangle(new VisualBrush(grid), null, new Rect(new Point(), new Size(grid.ActualWidth, grid.ActualHeight)));
            }

            return drawingVisual;
        }

        /// <summary>
        /// Prepares the save file dialog
        /// </summary>
        /// <param name="isSolution">Affects the preset file dialog title and file name</param>
        /// <param name="playFieldId">Affects the preset file name</param>
        /// <returns></returns>
        private SaveFileDialog PrepareSaveFileDialog(bool isSolution, string playFieldId)
		{
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";

            string saveType = isSolution ? "Solution" : "Riddle";

            saveFileDialog.Title = $"Save the {saveType}";
            saveFileDialog.FileName = $"{saveType}-{playFieldId}";

            saveFileDialog.ShowDialog();

            return saveFileDialog;
        }
    }
}
