using FluentAssertions;
using NUnit.Framework;
using Ofx.Battleship.Domain.Entities;
using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Domain.Test
{
    public class CoordinateTests
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(5, 4)]
        [TestCase(10, 10)]
        public void CreateCoordinate_Should_Create_Coordinate(int row, int column)
        {
            // Act
            var coordinate = Coordinate.CreateCoordinate(row, column);

            // Assert
            coordinate.Should().NotBeNull();
            coordinate.Row.Should().Be(row);
            coordinate.Column.Should().Be(column);
            coordinate.HashedValue.Should().Be((row + column) + (row * 10));
            coordinate.HasBeenHit.Should().BeFalse();
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(5, 0)]
        [TestCase(11, 1)]
        [TestCase(1, 11)]
        public void CreateCoordinate_Should_Throw_InvalidCoordinateException(int row, int column)
        {
            // Act & Assert
            Assert.Throws<InvalidCoordinateException>(() => Coordinate.CreateCoordinate(row, column),
                $"Coordinate values of ({row}, {column}) are not valid. Values should be 1 - 10");
        }

        [Test]
        public void TakeHit_Test()
        {
            // Arrange
            var coordinate = Coordinate.CreateCoordinate(1, 1);

            // Act
            coordinate.TakeHit();

            // Assert
            coordinate.HasBeenHit.Should().BeTrue();
        }
    }
}
