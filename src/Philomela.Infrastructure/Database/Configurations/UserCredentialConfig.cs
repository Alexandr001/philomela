using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Philomela.Domain.Entities.Authentication;
using Philomela.Domain.Entities.User;

namespace Philomela.Infrastructure.Database.Configurations
{
    internal class UserCredentialConfig : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            builder.ToTable("user_credentials");

            builder.Property(uc => uc.Login)
                .HasColumnType("varchar")
                .HasColumnName("login")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(uc => uc.Password)
                .HasColumnType("varchar")
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(65);
            builder.Property(uc => uc.UserRole)
                .HasColumnType("integer")
                .HasColumnName("user_role")
                .IsRequired();
            builder
                .HasOne(uc => uc.User)
                .WithOne(u => u.UserCredential)
                .HasForeignKey<User>(u => u.LoginUserCredential)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasKey(p => p.Login);
        }
    }
}
