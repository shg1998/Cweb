using Common.Utilities;
using System.Security.Claims;

namespace Common.Test.Utilities
{
    public class IdentityExtensionsTests
    {
        [Fact]
        public void FindFirstValue_ReturnsValue_WhenClaimExists()
        {
            // Arrange
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "John") });

            // Act
            var result = identity.FindFirstValue(ClaimTypes.Name);

            // Assert
            Xunit.Assert.Equal("John", result);
        }

        [Fact]
        public void FindFirstValue_ReturnsNull_WhenClaimDoesNotExist()
        {
            // Arrange
            var identity = new ClaimsIdentity();

            // Act
            var result = identity.FindFirstValue(ClaimTypes.Name);

            // Assert
            Xunit.Assert.Null(result);
        }
    }
}
