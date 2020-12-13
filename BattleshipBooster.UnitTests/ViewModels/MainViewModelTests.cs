using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleshipBooster.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using BattleshipBooster.Services;
using BattleshipBooster.Models;
using System.Windows.Controls;

namespace BattleshipBooster.ViewModels.Tests
{
    [TestClass()]
    public class MainViewModelTests
    {
        [TestMethod()]
        public void MainViewModelTest()
        {
            // Arrange
            Generator generator = new Generator();
            Export export = new Export();
            Config config = new Config();

            // Act
            MainViewModel mainViewModel = new MainViewModel(generator, export, config); ;


            // Assert
            Assert.AreEqual(mainViewModel.PlayField.Size, 6);
            Assert.AreEqual(CountBoatTilesInField(6, mainViewModel.PlayField.Fields), 9);
        }

        [TestMethod()]
        public void ResizePlayFieldTest()
        {
            // Arrange
            int size = 5;
            Generator generator = new Generator();
            Export export = new Export();
            Config config = new Config();
            MainViewModel mainViewModel = new MainViewModel(generator, export, config);

            // Act
            mainViewModel.ResizePlayField(size);

            // Assert
            Assert.AreEqual(mainViewModel.PlayField.Size, size);
            Assert.AreEqual(CountBoatTilesInField(size, mainViewModel.PlayField.Fields), 7);
        }

        [TestMethod()]
        public void GenerateNewTest()
        {
            // Arrange
            int size = 7;
            Generator generator = new Generator();
            Export export = new Export();
            Config config = new Config();
            MainViewModel mainViewModel = new MainViewModel(generator, export, config);
            mainViewModel.PlayField = new PlayField(7);

            // Act
            mainViewModel.GenerateNew();

            // Assert
            Assert.AreEqual(mainViewModel.PlayField.Size, size);
            Assert.AreEqual(CountBoatTilesInField(size, mainViewModel.PlayField.Fields), 12);
        }

        private int CountBoatTilesInField(int size, Field[,] fields)
        {
            int boatCount = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (fields[i, j].IsBoat)
                    {
                        boatCount++;
                    }
                }
            }

            return boatCount;
        }
    }
}