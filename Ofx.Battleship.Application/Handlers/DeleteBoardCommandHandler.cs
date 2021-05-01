using Ofx.Battleship.Application.Commands;
using Ofx.Battleship.Common.Mediator;
using Ofx.Battleship.Domain;

namespace Ofx.Battleship.Application.Handlers
{
    public class DeleteBoardCommandHandler : CommandHandlerBase<DeleteBoardCommand>
    {
        private readonly IGameManager _gameManager;

        public DeleteBoardCommandHandler(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override void HandleCommand(DeleteBoardCommand command)
        {
            _gameManager.DeleteBoard(command.PlayerId);
        }
    }
}
