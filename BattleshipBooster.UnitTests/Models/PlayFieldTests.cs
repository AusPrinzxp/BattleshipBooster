using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleshipBooster.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Models.Tests
{
    [TestClass()]
    public class PlayFieldTests
    {
        [TestMethod()]
        public void CalcBoatCountsTest()
        {
            // Arrange
            const int size = 3;
            bool boat = false;
            PlayField playField = new PlayField(size);
            playField.Fields = new Field[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    playField.Fields[i,j] = new Field("", true, boat);

                    boat = !boat;
                }
            }

            // Act
            playField.CalcBoatCounts();

            // Assert
            CollectionAssert.AreEqual(playField.ColumnBoatCounts, new int[size] { 1, 2, 1 });
            CollectionAssert.AreEqual(playField.RowBoatCounts, new int[size] { 1, 2, 1 });
        }
    }
}