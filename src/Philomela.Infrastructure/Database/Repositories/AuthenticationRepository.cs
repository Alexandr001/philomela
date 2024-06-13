using Microsoft.EntityFrameworkCore;
using Philomela.Domain.Entities.Authentication;

namespace Philomela.Infrastructure.Database.Repositories
{
    /// <inheritdoc />
    public sealed class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly BaseDbContext _dbContext;

        public AuthenticationRepository(BaseDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<UserCredential?> FindAuthenticationModelByLoginAsync(string login, CancellationToken cancellationToken)
        {
            UserCredential? userCredential = await _dbContext.Authentication
                .FirstOrDefaultAsync(a => a.Login == login, cancellationToken);
            return userCredential;
        }
    }
}
