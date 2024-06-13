namespace Philomela.Domain.Entities.Authentication
{
    /// <summary>
    ///     Репозиторий аутентификации.
    /// </summary>
    public interface IAuthenticationRepository
    {
        /// <summary>
        ///     Метод получения модели данных пользователя по логину. 
        /// </summary>
        /// <param name="login"> Логин пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <returns></returns>
        Task<UserCredential?> FindAuthenticationModelByLoginAsync(string login, CancellationToken cancellationToken = default);
    }
}
