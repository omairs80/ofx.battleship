using System;

namespace Ofx.Battleship.Domain.Exceptions
{
    public class BoardAlreadyExistsException : Exception
    {
        public BoardAlreadyExistsException(int playerId)
            : base($"Game board already exists for player {playerId}. Try to create a board with a different player Id.")
        {
            
        }
    }
}
