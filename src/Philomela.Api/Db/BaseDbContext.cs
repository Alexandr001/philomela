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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCredential>()
                .HasData(new UserCredential {
                    Login = "admin",
                    Password = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918"
                    // Пaроль: admin
                });
        }
    }
}
