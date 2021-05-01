using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Ofx.Battleship.Common.Mediator
{
    public abstract class CommandHandlerBase<TRequest> : AsyncRequestHandler<TRequest>
        where TRequest : IRequest
    {
        public abstract void HandleCommand(TRequest command);

        protected override Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            HandleCommand(request);
            return Task.CompletedTask;
        }
    }
}
