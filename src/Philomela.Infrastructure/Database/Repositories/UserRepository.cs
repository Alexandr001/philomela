using Microsoft.EntityFrameworkCore;
using Philomela.Domain.Entities.User;
using Serilog;

namespace Philomela.Infrastructure.Database.Repositories
{
    /// <inheritdoc />
    public sealed class UserRepository : IUserRepository
    {
        private readonly BaseDbContext _context;
        private readonly ILogger _logger;

        public UserRepository(BaseDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.User.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ue)
            {
                _logger.Error("Не удалось добавить объект!");
                throw;
            }
        }

        /// <inheritdoc />
        public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                _context.User.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ue)
            {
                _logger.Error("Не удалось обновить данные!");
                throw;
            } 
        }

        /// <inheritdoc />
        public async Task DeleteUserByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await _context.User
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
