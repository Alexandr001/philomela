using Philomela.Domain.Entities.Authentication;

namespace Philomela.Domain.Entities.User
{
    public class User
    {
        /// <summary>
        ///     Уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Логин пользователя из <see cref="UserCredential"/>
        /// </summary>
        public string LoginUserCredential { get; set; }

        public UserCredential UserCredential { get; set; }
    }
}
