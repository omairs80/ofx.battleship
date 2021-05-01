using System;

namespace Ofx.Battleship.Domain.Exceptions
{
    public class InvalidBattleShipException : Exception
    {
        public InvalidBattleShipException(string message)
            : base(message)
        {
        }
    }
}
