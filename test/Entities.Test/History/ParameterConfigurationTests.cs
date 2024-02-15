using Entities.History;
using Microsoft.EntityFrameworkCore;

namespace Entities.Test.History
{
    public class ParameterConfigurationTests
    {
        [Fact]
        public void Configure_SetsMaxLength_ForBedId()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("BedId").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForHr()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Hr").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForRr()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Rr").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForSpo2()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Spo2").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForT1()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("T1").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForT2()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("T2").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForDt()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Dt").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForIbp1Sys()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Ibp1Sys").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForIbp1Dia()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Ibp1Dia").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForIbp1Map()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Ibp1Map").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForIbp2Sys()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Ibp2Sys").GetMaxLength();
            Assert.Equal(10, maxLength);
        }
        
        [Fact]
        public void Configure_SetsMaxLength_ForIbp2Dia()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Ibp2Dia").GetMaxLength();
            Assert.Equal(10, maxLength);
        }
        
        [Fact]
        public void Configure_SetsMaxLength_ForIbp2Map()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Ibp2Map").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForAwrr()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("Awrr").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForEtcO2()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("EtcO2").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForFiCo2()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("FiCo2").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForNibpSys()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("NibpSys").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForNibpDia()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("NibpDia").GetMaxLength();
            Assert.Equal(10, maxLength);
        }

        [Fact]
        public void Configure_SetsMaxLength_ForNibpMap()
        {
            var modelBuilder = new ModelBuilder();
            new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
            var maxLength = modelBuilder.Entity<Parameter>().Metadata.FindProperty("NibpMap").GetMaxLength();
            Assert.Equal(10, maxLength);
        }
    }
}
