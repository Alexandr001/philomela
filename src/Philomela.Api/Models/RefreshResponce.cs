namespace Philomela.Api.Models
{
    public class RefreshResponce
    {
        /// <summary>
        ///     Токен доступа.
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        ///     Токен обновления.
        /// </summary>
        public string RefreshToken { get; set; }
        
    }
}
