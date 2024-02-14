using Entities.History;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Test.History
{
    public class AlarmConfigurationTests
    {
        [Fact]
        public void Configure_SetsMaxLength_ForBedId()
        {
            var modelBuilder = new ModelBuilder();
            new AlarmConfiguration().Configure(modelBuilder.Entity<Alarm>());
            var maxLength = modelBuilder.Entity<Alarm>().Metadata.FindProperty("BedId").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForCode()
        {
            var modelBuilder = new ModelBuilder();
            new AlarmConfiguration().Configure(modelBuilder.Entity<Alarm>());
            var maxLength = modelBuilder.Entity<Alarm>().Metadata.FindProperty("Code").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForLevel()
        {
            var modelBuilder = new ModelBuilder();
            new AlarmConfiguration().Configure(modelBuilder.Entity<Alarm>());
            var maxLength = modelBuilder.Entity<Alarm>().Metadata.FindProperty("Level").GetMaxLength();
            Assert.Equal(10, maxLength);
        }
    }
}
