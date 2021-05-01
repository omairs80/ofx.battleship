using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ofx.Battleship.Domain.Entities;
using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Domain.Test
{
    public class BattleShipTests
    {
        [Test]
        [TestCase(1, 1, 5, true)]
        [TestCase(1, 1, 5, false)]
        [TestCase(1, 5, 6, true)]
        [TestCase(5, 1, 6, false)]
        public void CreateBattleship_Test(int row, int column, int size, bool isHorizontal)
        {
            // Act
            var battleShip = BattleShip.CreateBattleship(1, row, column, size, isHorizontal);
            
            // Assert
            battleShip.Should().NotBeNull();
            battleShip.PlayerId.Should().Be(1);
            battleShip.StartingRow.Should().Be(row);
            battleShip.StartingColumn.Should().Be(column);
            battleShip.Size.Should().Be(size);
            battleShip.IsHorizontal.Should().Be(isHorizontal);
            battleShip.Health.Should().Be(battleShip.Size);
            battleShip.IsSunk.Should().BeFalse();
            battleShip.Coordinates.Count().Should().Be(battleShip.Size);
        }

        [Test]
        [TestCase(0)]
        [TestCase(11)]
        public void CreateBattleship_Test_With_Invalid_Size(int size)
        {
            // Act & Assert
            Assert.Throws<InvalidBattleShipException>(() => BattleShip.CreateBattleship(1, 1, 1, size, true),
                "Battleship's size should be >= 1 and <= 10");
        }

        [Test]
        [TestCase(0)]
        [TestCase(11)]
        public void CreateBattleship_Test_With_Invalid_StartingRow(int row)
        {
            // Act & Assert
            Assert.Throws<InvalidBattleShipException>(() => BattleShip.CreateBattleship(1, row, 1, 1, true),
                "Battleship's starting row position should be >= 1 and <= 10");
        }

        [Test]
        [TestCase(0)]
        [TestCase(11)]
        public void CreateBattleship_Test_With_Invalid_StartingColumn(int column)
        {
            // Act & Assert
            Assert.Throws<InvalidBattleShipException>(() => BattleShip.CreateBattleship(1, 1, column, 1, true),
                "Battleship's starting column position should be >= 1 and <= 10");
        }

        [Test]
        [TestCase(5, 7)]
        [TestCase(9, 3)]
        public void CreateBattleship_Test_With_Invalid_StartingPositions_With_Horizontal_Alignment(int column, int size)
        {
            // Act & Assert
            Assert.Throws<InvalidBattleShipException>(() => BattleShip.CreateBattleship(1, 1, column, size, true),
                "Battleship cannot be placed at this position. Due to the ship's position and size, it will exceed the board's boundaries");
        }

        [Test]
        [TestCase(5, 7)]
        [TestCase(9, 3)]
        public void CreateBattleship_Test_With_Invalid_StartingPositions_With_Vertical_Alignment(int row, int size)
        {
            // Act & Assert
            Assert.Throws<InvalidBattleShipException>(() => BattleShip.CreateBattleship(1, row, 1, size, false),
                "Battleship cannot be placed at this position. Due to the ship's position and size, it will exceed the board's boundaries");
        }

        [Test]
        [TestCase(5, 1, 1, 4, false)]
        [TestCase(1, 1, 1, 0, true)]
        public void TakeAttack_Test(int size, int attackAtRow, int attackAtColumn, int expectedHealth, bool isSunk)
        {
            // Arrange
            var battleShip = BattleShip.CreateBattleship(1, 1, 1, size, true);

            // Act
            battleShip.TakeAttack(Coordinate.CreateCoordinate(attackAtRow, attackAtColumn));

            // Act
            battleShip.Health.Should().Be(expectedHealth);
            battleShip.IsSunk.Should().Be(isSunk);
        }

        [Test]
        public void TakeAttack_MultipleTimes_Without_Ship_Sinking()
        {
            // Arrange
            var battleShip = BattleShip.CreateBattleship(1, 1, 1, 3, true);

            // Act
            battleShip.TakeAttack(Coordinate.CreateCoordinate(1, 1));   // Hit
            battleShip.TakeAttack(Coordinate.CreateCoordinate(1, 2));   // Hit
            battleShip.TakeAttack(Coordinate.CreateCoordinate(2, 1));   // Miss
            battleShip.TakeAttack(Coordinate.CreateCoordinate(5, 5));   // Miss

            // Act
            battleShip.Health.Should().Be(1);
            battleShip.IsSunk.Should().BeFalse();
        }

        [Test]
        public void TakeAttack_MultipleTimes_With_The_Ship_Sinking()
        {
            // Arrange
            var battleShip = BattleShip.CreateBattleship(1, 1, 1, 3, true);

            // Act
            battleShip.TakeAttack(Coordinate.CreateCoordinate(1, 1));   // Hit
            battleShip.TakeAttack(Coordinate.CreateCoordinate(1, 2));   // Hit
            battleShip.TakeAttack(Coordinate.CreateCoordinate(2, 1));   // Miss
            battleShip.TakeAttack(Coordinate.CreateCoordinate(5, 5));   // Miss
            battleShip.TakeAttack(Coordinate.CreateCoordinate(1, 3));   // Hit
            battleShip.TakeAttack(Coordinate.CreateCoordinate(1, 3));   // Hit

            // Act
            battleShip.Health.Should().Be(0);
            battleShip.IsSunk.Should().BeTrue();
        }
    }
}
