using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Philomela.Infrastructure.Database.Configurations
{
    internal class PhilomelaConfig : IEntityTypeConfiguration<Domain.Entities.Philomela.Philomela>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Philomela.Philomela> builder)
        {
            builder.ToTable("philomels");
        }
    }
}
