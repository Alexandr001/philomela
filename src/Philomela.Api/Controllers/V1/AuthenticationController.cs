using Microsoft.AspNetCore.Mvc;
using Philomela.Api.Models;
using Philomela.Api.Services.Interfaces;

namespace Philomela.Api.Controllers.V1
{
    /// <summary>
    ///     Контроллер аутентификации.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        ///     Метод аутентификации.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command,
            CancellationToken cancellationToken)
        {
            string token = await _authenticationService.GetTokenAsync(command, cancellationToken);
            return Ok(token);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> LoginAsync([FromBody] string accessToken,
            CancellationToken cancellationToken)
        {
            
            return Ok();
        }
    }
}
