using System;

namespace Ofx.Battleship.Domain.Exceptions
{
    public class InvalidCoordinateException : Exception
    {
        public InvalidCoordinateException(int row, int column)
            : base($"Coordinate values of ({row}, {column}) are not valid. Values should be 1 - 10")
        {
            
        }
    }
}
