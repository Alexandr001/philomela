using Microsoft.EntityFrameworkCore;
using Philomela.Domain.Entities.Philomela;
using Serilog;

namespace Philomela.Infrastructure.Database.Repositories
{
    /// <inheritdoc />
    public sealed class PhilomelaRepository : IPhilomelaRepository
    {
        private readonly BaseDbContext _context;
        private readonly ILogger _logger;

        public PhilomelaRepository(BaseDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task CreatePhilomelaAsync(Domain.Entities.Philomela.Philomela philomela, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Philomels.AddAsync(philomela, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ue)
            {
                _logger.Error("Не удалось создать птичку!");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task UpdatePhilomelaAsync(Domain.Entities.Philomela.Philomela philomela, CancellationToken cancellationToken = default)
        {
            await _context.Philomels
                .ExecuteUpdateAsync(calls => calls.SetProperty(p => p, philomela), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeletePhilomelsAsync(int id, CancellationToken cancellationToken = default)
        {
            await _context.Philomels
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
