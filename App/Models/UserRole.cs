using System.ComponentModel.DataAnnotations;

namespace App.Models
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
