using Ofx.Battleship.Domain.Entities;

namespace Ofx.Battleship.Domain
{
    public interface IGameManager
    {
        Board CreateBoard(int playerId);

        Board GetBoard(int playerId);
    }
}
