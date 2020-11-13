using Businesslogic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            playField.Generate();
            CreatePlayFieldElements();
        }

        public void CreatePlayFieldElements()
        {
            PlayFieldPanel.Children.Clear();

            for (int y = 0; y < playField.Fields.GetLength(0); y++)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                PlayFieldPanel.Children.Add(panel);

                for (int x = 0; x < playField.Fields.GetLength(1); x++)
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

                    panel.Children.Add(label);
                }
            }
        }

        private void GenerateNew_Click(object sender, RoutedEventArgs e)
        {
            playField.Generate();
            CreatePlayFieldElements();
        }

        private void SaveRiddleAsPNG(Window view)
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
            Window mainWindow = Application.Current.MainWindow;
            this.SaveRiddleAsPNG(mainWindow);
        }
    }
}
