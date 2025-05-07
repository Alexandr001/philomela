using App.Models;

namespace App.Services.Interfaces
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
        public Task<RefreshResponce> GetTokenAsync(LoginCommand model, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Метод обновления токенов.
        /// </summary>
        /// <param name="access"></param>
        /// <param name="oldRefresh"></param>
        /// <param name="login"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<RefreshResponce> RefreshTokenAsync(
            string access,
            string oldRefresh,
            string login,
            CancellationToken cancellationToken = default);
    }
}
