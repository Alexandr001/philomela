using System.ComponentModel.DataAnnotations;

namespace Philomela.Domain.Enums
{
    /// <summary>
    ///     Роли пользователя в системе. 
    /// </summary>
    public enum UserRole
    {
        [Display(Name = "Пользователь")]
        USER = 0,
        [Display(Name = "Администратор")]
        ADMIN = 1,
    }
}
