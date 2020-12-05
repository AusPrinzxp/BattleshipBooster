using BattleshipBooster.Models;
using BattleshipBooster.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
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

        private void DrawField()
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
                    Image image = new Image();
                    image.Width = 100;
                    image.Height = 100;
                    image.Source = new BitmapImage(new Uri(@$"../Icons/{playField.Fields[x, y].Icon}.png", UriKind.Relative));

                    row.Children.Add(image);
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Export(PlayFieldGrid);
        }
    }
}
