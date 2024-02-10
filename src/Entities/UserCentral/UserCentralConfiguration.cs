using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Entities.UserCentral
{
    public class UserCentralConfiguration : IEntityTypeConfiguration<UserCentral>
    {
        public void Configure(EntityTypeBuilder<UserCentral> builder)
        {
            builder.HasOne(s => s.User)
                .WithMany(s => s.UserCentrals)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Central)
                .WithMany(s => s.CentralUsers)
                .HasForeignKey(s => s.CentralId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
