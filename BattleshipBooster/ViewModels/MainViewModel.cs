using BattleshipBooster.Models;
using BattleshipBooster.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleshipBooster.ViewModels
{
	public class MainViewModel : BindableBase
    {
        // services
        private Generator generator;

        private PlayField _playField;
        public PlayField PlayField
        {
            get => _playField;
            set => SetProperty(ref _playField, value);
        }

        public MainViewModel(Generator generator)
        {
            this.generator = generator;
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
    }
}
