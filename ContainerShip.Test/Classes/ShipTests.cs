using ContainerShip.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerShip.Tests.Classes
{
    [TestFixture]
    public class ShipTests
    {
        private Ship _ship;

        [SetUp]
        public void Setup()
        {
            _ship = new Ship(4, 2); // Length=4, Width=2
        }

        [Test]
        public void ShipBalance_ShouldBeWithin20Percent()
        {
            // Arrange
            var leftContainers = new List<Container>
            {
                new RegularContainer(10),
                new RegularContainer(10),
                new RegularContainer(10)
            };

            var rightContainers = new List<Container>
            {
                new RegularContainer(10)
            };

            // Act
            _ship.PlaceContainers(leftContainers);
            _ship.PlaceContainers(rightContainers);

            var totalWeight = _ship.Rows.Sum(r => r.TotalWeight);
            var leftWeight = _ship.Rows[0].TotalWeight; // Assuming first row is left
            var rightWeight = _ship.Rows[1].TotalWeight; // Assuming second row is right

            var difference = Math.Abs(leftWeight - rightWeight);
            var allowedDifference = 0.2 * totalWeight;

            // Assert
            Assert.That(difference, Is.LessThanOrEqualTo(allowedDifference), "Ship is not balanced within 20% difference.");
        }

        [Test]
        public void PlaceContainers_ShouldNotAddWhenShipIsFull()
        {
            // Arrange
            // Fill the ship
            var containers = new List<Container>();
            for(int i =0; i < _ship.Width; i++)
            {
                var row = _ship.Rows[i];
                for(int j=0; j< row.Stacks.Count; j++)
                {
                    for(int k=0; k < 10; k++)
                    {
                        containers.Add(new RegularContainer(10));
                    }
                }
            }
            _ship.PlaceContainers(containers);

            // Act
            bool added = _ship.AddContainer(new RegularContainer(10));

            // Assert
            Assert.That(added, Is.False, "Should not add container when the ship is full.");
        }
    }
} 