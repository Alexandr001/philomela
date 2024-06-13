using Philomela.Application.Commands.Authentication.V1.Login;

namespace Philomela.Application.Common.Services.Interfaces
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
        Task<string> GetTokenAsync(LoginCommand model, CancellationToken cancellationToken = default);
    }
}
