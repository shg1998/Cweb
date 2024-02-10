using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Entities.History
{
    public class AlarmConfiguration : IEntityTypeConfiguration<Alarm>
    {
        public void Configure(EntityTypeBuilder<Alarm> builder)
        {
            builder.Property(p => p.BedId).HasMaxLength(10);
            builder.Property(p => p.Code).HasMaxLength(10);
            builder.Property(p => p.Level).HasMaxLength(10);
        }
    }
}
