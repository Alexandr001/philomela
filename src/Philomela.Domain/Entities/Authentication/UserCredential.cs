using System.Security.Cryptography;
using System.Text;
using Philomela.Domain.Enums;

namespace Philomela.Domain.Entities.Authentication
{
    public class UserCredential
    {
        /// <summary>
        ///     Уникальный логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     Пароль (в бд храниться хешем).
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Роль пользователя.
        /// </summary>
        public UserRole UserRole { get; set; } = UserRole.USER;

        public User.User User { get; set; }

        /// <summary>
        ///     Метод хеширования пароля.
        /// </summary>
        /// <remarks> Метод хеширования SHA256. </remarks>
        /// <param name="password"> Пароль. </param>
        /// <returns> Захешированая строка. </returns>
        public static string GetHashSha256(string password)
        {
            using SHA256 hash = SHA256.Create();
            byte[] computeHash = hash.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToHexString(computeHash);
        }
    }
}
