using System.Text.Json.Serialization;
using MediatR;
using Philomela.Domain.Enums;

namespace Philomela.Application.Commands.Authentication.V1.Login
{
    /// <summary>
    ///     Команда аутентификации. 
    /// </summary>
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        /// <summary>
        ///     Логин. 
        /// </summary>
        [JsonPropertyName("login")]
        public string Login { get; set; }

        /// <summary>
        ///     Пароль.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }

        /// <summary>
        ///     Роль пользователя.
        /// </summary>
        [JsonPropertyName("userRole")]
        public UserRole UserRole { get; set; } = UserRole.USER;
    }
}
