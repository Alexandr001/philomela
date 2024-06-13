using Mapster;
using Philomela.Application.Commands.User.V1.Registration;
using Philomela.Application.Common.Services.Interfaces;
using Philomela.Domain.Entities.Authentication;
using Philomela.Domain.Entities.User;

namespace Philomela.Application.Common.Services
{
    /// <inheritdoc />
    internal sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task CreateUserAsync(CreateUserCommand model, CancellationToken cancellationToken = default)
        {
            model.LoginModel.Password = UserCredential.GetHashSha256(model.LoginModel.Password);
            User user = model.Adapt<User>();
            await _userRepository.CreateUserAsync(user, cancellationToken);
        }
    }
}
