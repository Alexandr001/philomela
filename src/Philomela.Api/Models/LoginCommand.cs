using System.Text.Json.Serialization;

namespace Philomela.Api.Models
{
    public class LoginCommand
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
    }
}
