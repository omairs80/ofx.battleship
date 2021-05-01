using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.Application.Commands;
using Ofx.Battleship.Contract.Requests;

namespace Ofx.Battleship.Controller
{
    [ApiController]
    [Route("ofx/[controller]")]
    public class BattleshipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BattleshipController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("{playerId}/board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public Task CreateBoard(int playerId, CancellationToken cancellationToken)
        {
            var command = new CreateBoardCommand(playerId);
            return _mediator.Send(command, cancellationToken);
        }

        [HttpPost("{playerId}/battleship")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public Task AddBattleship(int playerId, AddBattleShipRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddBattleShipCommand>(request);
            command.PlayerId = playerId;
            return _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{playerId}/battleship")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<string> AttackBattleShip(int playerId, AttackBattleShipRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AttackBattleShipCommand>(request);
            command.PlayerId = playerId;
            return _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{playerId}/board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task DeleteBoard(int playerId, CancellationToken cancellationToken)
        {
            var command = new DeleteBoardCommand(playerId);
            return _mediator.Send(command, cancellationToken);
        }
    }
}
