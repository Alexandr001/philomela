using System.Text.Json.Serialization;

namespace Philomela.Application.Commands.Authentication.V1.Login
{
    /// <summary>
    ///     Модель ответа аутентификации.
    /// </summary>
    public class LoginCommandResponse
    {
        /// <summary>
        ///     Токен. 
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}
