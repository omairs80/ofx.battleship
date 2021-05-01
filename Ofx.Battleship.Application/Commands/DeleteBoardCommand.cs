using MediatR;

namespace Ofx.Battleship.Application.Commands
{
    public class DeleteBoardCommand : IRequest
    {
        public DeleteBoardCommand(int playerId)
        {
            PlayerId = playerId;
        }

        public int PlayerId { get; }
    }
}
