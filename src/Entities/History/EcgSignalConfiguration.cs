using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.History
{
    public class EcgSignalConfiguration : IEntityTypeConfiguration<EcgSignal>
    {
        public void Configure(EntityTypeBuilder<EcgSignal> builder)
        {
            builder.Property(p => p.BedId).HasMaxLength(20);
            builder.Property(p => p.EcgLead).HasMaxLength(10);
            builder.Property(p => p.EcgFilter).HasMaxLength(10);
        }
    }
}
