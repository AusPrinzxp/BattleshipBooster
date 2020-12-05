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
	public class Export
	{
        public void SaveAsPNG(Grid grid)
        {
            Size size = new Size(grid.ActualWidth, grid.ActualHeight);

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height), 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingvisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingvisual.RenderOpen())
            {
                drawingContext.DrawRectangle(new VisualBrush(grid), null, new Rect(new System.Windows.Point(), size));
                drawingContext.Close();
            }

            renderTargetBitmap.Render(drawingvisual);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save the Riddle";

            string timeNow = DateTime.Now.ToBinary().ToString();
            saveFileDialog.FileName = "Riddle-" + timeNow.Substring(timeNow.Length - 6);

            saveFileDialog.ShowDialog();

            PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
            {
                bitmapEncoder.Save(fs);
            }
        }
    }
}
