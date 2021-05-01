using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Ofx.Battleship.Domain.Exceptions;

namespace Ofx.Battleship.Controller
{
    public class MappingExceptionFilter : IExceptionFilter
    {
        private static readonly EmptyResult _emptyResult = new EmptyResult();
        private readonly ILogger<MappingExceptionFilter> _logger;

        public MappingExceptionFilter(ILogger<MappingExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                BoardAlreadyExistsException ex => new ConflictObjectResult(ex.Message),
                BoardPositionAlreadyOccupiedException ex => new ConflictObjectResult(ex.Message),
                BoardDoesNotExistException ex => new NotFoundObjectResult(ex.Message),
                ValidationException ex => new BadRequestObjectResult(ex.Message),
                InvalidBattleShipException ex => new BadRequestObjectResult(ex.Message),
                InvalidCoordinateException ex => new BadRequestObjectResult(ex.Message),
                TaskCanceledException _ when context.HttpContext.RequestAborted.IsCancellationRequested => _emptyResult,
                _ => null
            };

            if (context.Result != null)
            {
                context.ExceptionHandled = true;
                _logger.LogDebug(context.Exception, context.Exception.Message);
            }
        }
    }
}
