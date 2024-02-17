using Entities.UserCentral;
using Microsoft.EntityFrameworkCore;

namespace Entities.Test.UserCentral
{
    public class UserCentralConfigurationTests
    {
        [Fact]
        public void Configure_SetsCorrectForeignKeysAndDeleteBehavior()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var builder = modelBuilder.Entity<Entities.UserCentral.UserCentral>();
            var configuration = new UserCentralConfiguration();

            // Act
            configuration.Configure(builder);

            // Assert
            var foreignKeyUser = builder.Metadata.FindNavigation(nameof(Entities.UserCentral.UserCentral.User));
            var foreignKeyCentral = builder.Metadata.FindNavigation(nameof(Entities.UserCentral.UserCentral.Central));

            Assert.NotNull(foreignKeyUser);
            Assert.NotNull(foreignKeyCentral);

            Assert.Equal(DeleteBehavior.Cascade, foreignKeyUser.ForeignKey.DeleteBehavior);
            Assert.Equal(DeleteBehavior.Restrict, foreignKeyCentral.ForeignKey.DeleteBehavior);
        }

        [Fact]
        public void Configure_SetsCorrectRelationships()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var builder = modelBuilder.Entity<Entities.UserCentral.UserCentral>();
            var configuration = new UserCentralConfiguration();

            // Act
            configuration.Configure(builder);

            // Assert
            var userNavigation = builder.Metadata.FindNavigation(nameof(Entities.UserCentral.UserCentral.User));
            var centralNavigation = builder.Metadata.FindNavigation(nameof(Entities.UserCentral.UserCentral.Central));

            Assert.NotNull(userNavigation);
            Assert.NotNull(centralNavigation);

            Assert.True(userNavigation.IsOnDependent);
            Assert.True(centralNavigation.IsOnDependent);
        }
    }
}
