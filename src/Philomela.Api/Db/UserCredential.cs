using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Philomela.Api.Models;

namespace Philomela.Api.Db
{
    [Table("UserCredential")]
    public class UserCredential
    {
        /// <summary>
        ///     Уникальный логин пользователя.
        /// </summary>
        [Key]
        public string Login { get; set; }

        /// <summary>
        ///     Пароль (в бд храниться хешем).
        /// </summary>
        [PasswordPropertyText]
        public string Password { get; set; }

        /// <summary>
        ///     Роль пользователя.
        /// </summary>
        public UserRole UserRole { get; set; } = UserRole.USER;
        
        /// <summary>
        ///     Токен обновления.
        /// </summary>
        [PasswordPropertyText]
        public string? RefreshToken { get; set; }

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
