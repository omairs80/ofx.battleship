using System;

namespace Ofx.Battleship.Domain.Exceptions
{
    public class BoardPositionAlreadyOccupiedException : Exception
    {
        public BoardPositionAlreadyOccupiedException()
            : base("Cannot add battle ship the area that it will occupy already overlaps with another ship")
        {
            
        }
    }
}
