using System.Collections.Generic;
using Ofx.Battleship.Domain.Entities;
using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Domain
{
    public class GameManager : IGameManager
    {
        private readonly IDictionary<int, Board> _boards;

        public GameManager()
        {
            _boards = new Dictionary<int, Board>();
        }

        public Board CreateBoard(int playerId)
        {
            if (_boards.ContainsKey(playerId))
            {
                throw new BoardAlreadyExistsException(playerId);
            }

            var board = Board.CreateBoard(playerId);
            _boards.Add(playerId, board);

            return board;
        }

        public Board GetBoard(int playerId)
        {
            if (!_boards.ContainsKey(playerId))
            {
                throw new BoardDoesNotExistException(playerId);
            }

            return _boards[playerId];
        }
    }
}
