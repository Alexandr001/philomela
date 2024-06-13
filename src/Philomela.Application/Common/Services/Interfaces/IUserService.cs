using Philomela.Application.Commands.User.V1.Registration;

namespace Philomela.Application.Common.Services.Interfaces
{
    /// <summary>
    ///     Сервис пользователя.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     Метод создания пользователя.
        /// </summary>
        /// <param name="model"> Модель создания пользователя. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <returns></returns>
        Task CreateUserAsync(CreateUserCommand model, CancellationToken cancellationToken = default);
    }
}
