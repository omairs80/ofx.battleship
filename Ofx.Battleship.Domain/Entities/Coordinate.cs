using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Domain.Entities
{
    public class Coordinate
    {
        public static Coordinate CreateCoordinate(int row, int column)
        {
            return new Coordinate(row, column);
        }

        private Coordinate(int row, int column)
        {
            if (row < 1 || row > 10 || column < 1 || column > 10)
            {
                throw new InvalidCoordinateException(row, column);
            }

            Row = row;
            Column = column;
        }

        public int Row { get; private set; }

        public int Column { get; private set; }

        public int HashedValue => (Row + Column) + (Row * 10);

        public bool HasBeenHit { get; private set; }

        public void TakeHit()
        {
            HasBeenHit = true;
        }
    }
}
