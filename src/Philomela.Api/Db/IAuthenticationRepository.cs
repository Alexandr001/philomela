namespace Philomela.Api.Db
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
        public Task<UserCredential?> FindAuthenticationModelByLoginAsync(string login, CancellationToken cancellationToken = default);
        
        public Task CreateOrUpdateRefreshAsync(
            string login, 
            string refreshToken, 
            CancellationToken cancellationToken);

        public Task<bool> UpdateRefreshAsync(
            string login, 
            string oldRefresh, 
            string newRefresh,
            CancellationToken cancellationToken);
    }
}
