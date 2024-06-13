using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Philomela.Application.Commands.Authentication.V1.Login;
using Philomela.Application.Common.Services.Interfaces;
using Philomela.Application.Options;
using Philomela.Domain.Entities.Authentication;

namespace Philomela.Application.Common.Services
{
    /// <inheritdoc />
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public AuthenticationService(IAuthenticationRepository authenticationRepository, IOptions<JwtOptions> jwtOptions)
        {
            _authenticationRepository = authenticationRepository;
            _jwtOptions = jwtOptions;
        }

        /// <inheritdoc />
        public async Task<string> GetTokenAsync(LoginCommand model, CancellationToken cancellationToken = default)
        {
            UserCredential? userCredential =
                await _authenticationRepository.FindAuthenticationModelByLoginAsync(model.Login, cancellationToken);
            if (userCredential is null || VerifyPassword(model.Password, userCredential.Password) == false)
            {
                throw new AuthenticationException("Неверное имя пользователя или пароль!");
            }

            string jwt = CreateJwt(userCredential);
            return jwt;
        }

        /// <summary>
        ///     Метод создания jwt токена.
        /// </summary>
        /// <param name="userCredential"> Модель пользователя. </param>
        /// <returns> JWT токен. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string CreateJwt(UserCredential userCredential)
        {
            List<Claim> claims = new()  
            {
                new Claim(ClaimTypes.Name, userCredential.Login),
                new Claim(ClaimTypes.Role, userCredential.UserRole.ToString()),
            };
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtOptions.Value.Secret));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new(
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.Value.Lifetime),
                signingCredentials: signingCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        /// <summary>
        ///     Метод проверки пароля.
        /// </summary>
        /// <param name="password"> Пароль в оригинальном виде. </param>
        /// <param name="hashPassword"> Хеш пароля из бд. </param>
        /// <returns> true - верный пароль, false - неверный. </returns>
        private bool VerifyPassword(string password, string hashPassword) =>
            UserCredential.GetHashSha256(password) == hashPassword;
    }
}
