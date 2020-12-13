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
        public void PlaceTestVertical()
        {
            // Arrange
            int size = 3;
            Boat boat = new Boat(2);
            StartPosition startPosition = new StartPosition(0, 0, false);
            Field[,] fields = new Field[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    fields[i, j] = new Field("", true, false);
                }
            }
            // Act
            boat.Place(fields, startPosition);
            // Assert
            Assert.AreEqual(fields[0, 0].Icon, "BoatEndUp");
            Assert.AreEqual(fields[0, 1].Icon, "BoatEndDown");
        }

        [TestMethod()]
        public void PlaceTestHorizontal()
        {
            // Arrange
            int size = 3;
            Boat boat = new Boat(2);
            StartPosition startPosition = new StartPosition(0, 0, true);
            Field[,] fields = new Field[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    fields[i, j] = new Field("", true, false);
                }
            }
            // Act
            boat.Place(fields, startPosition);
            // Assert
            Assert.AreEqual(fields[0, 0].Icon, "BoatEndLeft");
            Assert.AreEqual(fields[1, 0].Icon, "BoatEndRight");
        }
    }
}