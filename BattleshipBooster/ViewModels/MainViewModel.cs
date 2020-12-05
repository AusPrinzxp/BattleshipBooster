using BattleshipBooster.Models;
using BattleshipBooster.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace BattleshipBooster.ViewModels
{
	public class MainViewModel : BindableBase
    {
        // services
        private Generator generator;
        private Export export;

        private PlayField _playField;
        public PlayField PlayField
        {
            get => _playField;
            set => SetProperty(ref _playField, value);
        }

        private bool _showSolution;
        public bool ShowSolution
		{
            get => _showSolution;
            set => SetProperty(ref _showSolution, value);
		}

        public MainViewModel(Generator generator, Export export)
        {
            this.generator = generator;
            this.export = export;
            this.ShowSolution = false;
            ResizePlayField(6);
        }

        public void ResizePlayField(int size)
		{
			PlayField = new PlayField(size);
            GenerateNew();
        }

        public void GenerateNew()
		{
            PlayField.Fields = generator.Generate(PlayField.Size);
        }

        public void Export(Grid grid)
		{
            export.SaveAsPNG(grid, ShowSolution, PlayField.Id);
		}
    }
}
