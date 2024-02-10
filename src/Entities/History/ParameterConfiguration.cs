using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Entities.History
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.Property(p => p.BedId).HasMaxLength(10);
            builder.Property(p => p.Hr).HasMaxLength(10);
            builder.Property(p => p.Rr).HasMaxLength(10);
            builder.Property(p => p.Spo2).HasMaxLength(10);
            builder.Property(p => p.T1).HasMaxLength(10);
            builder.Property(p => p.T2).HasMaxLength(10);
            builder.Property(p => p.Dt).HasMaxLength(10);
            builder.Property(p => p.Ibp1Sys).HasMaxLength(10);
            builder.Property(p => p.Ibp1Dia).HasMaxLength(10);
            builder.Property(p => p.Ibp1Map).HasMaxLength(10);
            builder.Property(p => p.Ibp2Sys).HasMaxLength(10);
            builder.Property(p => p.Ibp2Dia).HasMaxLength(10);
            builder.Property(p => p.Ibp2Map).HasMaxLength(10);
            builder.Property(p => p.Awrr).HasMaxLength(10);
            builder.Property(p => p.EtcO2).HasMaxLength(10);
            builder.Property(p => p.FiCo2).HasMaxLength(10);
            builder.Property(p => p.NibpSys).HasMaxLength(10);
            builder.Property(p => p.NibpDia).HasMaxLength(10);
            builder.Property(p => p.NibpMap).HasMaxLength(10);
        }
    }
}
