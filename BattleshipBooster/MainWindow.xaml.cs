using Businesslogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
    }
}
