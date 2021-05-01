using MediatR;

namespace Ofx.Battleship.Application.Commands
{
    public class CreateBoardCommand : IRequest
    {
        public CreateBoardCommand(int playerId)
        {
            PlayerId = playerId;
        }

        public int PlayerId { get; }
    }
}
