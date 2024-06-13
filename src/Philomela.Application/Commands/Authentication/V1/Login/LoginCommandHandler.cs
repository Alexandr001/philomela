using MediatR;
using Philomela.Application.Common.Services.Interfaces;

namespace Philomela.Application.Commands.Authentication.V1.Login
{
    /// <summary>
    ///     Обработчик для <see cref="LoginCommand"/>
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            string token = await _authenticationService.GetTokenAsync(request, cancellationToken);
            return new LoginCommandResponse { Token = token };
        }
    }
}
