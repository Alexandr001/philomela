using MediatR;
using Philomela.Application.Common.Services.Interfaces;

namespace Philomela.Application.Commands.User.V1.Registration
{
    /// <summary>
    ///     Обработчик для <see cref="CreateUserCommand"/>
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            return _userService.CreateUserAsync(request, cancellationToken);
        }
    }
}
