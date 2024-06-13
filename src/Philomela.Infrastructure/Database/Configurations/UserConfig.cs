using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Philomela.Domain.Entities.User;

namespace Philomela.Infrastructure.Database.Configurations
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(p => p.Id)
                .HasColumnType("integer")
                .HasColumnName("id")
                .IsRequired();
            builder.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.LoginUserCredential)
                .HasColumnType("varchar")
                .HasColumnName("user_credential_login");
            
            builder.HasKey(p => p.Id);
        }
    }
}
