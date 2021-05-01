using System.Collections.Generic;
using System.Linq;
using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Domain.Entities
{
    public class Board
    {
        private readonly IDictionary<int, BattleShip> _coordinates = new Dictionary<int, BattleShip>();

        public static Board CreateBoard(int playerId)
        {
            return new Board(playerId);
        }

        private Board(int playerId)
        {
            PlayerId = playerId;
        }

        public int PlayerId { get; private set; }

        public void AddBattleShip(BattleShip battleShip)
        {
            if (battleShip.Coordinates.All(coordinate => !IsPositionOccupied(coordinate)))
            {
                foreach (var coordinate in battleShip.Coordinates)
                {
                    _coordinates.Add(coordinate.HashedValue, battleShip);
                }

                return;
            }

            throw new BoardPositionAlreadyOccupiedException();
        }

        public string AttackPosition(Coordinate coordinate)
        {
            if (IsPositionOccupied(coordinate))
            {
                var battleShip = _coordinates[coordinate.HashedValue];
                battleShip.TakeAttack(coordinate);

                return battleShip.IsSunk ? "sunk" : "hit";
            }

            return "miss";
        }

        public bool IsPositionOccupied(Coordinate coordinate)
        {
            return _coordinates.ContainsKey(coordinate.HashedValue);
        }
    }
}
