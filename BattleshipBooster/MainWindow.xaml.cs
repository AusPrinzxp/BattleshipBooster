using Businesslogic;
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

            for (int x = 0; x < playField.fields.GetLength(1); x++)
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

            for (int y = 0; y < playField.fields.GetLength(0); y++)
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

                for (int x = 0; x < playField.fields.GetLength(1); x++)
                {
                    Label label = new Label();
                    label.Width = 100;
                    label.Height = 100;
                    label.BorderThickness = new Thickness(3);
                    label.BorderBrush = Brushes.DarkGray;

                    if (!playField.fields[y, x].isBoat)
                    {
                        label.Background = playField.fields[y, x].isBoat ? Brushes.Black : Brushes.LightSkyBlue;
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
    }
}
