using Common.Exceptions;
using System.Net;

namespace Common.Test.Exceptions
{
    public class AppExceptionTests
    {
        [Fact]
        public void Constructor_DefaultValues_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var exception = new AppException();

            // Assert
            Assert.Equal(ApiResultStatusCode.ServerError, exception.ApiStatusCode);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.HttpStatusCode);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Null(exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithStatusCode_ShouldSetStatusCodeCorrectly()
        {
            // Arrange & Act
            var exception = new AppException(ApiResultStatusCode.NotFound);

            // Assert
            Assert.Equal(ApiResultStatusCode.NotFound, exception.ApiStatusCode);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.HttpStatusCode);
        }
    }
}
