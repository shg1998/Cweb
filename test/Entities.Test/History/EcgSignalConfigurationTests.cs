using Entities.History;
using Microsoft.EntityFrameworkCore;

namespace Entities.Test.History
{
    public class EcgSignalConfigurationTests
    {
        [Fact]
        public void Configure_SetsMaxLength_ForBedId()
        {
            var modelBuilder = new ModelBuilder();
            new EcgSignalConfiguration().Configure(modelBuilder.Entity<EcgSignal>());
            var maxLength = modelBuilder.Entity<EcgSignal>().Metadata.FindProperty("BedId").GetMaxLength();
            Assert.Equal(20, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForEcgLead()
        {
            var modelBuilder = new ModelBuilder();
            new EcgSignalConfiguration().Configure(modelBuilder.Entity<EcgSignal>());
            var maxLength = modelBuilder.Entity<EcgSignal>().Metadata.FindProperty("EcgLead").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForEcgFilter()
        {
            var modelBuilder = new ModelBuilder();
            new EcgSignalConfiguration().Configure(modelBuilder.Entity<EcgSignal>());
            var maxLength = modelBuilder.Entity<EcgSignal>().Metadata.FindProperty("EcgFilter").GetMaxLength();
            Assert.Equal(10, maxLength);
        }
    }
}
