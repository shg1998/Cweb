using Entities.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Entities.Test.Role
{
    public class RoleConfigurationTests
    {
        [Fact]
        public void Configure_NameProperty_ShouldBeRequired()
        {
            var modelBuilder = new ModelBuilder();
            var entityBuilder = modelBuilder.Entity<Entities.Role.Role>();

            var configuration = new RoleConfiguration();

            // Act
            configuration.Configure(entityBuilder);

            // Assert
            var property = entityBuilder.Metadata.FindProperty(nameof(Entities.Role.Role.Name));
            Assert.True(property.RequiresOriginalValue());
        }

        [Fact]
        public void Configure_NameProperty_ShouldHaveMaxLength50()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var entityBuilder = modelBuilder.Entity<Entities.Role.Role>();

            var configuration = new RoleConfiguration();

            // Act
            configuration.Configure(entityBuilder);

            // Assert
            var property = entityBuilder.Metadata.FindProperty(nameof(Entities.Role.Role.Name));
            Assert.Equal(50, property.GetMaxLength());
        }

        [Fact]
        public void Configure_DescriptionProperty_ShouldBeRequired()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var entityBuilder = modelBuilder.Entity<Entities.Role.Role>();

            var configuration = new RoleConfiguration();

            // Act
            configuration.Configure(entityBuilder);

            // Assert
            var property = entityBuilder.Metadata.FindProperty(nameof(Entities.Role.Role.Description));
            Assert.True(property.RequiresOriginalValue());
        }

        [Fact]
        public void Configure_DescriptionProperty_ShouldHaveMaxLength100()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var entityBuilder = modelBuilder.Entity<Entities.Role.Role>();

            var configuration = new RoleConfiguration();

            // Act
            configuration.Configure(entityBuilder);

            // Assert
            var property = entityBuilder.Metadata.FindProperty(nameof(Entities.Role.Role.Description));
            Assert.Equal(100, property.GetMaxLength());
        }
    }
}
