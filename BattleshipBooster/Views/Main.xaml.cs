using BattleshipBooster.Models;
using BattleshipBooster.ViewModels;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BattleshipBooster.Views
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class Main : Window
  {
        public Main()
        {
            InitializeComponent();
			DrawField();
		}

        public void DrawField()
        {
            PlayField playField = (DataContext as MainViewModel).PlayField;

            PlayFieldPanel.Children.Clear();

            for (int y = 0; y < playField.Size; y++)
            {
                StackPanel row = new StackPanel();
                row.Orientation = Orientation.Horizontal;
                PlayFieldPanel.Children.Add(row);

                for (int x = 0; x < playField.Size; x++)
                {
                    Label label = new Label();
                    label.Width = 100;
                    label.Height = 100;
                    label.BorderThickness = new Thickness(3);
                    label.BorderBrush = Brushes.DarkGray;

                    if (!playField.Fields[y, x].IsBoat)
                    {
                        label.Background = playField.Fields[y, x].IsBoat ? Brushes.Black : Brushes.LightSkyBlue;
                    }

                    row.Children.Add(label);
                }
            }
        }

        private void GenerateNew_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).GenerateNew();
            DrawField();
        }

		private void SmallSize_Click(object sender, RoutedEventArgs e)
		{
            (DataContext as MainViewModel).ResizePlayField(5);
            DrawField();
        }

		private void MeduimSize_Click(object sender, RoutedEventArgs e)
		{
            (DataContext as MainViewModel).ResizePlayField(6);
            DrawField();
        }

		private void LargeSize_Click(object sender, RoutedEventArgs e)
		{
            (DataContext as MainViewModel).ResizePlayField(7);
            DrawField();
        }

        private void SaveRiddleAsPNG(Grid grid)
        {
            Size size = new Size(grid.ActualWidth, grid.ActualHeight);

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingvisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingvisual.RenderOpen())
            {
                drawingContext.DrawRectangle(new VisualBrush(grid), null, new Rect(new Point(), size));
                drawingContext.Close();
            }

            renderTargetBitmap.Render(drawingvisual);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save the Riddle";
            saveFileDialog.FileName = "Riddle";
            saveFileDialog.ShowDialog();

            PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
            {
                bitmapEncoder.Save(fs);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.SaveRiddleAsPNG(PlayFieldGrid);
        }
    }
}
