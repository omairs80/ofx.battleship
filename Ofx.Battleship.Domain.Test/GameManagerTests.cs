using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Domain.Test
{
    public class GameManagerTests
    {
        [Test]
        [AutoData]
        public void CreateBoard_Test(int playerId)
        {
            // Act
            var gameManager = new GameManager();
            var board = gameManager.CreateBoard(playerId);

            // Assert
            board.Should().NotBeNull();
            board.PlayerId.Should().Be(playerId);
        }

        [Test]
        [AutoData]
        public void CreateBoard_Should_Throw_BoardAlreadyExistsException_When_Player_Board_Already_Exists(int playerId)
        {
            // Arrange
            var gameManager = new GameManager();
            gameManager.CreateBoard(playerId);

            // Act & Assert
            Assert.Throws<BoardAlreadyExistsException>(() => gameManager.CreateBoard(playerId),
                $"Game board already exists for player {playerId}. Try to create a board with a different player Id.");
        }

        [Test]
        [AutoData]
        public void GetBoard_Should_Return_PlayerBoard(int playerId)
        {
            // Arrange
            var gameManager = new GameManager();
            var board = gameManager.CreateBoard(playerId);

            // Act
            var playerBoard = gameManager.GetBoard(playerId);

            // Assert
            playerBoard.Should().BeEquivalentTo(board);
        }

        [Test]
        [AutoData]
        public void GetBoard_Should_Throw_BoardDoesNotExistException_If_Player_Board_Does_Not_Exist(int playerId)
        {
            // Act & Assert
            var gameManager = new GameManager();
            Assert.Throws<BoardDoesNotExistException>(() => gameManager.GetBoard(playerId),
                $"Game board does not exist for player {playerId}. Please first create a board for this player.");
        }

        [Test]
        [AutoData]
        public void Delete_Should_Delete_Player_Board(int playerId)
        {
            // Arrange
            var gameManager = new GameManager();
            gameManager.CreateBoard(playerId);
            gameManager.GetBoard(playerId);

            // Act
            gameManager.DeleteBoard(playerId);

            // Assert
            Assert.Throws<BoardDoesNotExistException>(() => gameManager.GetBoard(playerId));
        }

        [Test]
        [AutoData]
        public void Delete_Should_Throw_BoardDoesNotExistException_If_Player_Board_Does_Not_Exist(int playerId)
        {
            // Act & Assert
            var gameManager = new GameManager();
            Assert.Throws<BoardDoesNotExistException>(() => gameManager.DeleteBoard(playerId),
                $"Game board does not exist for player {playerId}. Please first create a board for this player.");
        }
    }
}
