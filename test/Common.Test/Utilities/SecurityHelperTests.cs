using Common.Utilities;

namespace Common.Test.Utilities
{
    public class SecurityHelperTests
    {
        [Fact]
        public void GetSha256Hash_Returns_Correct_Hash()
        {
            // Arrange
            var input = "password123";

            // Act
            var result = SecurityHelper.GetSha256Hash(input);

            // Assert
            var expectedHash = "75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=";
            Xunit.Assert.Equal(expectedHash, result);
        }
    }
}
