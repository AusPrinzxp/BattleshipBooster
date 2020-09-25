using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BattleshipBooster
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
        public MainWindow()
        {
            InitializeComponent();

            CreateGrid(6);
        }

        public void CreateGrid(int size)
        {
            PlayFieldGrid.ShowGridLines = true;

            for (int i = 0; i < size; i++)
            {
                PlayFieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
                PlayFieldGrid.RowDefinitions.Add(new RowDefinition());
            }
        }
    }
}
