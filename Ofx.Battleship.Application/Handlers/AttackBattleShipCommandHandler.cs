using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ofx.Battleship.Application.Commands;
using Ofx.Battleship.Domain;
using Ofx.Battleship.Domain.Entities;

namespace Ofx.Battleship.Application.Handlers
{
    public class AttackBattleShipCommandHandler : IRequestHandler<AttackBattleShipCommand, string>
    {
        private readonly IGameManager _gameManager;

        public AttackBattleShipCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public Task<string> Handle(AttackBattleShipCommand command, CancellationToken cancellationToken)
        {
            var board = _gameManager.GetBoard(command.PlayerId);
            var result = board.AttackPosition(Coordinate.CreateCoordinate(command.Row, command.Column));
            return Task.FromResult(result);
        }
    }
}
