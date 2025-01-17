using ContainerShip.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ContainerShip.Tests.Classes
{
    [TestFixture]
    public class StackTests
    {
        private Stack _stack;

        [SetUp]
        public void Setup()
        {
            _stack = new Stack(isFirstRow: true);
        }

        [Test]
        public void AddContainer_ShouldPreventStackingAboveValuableContainer()
        {
            // Arrange
            var valuableContainer = new ValuableContainer(10);
            var containerAbove = new RegularContainer(10);

            // Act
            bool addedValuable = _stack.AddContainer(valuableContainer);
            bool addedAbove = _stack.AddContainer(containerAbove);

            // Assert
            Assert.That(addedValuable, Is.True, "Valuable container should be added successfully.");
            Assert.That(addedAbove, Is.False, "Should not allow adding container above a valuable container.");
        }

        [Test]
        public void AddContainer_ShouldAllowValuableContainersOnOthers()
        {
            // Arrange
            var container = new RegularContainer(10);
            var valuableContainer = new ValuableContainer(10);

            // Act
            bool addedRegular = _stack.AddContainer(container);
            bool addedValuable = _stack.AddContainer(valuableContainer);

            // Assert
            Assert.That(addedRegular, Is.True, "Regular container should be added successfully.");
            Assert.That(addedValuable, Is.True, "Valuable container should be added on top of regular container.");
        }

        [Test]
        public void CanAddContainer_ShouldEnforceCoolingRequirement()
        {
            // Arrange
            var coolableContainer = new CoolableContainer(10);
            var nonFirstRowStack = new Stack(isFirstRow: false);
            var regularContainer = new RegularContainer(10);

            // Act
            bool addedCoolable = _stack.AddContainer(coolableContainer);
            bool addedNonFirstRow = nonFirstRowStack.AddContainer(coolableContainer);
            bool addedRegular = _stack.AddContainer(regularContainer);

            // Assert
            Assert.That(addedCoolable, Is.True, "Coolable container should be added to first row.");
            Assert.That(addedNonFirstRow, Is.False, "Coolable container should not be added to non-first row.");
            Assert.That(addedRegular, Is.True, "Regular container should be added successfully.");
        }

        [Test]
        public void AddContainer_ShouldPreventMultipleValuablesInSameStack()
        {
            // Arrange
            var valuableContainer1 = new ValuableContainer(10);
            var valuableContainer2 = new ValuableContainer(10);

            // Act
            bool firstAdd = _stack.AddContainer(valuableContainer1);
            bool secondAdd = _stack.AddContainer(valuableContainer2);

            // Assert
            Assert.That(firstAdd, Is.True, "First valuable container should be added successfully.");
            Assert.That(secondAdd, Is.False, "Should not allow adding another valuable container to the same stack.");
        }

        [Test]
        public void AddContainer_ShouldAllowNonValuableContainersUpToMaxWeight()
        {
            // Arrange
            var container1 = new RegularContainer(20);
            var container2 = new RegularContainer(20);

            // Act
            bool added1 = _stack.AddContainer(container1);
            bool added2 = _stack.AddContainer(container2);

            // Assert
            Assert.That(added1, Is.True, "First regular container should be added successfully.");
            Assert.That(added2, Is.True, "Second regular container should be added successfully without exceeding max weight.");
        }
    }
} 