using FluentValidation;

namespace Philomela.Application.Commands.Calculate
{
    public class CalculateCommandValidator : AbstractValidator<CalculateCommand>
    {
        public CalculateCommandValidator()
        {
            RuleFor(c => c.Expression)
                .NotEmpty().WithMessage("Выражение не может быть пустым!");
        }    
    }
}
