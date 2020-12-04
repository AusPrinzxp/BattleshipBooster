using Businesslogic;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BattleshipBooster
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
        PlayField playField = new PlayField(6);

        public MainWindow()
        {
            InitializeComponent();

            DrawField();
        }

        public void DrawField()
        {
            PlayFieldPanel.Children.Clear();

            StackPanel alphabetIndexPanel = new StackPanel();
            alphabetIndexPanel.Orientation = Orientation.Horizontal;
            PlayFieldPanel.Children.Add(alphabetIndexPanel);

            Label alphaLabel = new Label();
            alphaLabel.Width = 50;
            alphaLabel.Height = 50;
            alphaLabel.FontSize = 20;
            alphaLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            alphaLabel.VerticalContentAlignment = VerticalAlignment.Center;

            alphabetIndexPanel.Children.Add(alphaLabel);

            for (int x = 0; x < playField.Size; x++)
            {
                alphaLabel = new Label();
                alphaLabel.Width = 100;
                alphaLabel.Height = 50;
                alphaLabel.FontSize = 20;
                alphaLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                alphaLabel.VerticalContentAlignment = VerticalAlignment.Center;
                alphaLabel.Content = ((char)(x + 65)).ToString();

                alphabetIndexPanel.Children.Add(alphaLabel);
            }

            for (int y = 0; y < playField.Size; y++)
            {
                StackPanel row = new StackPanel();
                row.Orientation = Orientation.Horizontal;
                PlayFieldPanel.Children.Add(row);

                Label numberLabel = new Label();
                numberLabel.Width = 50;
                numberLabel.Height = 100;
                numberLabel.FontSize = 20;
                numberLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                numberLabel.VerticalContentAlignment = VerticalAlignment.Center;
                numberLabel.Content = (y + 1).ToString();

                row.Children.Add(numberLabel);

                for (int x = 0; x < playField.Size; x++)
                {
                    Image image = new Image();
                    image.Width = 100;
                    image.Height = 100;

                    if (playField.Fields[x, y].IsBoat)
                    {
                        image.Source = new BitmapImage(new Uri(@"Icons/BoatSingle.png", UriKind.Relative));
                    } else
                    {
                        int random = new Random().Next(4);
                        string imagePath = random == 0 ? @"Icons/Wave.png" : @"Icons/Water.png";
                        image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    }

                    row.Children.Add(image);
                }
            }
        }

        private void GenerateNew_Click(object sender, RoutedEventArgs e)
        {
            playField.Generate();
            DrawField();
        }

		private void SmallSize_Click(object sender, RoutedEventArgs e)
		{
            playField = new PlayField(5);
            playField.Generate();
            DrawField();
        }

		private void MeduimSize_Click(object sender, RoutedEventArgs e)
		{
            playField = new PlayField(6);
            playField.Generate();
            DrawField();
        }

		private void LargeSize_Click(object sender, RoutedEventArgs e)
		{
            playField = new PlayField(7);
            playField.Generate();
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.SaveRiddleAsPNG(PlayFieldGrid);
        }
    }
}
