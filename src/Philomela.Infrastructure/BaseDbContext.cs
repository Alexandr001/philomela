using Microsoft.EntityFrameworkCore;
using Philomela.Domain.Entities.Authentication;
using Philomela.Domain.Entities.User;
using Philomela.Infrastructure.Database.Configurations;

namespace Philomela.Infrastructure
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

        public DbSet<Domain.Entities.Philomela.Philomela> Philomels { get; set; }

        public DbSet<UserCredential> Authentication { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserCredentialConfig).Assembly);
        }
    }
}
