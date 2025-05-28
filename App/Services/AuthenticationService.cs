using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using App.Db;
using App.Models;
using App.Options;
using App.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.Services
{
    /// <inheritdoc />
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly IOptions<JwtRefreshOptions> _jwtRefreshOptions;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IAuthenticationRepository authenticationRepository, IOptions<JwtOptions> jwtOptions, IOptions<JwtRefreshOptions> jwtRefreshOptions, ILogger<AuthenticationService> logger)
        {
            _authenticationRepository = authenticationRepository;
            _jwtOptions = jwtOptions;
            _jwtRefreshOptions = jwtRefreshOptions;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<RefreshResponce> GetTokenAsync(LoginCommand model, CancellationToken cancellationToken = default)
        {
            Console.WriteLine(UserCredential.GetHashSha256(model.Password));
            UserCredential? userCredential =
                await _authenticationRepository.FindAuthenticationModelByLoginAsync(model.Login, cancellationToken);
            if (userCredential is null || VerifyPassword(model.Password, userCredential.Password) == false)
            {
                _logger.LogError("Ivalid username or password. UserName = {0}. Password = {1}", model.Login, model.Password);
                throw new AuthenticationException("Неверное имя пользователя или пароль!");
            }

            string jwt = CreateJwt(userCredential);
            string jwtRefresh = CreateRefreshJwt();

            await _authenticationRepository.CreateOrUpdateRefreshAsync(userCredential.Login, jwtRefresh, cancellationToken);
            return new RefreshResponce
            {
                AccessToken = jwt,
                RefreshToken = jwtRefresh
            };
        }

        /// <inheritdoc />
        public async Task<RefreshResponce> RefreshTokenAsync(
            string access, 
            string oldRefresh,
            string login,
            CancellationToken cancellationToken = default)
        {
            string newRefresh = CreateRefreshJwt();
            string newAccess = CreateJwt(new UserCredential{Login = login, UserRole = UserRole.USER});
            
            bool isSuccess = await _authenticationRepository.UpdateRefreshAsync(login, oldRefresh, newRefresh, cancellationToken);
            if (!isSuccess)
            {
                _logger.LogError("Not update tokens. User = {0}", login);
                throw new AuthenticationException("Не удалось обновить токены.");
            }

            return new RefreshResponce { AccessToken = newAccess, RefreshToken = newRefresh };

        }

        /// <summary>
        ///     Метод создания jwt токена.
        /// </summary>
        /// <param name="userCredential"> Модель пользователя. </param>
        /// <returns> JWT токен. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string CreateJwt(UserCredential userCredential)
        {
            List<Claim> claims =
            [
                new(ClaimTypes.Name, userCredential.Login),
                new(ClaimTypes.Role, userCredential.UserRole.ToString())
            ];
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
        ///     Метод создания jwt токена.
        /// </summary>
        /// <param name="userCredential"> Модель пользователя. </param>
        /// <returns> JWT токен. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string CreateRefreshJwt()
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtRefreshOptions.Value.Secret));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new(
                issuer: _jwtRefreshOptions.Value.Issuer,
                audience: _jwtRefreshOptions.Value.Audience,
                expires: DateTime.Now.AddHours(_jwtRefreshOptions.Value.LifetimeHour),
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
