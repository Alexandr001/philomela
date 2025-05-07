namespace App.Options
{
    public class JwtRefreshOptions
    {
        /// <summary>
        ///     Создатель токена.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        ///     Пользователь.
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        ///     Время жизни.
        /// </summary>
        public int LifetimeHour { get; set; }

        /// <summary>
        ///     Секрет. 
        /// </summary>
        public string Secret { get; set; } = string.Empty;
    }
}
