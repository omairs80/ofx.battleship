using System.Collections.Generic;
using System.Linq;
using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Domain.Entities
{
    public class BattleShip
    {
        public static BattleShip CreateBattleship(
            int playerId,
            int startingRow,
            int startingColumn,
            int size,
            bool isHorizontal)
        {
            return new BattleShip(playerId, startingRow, startingColumn, size, isHorizontal);
        }

        private BattleShip(
            int playerId,
            int startingRow,
            int startingColumn,
            int size,
            bool isHorizontal)
        {
            PlayerId = playerId;
            StartingRow = startingRow;
            StartingColumn = startingColumn;
            Size = size;
            IsHorizontal = isHorizontal;
            Health = size;

            ValidateBattleShip();

            Coordinates = CalculateCoordinates();
        }

        public int PlayerId { get; private set; }

        public int StartingRow { get; private set; }

        public int StartingColumn { get; private set; }

        public int Size { get; private set; }

        public bool IsHorizontal { get; private set; }

        public int Health { get; private set; }

        public bool IsSunk => Health <= 0;

        public IEnumerable<Coordinate> Coordinates { get; }

        public void TakeAttack(Coordinate coordinate)
        {
            var shipCoordinate = Coordinates.SingleOrDefault(c => c.HashedValue == coordinate.HashedValue);
            if (shipCoordinate != null)
            {
                if (!shipCoordinate.HasBeenHit)
                {
                    shipCoordinate.TakeHit();
                    Health--;
                }
            }
        }

        private IEnumerable<Coordinate> CalculateCoordinates()
        {
            var coordinates = new List<Coordinate>();

            if (IsHorizontal)
            {
                for (var column = StartingColumn; column <= (StartingColumn + Size - 1); column++)
                {
                    coordinates.Add(Coordinate.CreateCoordinate(StartingRow, column));
                }
            }
            else
            {
                for (var row = StartingRow; row <= (StartingRow + Size -1); row++)
                {
                    coordinates.Add(Coordinate.CreateCoordinate(row, StartingColumn));
                }
            }

            return coordinates;
        }

        private void ValidateBattleShip()
        {
            var errorMessage = string.Empty;
            if (Size < 1 || Size > 10)
            {
                errorMessage = "Battleship's size should be >= 1 and <= 10";
            }
            else if (StartingRow < 1 || StartingRow > 10)
            {
                errorMessage = "Battleship's starting row position should be >= 1 and <= 10";
            }
            else if (StartingColumn < 1 || StartingColumn > 10)
            {
                errorMessage = "Battleship's starting column position should be >= 1 and <= 10";
            }

            if (IsHorizontal)
            {
                if (StartingColumn + Size - 1 > 10)
                {
                    errorMessage = "Battleship cannot be placed at this position. Due to the ship's position and size, it will exceed the board's boundaries";
                }
            }
            else
            {
                if (StartingRow + Size - 1 > 10)
                {
                    errorMessage = "Battleship cannot be placed at this position. Due to the ship's position and size, it will exceed the board's boundaries";
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new InvalidBattleShipException(errorMessage);
            }
        }
    }
}
