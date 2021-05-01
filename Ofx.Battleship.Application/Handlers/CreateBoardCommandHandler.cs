using Ofx.Battleship.Application.Commands;
using Ofx.Battleship.Common.Mediator;
using Ofx.Battleship.Domain;

namespace Ofx.Battleship.Application.Handlers
{
    public class CreateBoardCommandHandler : CommandHandlerBase<CreateBoardCommand>
    {
        private readonly IGameManager _gameManager;

        public CreateBoardCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override void HandleCommand(CreateBoardCommand command)
        {
            _gameManager.CreateBoard(command.PlayerId);
        }
    }
}
