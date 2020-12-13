using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleshipBooster.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipBooster.Models.Tests
{
    [TestClass()]
    public class BoatTests
    {
        [TestMethod()]
        public void PlaceTest()
        {
            // Arrange
            int size = 3;
            Boat boat = new Boat(2);
            StartPosition verticalStartPosition = new StartPosition(0, 0, false);
            Field[,] fields = new Field[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    fields[i, j] = new Field("", true, false);
                }
            }
            // Act
            boat.Place(fields, verticalStartPosition);
            // Assert
            Assert.AreEqual(fields[0, 0].Icon, "BoatEndUp");
            Assert.AreEqual(fields[0, 1].Icon, "BoatEndDown");
        }
    }
}