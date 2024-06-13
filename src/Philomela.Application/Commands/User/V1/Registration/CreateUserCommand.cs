using System.Text.Json.Serialization;
using MediatR;
using Philomela.Application.Commands.Authentication.V1.Login;

namespace Philomela.Application.Commands.User.V1.Registration
{
    /// <summary>
    ///     Модель создания пользователя. 
    /// </summary>
    public class CreateUserCommand : IRequest
    {
        /// <summary>
        ///     Уникальный Id поьзователя.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Имя.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Данные аутентификации.
        /// </summary>
        [JsonPropertyName("loginModel")]
        public LoginCommand LoginModel { get; set; }
    }
}
