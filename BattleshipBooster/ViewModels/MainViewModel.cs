using BattleshipBooster.Models;
using BattleshipBooster.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Linq;

namespace BattleshipBooster.ViewModels
{
	public class MainViewModel : BindableBase
    {
        // services
        private GeneratorService generator;
        private ExportService export;
        private PlayFieldConfigService config;

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

        private int _longBoatCount;
        public int LongBoatCount
        {
            get => _longBoatCount;
            set => SetProperty(ref _longBoatCount, value);
        }

        private int _mediumBoatCount;
        public int MediumBoatCount
		{
            get => _mediumBoatCount;
            set => SetProperty(ref _mediumBoatCount, value);
		}

        private int _shortBoatCount;
        public int ShortBoatCount
		{
            get => _shortBoatCount;
            set => SetProperty(ref _shortBoatCount, value);
		}

        public MainViewModel(GeneratorService generator, ExportService export, PlayFieldConfigService config)
        {
            this.generator = generator;
            this.export = export;
            this.config = config;
            this.ShowSolution = false;
            ResizePlayField(6);
        }

        /// <summary>
        /// Resizes the play field
        /// </summary>
        /// <param name="size">New size</param>
        public void ResizePlayField(int size)
		{
			PlayField = new PlayField(size);
            GenerateNew();
        }

        /// <summary>
        /// Generates a new riddle with the generator service
        /// </summary>
        public void GenerateNew()
		{
            PlayFieldConfig playFieldConfig = config.GetPlayFieldConfig(PlayField.Size);
            LongBoatCount = playFieldConfig.Boats.Where(boat => boat.Length == 3).Count();
            MediumBoatCount = playFieldConfig.Boats.Where(boat => boat.Length == 2).Count();
            ShortBoatCount = playFieldConfig.Boats.Where(boat => boat.Length == 1).Count();

            PlayField.Fields = generator.Generate(PlayField.Size, playFieldConfig);
            PlayField.CalcBoatCounts();
        }

        /// <summary>
        /// Exports the play field as a PNG file
        /// </summary>
        /// <param name="grid">Grid from view to draw PNG</param>
        public void Export(Grid grid)
		{
            export.SaveAsPNG(grid, ShowSolution, PlayField.Id);
		}
    }
}