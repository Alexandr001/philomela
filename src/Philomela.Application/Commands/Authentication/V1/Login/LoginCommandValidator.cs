using FluentValidation;

namespace Philomela.Application.Commands.Authentication.V1.Login
{
    /// <summary>
    ///     Валидация <see cref="LoginCommand"/>. 
    /// </summary>
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(m => m.Login)
                .NotEmpty().WithMessage("Логин не может быть пустым!");
            RuleFor(m => m.Login)
                .NotEmpty().WithMessage("Пароль не может быть пустым!");
            
        }
    }
}
