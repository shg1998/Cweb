using Entities.Role;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities.Test.User
{
    public class UserConfigurationTests
    {
        [Fact]
        public void Configure_UserNameProperty_ShouldBeRequired()
        {
            var modelBuilder = new ModelBuilder();
            var entityBuilder = modelBuilder.Entity<Entities.User.User>();

            var configuration = new UserConfiguration();

            // Act
            configuration.Configure(entityBuilder);

            // Assert
            var property = entityBuilder.Metadata.FindProperty(nameof(Entities.User.User.UserName));
            Assert.True(property.RequiresOriginalValue());
        }

        [Fact]
        public void Configure_UserNameProperty_ShouldHaveMaxLength100()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var entityBuilder = modelBuilder.Entity<Entities.User.User>();
            var configuration = new UserConfiguration();

            // Act
            configuration.Configure(entityBuilder);

            // Assert
            var property = entityBuilder.Metadata.FindProperty(nameof(Entities.User.User.UserName));
            Assert.Equal(100, property.GetMaxLength());
        }

        [Fact]
        public void Configure_FullNameProperty_ShouldHaveMaxLength100()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var entityBuilder = modelBuilder.Entity<Entities.User.User>();
            var configuration = new UserConfiguration();

            // Act
            configuration.Configure(entityBuilder);

            // Assert
            var property = entityBuilder.Metadata.FindProperty(nameof(Entities.User.User.FullName));
            Assert.Equal(100, property.GetMaxLength());
        }
    }
}
