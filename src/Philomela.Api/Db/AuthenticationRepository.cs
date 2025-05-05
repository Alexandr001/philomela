using Microsoft.EntityFrameworkCore;

namespace Philomela.Api.Db
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
        public async Task<UserCredential?> FindAuthenticationModelByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            UserCredential? userCredential = await _dbContext.Authentication
                .FirstOrDefaultAsync(a => a.Login == login, cancellationToken);
            return userCredential;
        }

        /// <inheritdoc />
        public async Task CreateOrUpdateRefreshAsync(string login, string refreshToken, CancellationToken cancellationToken)
        {
            var userCredential = await _dbContext.Authentication
                .FirstOrDefaultAsync(x => x.Login == login, cancellationToken);
            if (userCredential is null)
            {
                return;
            }
            userCredential.RefreshToken = refreshToken;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        
        /// <inheritdoc />
        public async Task<bool> UpdateRefreshAsync(string login, string oldRefresh, string newRefresh, CancellationToken cancellationToken)
        {
            var userCredential = await _dbContext.Authentication
                .FirstOrDefaultAsync(x => x.RefreshToken == oldRefresh && x.RefreshToken != null && x.Login == login, cancellationToken);
            if (userCredential is null)
            {
                var user = await _dbContext.Authentication.Where(x => x.Login == login).FirstOrDefaultAsync(cancellationToken);
                if (user is null)
                {
                    throw new InvalidOperationException($"Логин - {login} не существует.");
                }
                user.RefreshToken = null;
                await _dbContext.SaveChangesAsync(cancellationToken);
                return false;
            }
            userCredential.RefreshToken = newRefresh;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
