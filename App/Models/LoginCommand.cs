using System.Text.Json.Serialization;

namespace App.Models
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
