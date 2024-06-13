using MediatR;
using Serilog;

namespace Philomela.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.Information($"Начало выполнения команды - {nameof(TRequest)}");
            TResponse response = await next();
            _logger.Information($"Команда <{nameof(TRequest)}> выполнена, команда ответа - {nameof(TResponse)}");
            return response;
        }
    }
}
