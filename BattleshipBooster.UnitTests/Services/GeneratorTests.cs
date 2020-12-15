using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleshipBooster.Services;
using System;
using System.Collections.Generic;
using System.Text;
using BattleshipBooster.Models;

namespace BattleshipBooster.Services.Tests
{
    [TestClass()]
    public class GeneratorTests
    {
        [TestMethod()]
        public void GenerateTest()
        {
            // Arrange
            int size = 6;
            GeneratorService generator = new GeneratorService();
            PlayFieldConfigService config = new PlayFieldConfigService();
            Field[,] fields = new Field[size, size];

            // Act
            Field[,] generatedFields = generator.Generate(size, config.GetPlayFieldConfig(size));

            // Assert
            Assert.AreEqual(CountBoatTilesInField(size, generatedFields), 9);
        }

        private int CountBoatTilesInField(int size, Field[,] fields)
        {
            int boatCount = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(fields[i,j].IsBoat)
                    {
                        boatCount++;
                    }
                }
            }

            return boatCount;
        }
    }
}