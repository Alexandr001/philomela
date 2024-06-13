namespace Philomela.Domain.Entities.User
{
    /// <summary>
    ///     Репозиторий пользователя.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        ///     Метод создания пользователя в бд.
        /// </summary>
        /// <param name="user"> Модель пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        Task CreateUserAsync(User user, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Обновление пользователя в бд.
        /// </summary>
        /// <param name="user"> Модель пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Удаление пользователя по Id.
        /// </summary>
        /// <param name="id"> Id пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        Task DeleteUserByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
