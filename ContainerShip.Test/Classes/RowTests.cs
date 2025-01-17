using ContainerShip.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ContainerShip.Tests.Classes
{
    [TestFixture]
    public class RowTests
    {
        private Row _row;

        [SetUp]
        public void Setup()
        {
            _row = new Row(length: 2); // Example with 2 stacks
        }

        [Test]
        public void AddCoolableContainer_ShouldPlaceInFirstStack()
        {
            // Arrange
            var coolableContainer = new CoolableContainer(10);

            // Act
            bool result = _row.AddContainer(coolableContainer);

            // Assert
            Assert.That(result, Is.True, "Coolable container should be added successfully.");
            Assert.That(_row.Stacks[0].Containers, Contains.Item(coolableContainer), "Coolable container should be placed in the first stack.");
        }

        [Test]
        public void AddValuableContainer_ShouldPreventMultipleValuablesInSameStack()
        {
            // Arrange
            var valuableContainer1 = new ValuableContainer(10);
            var valuableContainer2 = new ValuableContainer(10);

            // Act
            bool firstAdd = _row.AddContainer(valuableContainer1);
            bool secondAdd = _row.Stacks[0].AddContainer(valuableContainer2); // Try adding to the same stack

            // Assert
            Assert.That(firstAdd, Is.True, "First valuable container should be added successfully.");
            Assert.That(secondAdd, Is.False, "Should not allow adding multiple valuable containers to the same stack.");
        }

        [Test]
        public void AddContainer_ShouldUpdateTotalWeightCorrectly()
        {
            // Arrange
            var container1 = new RegularContainer(10);
            var container2 = new ValuableContainer(15);

            // Act
            _row.AddContainer(container1);
            _row.AddContainer(container2);

            // Calculate expected total weight
            int expectedTotalWeight = container1.Weight + container2.Weight;

            // Assert
            Assert.That(_row.TotalWeight, Is.EqualTo(expectedTotalWeight), "Total weight should be updated correctly after adding containers.");
        }

        [Test]
        public void IsEmpty_ShouldReturnTrue_WhenNoContainers()
        {
            // Act & Assert
            Assert.That(_row.IsEmpty(), Is.True, "Row should be empty when no containers are added.");
        }

        [Test]
        public void IsEmpty_ShouldReturnFalse_WhenContainersAreAdded()
        {
            // Arrange
            var container = new RegularContainer(10);

            // Act
            _row.AddContainer(container);

            // Assert
            Assert.That(_row.IsEmpty(), Is.False, "Row should not be empty when containers are added.");
        }

        [Test]
        public void IsFull_ShouldReturnFalse_WhenAnyStackIsNotFull()
        {
            // Arrange
            foreach (var stack in _row.Stacks)
            {
                for (int i = 0; i < 5; i++) // Not all stacks are full
                {
                    stack.AddContainer(new RegularContainer(10));
                }
            }

            // Act
            bool isFull = _row.IsFull();

            // Assert
            Assert.That(isFull, Is.False, "Row should not be full when any stack is not full.");
        }
    }
} 