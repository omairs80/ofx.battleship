using MediatR;

namespace Ofx.Battleship.Application.Commands
{
    public class AttackBattleShipCommand : IRequest<string>
    {
        public int PlayerId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
