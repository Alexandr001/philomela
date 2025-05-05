using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Philomela.Api.Db;
using Philomela.Api.Middlewares;
using Philomela.Api.Models;
using Philomela.Api.Options;
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
        private readonly IAuthenticationRepository _repository;
        private readonly JwtOptions _jwtOptions;
        private readonly JwtRefreshOptions _jwtRefreshOptions;

        public AuthenticationController(
            IAuthenticationService authenticationService,
            IAuthenticationRepository repository, 
            IOptions<JwtRefreshOptions> jwtRefreshOptions,
            IOptions<JwtOptions> jwtOptions)
        {
            _authenticationService = authenticationService;
            _repository = repository;
            _jwtOptions = jwtOptions.Value;
            _jwtRefreshOptions = jwtRefreshOptions.Value;
        }

        /// <summary>
        ///     Метод аутентификации.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command,
            CancellationToken cancellationToken)
        {
            var model = await _authenticationService.GetTokenAsync(command, cancellationToken);
            AddCookie = model.AccessToken;
            AddCookieRefr = model.RefreshToken;
            return Ok(model);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshModel model,
            CancellationToken cancellationToken)
        {
            var refresh = HttpContext.Request.Headers["refresh"];
            var access = HttpContext.Request.Headers.Authorization.ToString()[7..];
            JwtSecurityToken? refreshJwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refresh);
            JwtSecurityToken? accessJwtToken = new JwtSecurityTokenHandler().ReadJwtToken(access);

            if (accessJwtToken == null || refreshJwtToken == null)
            {
                return Unauthorized("Не валидный токен доступа или токен обновления.");
            }

            var responce =
                await _authenticationService.RefreshTokenAsync(access, refresh, model.Login, cancellationToken);
            AddCookie = responce.AccessToken;
            AddCookieRefr = responce.RefreshToken;
            return Ok(responce);
        }

        private string AddCookie
        {
            set =>
                Response.Cookies.Append(TokenMiddleware.COOKIE_NAME, value,
                    new CookieOptions
                    {
                        HttpOnly = true, 
                        Secure = true,
                        Expires = DateTime.Now.AddHours(_jwtOptions.Lifetime)
                    });
        }
        
        private string AddCookieRefr
        {
            set =>
                Response.Cookies.Append(TokenMiddleware.COOKIE_NAME_REFRESH, value,
                    new CookieOptions
                    {
                        HttpOnly = true, 
                        Secure = true,
                        Expires = DateTime.Now.AddHours(_jwtRefreshOptions.LifetimeHour)
                    });
        }
    }
}
