using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Philomela.Application.Commands.Authentication.V1.Login;

namespace Philomela.Api.Controllers.V1
{
    /// <summary>
    ///     Контроллер аутентификации.
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        ///     Метод аутентификации.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var send = await _mediator.Send(command, cancellationToken);
            return Ok(send);
        }
    }
}
