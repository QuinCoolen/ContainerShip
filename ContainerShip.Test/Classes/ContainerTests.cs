using ContainerShip.Classes;
using NUnit.Framework;
using System;

namespace ContainerShip.Tests.Classes
{
    [TestFixture]
    public class ContainerTests
    {
        [Test]
        public void RegularContainer_Creation_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            int weight = 20;
            var container = new RegularContainer(weight);

            // Act & Assert
            Assert.That(container.Type, Is.EqualTo(ContainerType.Regular), "Container type should be Regular.");
            Assert.That(container.IsValuable, Is.False, "Regular container should not be valuable.");
            Assert.That(container.RequiresCooling, Is.False, "Regular container should not require cooling.");
            Assert.That(container.Weight, Is.EqualTo(24), "Container weight should be initialized correctly."); // 4 + 20
        }

        [Test]
        public void ValuableCoolableContainer_Creation_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            int weight = 25;
            var container = new ValuableCoolableContainer(weight);

            // Act & Assert
            Assert.That(container.Type, Is.EqualTo(ContainerType.ValuableCoolable), "Container type should be ValuableCoolable.");
            Assert.That(container.IsValuable, Is.True, "ValuableCoolable container should be valuable.");
            Assert.That(container.RequiresCooling, Is.True, "ValuableCoolable container should require cooling.");
            Assert.That(container.Weight, Is.EqualTo(29), "Container weight should be initialized correctly."); // 4 + 25
        }

        [Test]
        public void Container_ShouldThrowException_WhenWeightExceedsMax()
        {
            // Arrange, Act & Assert
            var ex = Assert.Throws<Exception>(() => new RegularContainer(40), "Should throw exception when container weight exceeds maximum.");
            Assert.That(ex.Message, Is.EqualTo("Container is too heavy"), "Exception message should be 'Container is too heavy'.");
        }

        [Test]
        public void Container_ShouldSetCorrectTypeBasedOnConstructor()
        {
            // Arrange & Act
            var regular = new RegularContainer(10);
            var coolable = new CoolableContainer(10);
            var valuable = new ValuableContainer(10);
            var valuableCoolable = new ValuableCoolableContainer(10);

            // Assert
            Assert.That(regular.Type, Is.EqualTo(ContainerType.Regular), "Container type should be Regular.");
            Assert.That(coolable.Type, Is.EqualTo(ContainerType.Coolable), "Container type should be Coolable.");
            Assert.That(valuable.Type, Is.EqualTo(ContainerType.Valuable), "Container type should be Valuable.");
            Assert.That(valuableCoolable.Type, Is.EqualTo(ContainerType.ValuableCoolable), "Container type should be ValuableCoolable.");
        }
    }
} 