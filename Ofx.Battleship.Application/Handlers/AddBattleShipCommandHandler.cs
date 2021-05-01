using Ofx.Battleship.Application.Commands;
using Ofx.Battleship.Common.Mediator;
using Ofx.Battleship.Domain;
using Ofx.Battleship.Domain.Entities;

namespace Ofx.Battleship.Application.Handlers
{
    public class AddBattleShipCommandHandler : CommandHandlerBase<AddBattleShipCommand>
    {
        private readonly IGameManager _gameManager;

        public AddBattleShipCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override void HandleCommand(AddBattleShipCommand command)
        {
            var board = _gameManager.GetBoard(command.PlayerId);
            var battleShip = BattleShip.CreateBattleship(
                command.PlayerId,
                command.StartingRow,
                command.StartingColumn,
                command.Size,
                command.IsHorizontal);
            board.AddBattleShip(battleShip);
        }
    }
}
