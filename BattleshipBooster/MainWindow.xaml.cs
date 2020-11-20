using Businesslogic;
using Microsoft.Win32;
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

        private void SaveRiddleAsPNG(Grid view)
        {
            Size size = new Size(view.ActualWidth, view.ActualHeight);

            RenderTargetBitmap result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingvisual = new DrawingVisual();
            using (DrawingContext context = drawingvisual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(view), null, new Rect(new Point(), size));
                context.Close();
            }

            result.Render(drawingvisual);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save the Riddle";
            saveFileDialog.FileName = "Riddle";
            saveFileDialog.ShowDialog();

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(result));

            using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
            {
                encoder.Save(fs);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.SaveRiddleAsPNG(PlayFieldGrid);
        }
    }
}
