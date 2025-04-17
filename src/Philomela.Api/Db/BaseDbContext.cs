using Microsoft.EntityFrameworkCore;

namespace Philomela.Api.Db
{
    /// <summary>
    ///     Базовый контекст.
    /// </summary>
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserCredential> Authentication { get; set; }
    }
}
