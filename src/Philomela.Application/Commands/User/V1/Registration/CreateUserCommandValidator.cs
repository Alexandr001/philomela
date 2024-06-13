using FluentValidation;

namespace Philomela.Application.Commands.User.V1.Registration
{
    /// <summary>
    ///     Валидация <see cref="CreateUserCommand"/>. 
    /// </summary>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Поле <name> не может быть пустым!");
        }
    }
}
