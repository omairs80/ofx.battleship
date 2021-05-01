using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using Ofx.Battleship.Domain.Entities;
using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Domain.Test
{
    public class BoardTests
    {
        [Test]
        [AutoData]
        public void CreateBoard_Should_CreateBoard(int playerId)
        {
            // Act
            var board = Board.CreateBoard(playerId);

            // Assert
            board.Should().NotBeNull();
            board.PlayerId.Should().Be(playerId);
        }

        [Test]
        [AutoData]
        public void AddBattleShip_Single_Ship(int playerId)
        {
            // Arrange
            var battleShip = BattleShip.CreateBattleship(playerId, 1, 1, 5, true);

            // Act & Assert
            var board = Board.CreateBoard(playerId);
            board.AddBattleShip(battleShip);
        }

        [Test]
        [AutoData]
        public void AddBattleShip_Two_Ships_At_Different_Places(int playerId)
        {
            // Arrange
            var battleShip = BattleShip.CreateBattleship(playerId, 1, 1, 5, true);
            var battleShip2 = BattleShip.CreateBattleship(playerId, 1, 6, 5, true);

            // Act & Assert
            var board = Board.CreateBoard(playerId);
            board.AddBattleShip(battleShip);
            board.AddBattleShip(battleShip2);
        }

        [Test]
        [AutoData]
        public void AddBattleShip_Two_Ships_Overlapping_Each_Other(int playerId)
        {
            // Arrange
            var battleShip = BattleShip.CreateBattleship(playerId, 1, 1, 5, true);
            var battleShip2 = BattleShip.CreateBattleship(playerId, 1, 3, 5, false);

            // Act & Assert
            var board = Board.CreateBoard(playerId);
            board.AddBattleShip(battleShip);
            Assert.Throws<BoardPositionAlreadyOccupiedException>(() => board.AddBattleShip(battleShip2));
        }

        [Test]
        [TestCase(1, 1, true)]
        [TestCase(1, 2, true)]
        [TestCase(1, 3, true)]
        [TestCase(1, 8, false)]
        [TestCase(5, 5, false)]
        public void IsPositionOccupied_Test(int row, int column, bool isOccupied)
        {
            // Arrange
            var battleShip = BattleShip.CreateBattleship(1, 1, 1, 5, true);

            // Act
            var board = Board.CreateBoard(1);
            board.AddBattleShip(battleShip);

            // Assert
            board.IsPositionOccupied(Coordinate.CreateCoordinate(row, column)).Should().Be(isOccupied);
        }

        [Test]
        [TestCase(1, 1, "miss")]
        [TestCase(5, 1, "miss")]
        [TestCase(6, 6, "miss")]
        [TestCase(10, 10, "miss")]
        public void AttackPosition_Not_Occupied(int attackAtRow, int attackAtColumn, string attackResult)
        {
            // Arrange
            var board = Board.CreateBoard(1);

            // Act
            var result = board.AttackPosition(Coordinate.CreateCoordinate(attackAtRow, attackAtColumn));

            // Assert
            result.Should().Be(attackResult);
        }

        [Test]
        [TestCase(1, 1, "hit")]
        [TestCase(1, 2, "hit")]
        [TestCase(1, 5, "hit")]
        [TestCase(5, 1, "miss")]
        [TestCase(6, 6, "miss")]
        [TestCase(10, 10, "miss")]
        public void AttackPosition_At_Occupied_Positions(int attackAtRow, int attackAtColumn, string attackResult)
        {
            // Arrange
            var board = Board.CreateBoard(1);
            var battleShip = BattleShip.CreateBattleship(1, 1, 1, 5, true);
            board.AddBattleShip(battleShip);

            // Act
            var result = board.AttackPosition(Coordinate.CreateCoordinate(attackAtRow, attackAtColumn));

            // Assert
            result.Should().Be(attackResult);
        }

        [Test]
        public void AttackPosition_To_Sink_Battleship()
        {
            // Arrange
            var board = Board.CreateBoard(1);
            var battleShip = BattleShip.CreateBattleship(1, 1, 1, 3, true);
            board.AddBattleShip(battleShip);

            // Act
            var result = board.AttackPosition(Coordinate.CreateCoordinate(1, 1));
            result.Should().Be("hit");

            result = board.AttackPosition(Coordinate.CreateCoordinate(1, 2));
            result.Should().Be("hit");

            result = board.AttackPosition(Coordinate.CreateCoordinate(3, 1));
            result.Should().Be("miss");

            result = board.AttackPosition(Coordinate.CreateCoordinate(1, 3));
            result.Should().Be("sunk");
        }
    }
}
