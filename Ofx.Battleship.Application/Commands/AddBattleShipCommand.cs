using MediatR;

namespace Ofx.Battleship.Application.Commands
{
    public class AddBattleShipCommand : IRequest
    {
        public int PlayerId { get; set; }

        public int Size { get; set; }

        public int StartingRow { get; set; }

        public int StartingColumn { get; set; }

        public bool IsHorizontal { get; set; }
    }
}
