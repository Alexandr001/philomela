using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Philomela.Application.ActionsFilters.ApiKey;
using Philomela.Application.Commands.Calculate;

namespace Philomela.Api.Controllers.V1
{
    /// <summary>
    ///     Контроллер для расчетов.
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalculationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        ///     Метод для расчета выражения.
        /// </summary>
        [ApiKey]
        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateExpression(CalculateCommand command, CancellationToken cancellationToken)
        {
            CalculateCommandResponse response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        } 
    }
}
