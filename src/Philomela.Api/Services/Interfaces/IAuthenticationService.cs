using Philomela.Api.Models;

namespace Philomela.Api.Services.Interfaces
{
    /// <summary>
    ///     Сервис аутентификации.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        ///     Метод получения токена.
        /// </summary>
        /// <param name="model"> Модель аутентификации. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <returns> JWT токен. </returns>
        public Task<string> GetTokenAsync(LoginCommand model, CancellationToken cancellationToken = default);
    }
}
