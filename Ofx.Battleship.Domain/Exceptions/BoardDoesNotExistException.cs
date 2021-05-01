using System;

namespace Ofx.Battleship.Domain.Exceptions
{
    public class BoardDoesNotExistException : Exception
    {
        public BoardDoesNotExistException(int playerId)
            : base($"Game board does not exist for player {playerId}. Please first create a board for this player.")
        {

        }
    }
}
