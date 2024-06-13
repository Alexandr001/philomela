using MediatR;
using Philomela.Application.Common.Services.Interfaces;
using Serilog;

namespace Philomela.Application.Commands.Calculate
{
    public class CalculateCommandHandler : IRequestHandler<CalculateCommand, CalculateCommandResponse>
    {
        private readonly ICalculationService _calculationService;
        private readonly ILogger _logger;

        public CalculateCommandHandler(ICalculationService calculationService, ILogger logger)
        {
            _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CalculateCommandResponse> Handle(CalculateCommand request, CancellationToken cancellationToken)
        {
            _logger.Debug("Исполнение метода <calculate>");
            CalculateCommandResponse response = new();
            response.Postfix = _calculationService.GetReversePolishEntry(request.Expression);
            response.Result = _calculationService.CalculateExpression(response.Postfix);
            return response;
        }
    }
}
