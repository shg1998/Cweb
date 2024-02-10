using Common.Exceptions;

namespace Common.Test.Exceptions
{
    public class NotFoundExceptionTests
    {
        [Fact]
        public void Constructor_DefaultValues_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var exception = new NotFoundException();

            // Assert
            Assert.Equal(ApiResultStatusCode.NotFound, exception.ApiStatusCode);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Null(exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithMessage_ShouldSetMessageCorrectly()
        {
            // Arrange
            var message = "Resource not found";

            // Act
            var exception = new NotFoundException(message);

            // Assert
            Assert.Equal(ApiResultStatusCode.NotFound, exception.ApiStatusCode);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Null(exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithAdditionalData_ShouldSetAdditionalDataCorrectly()
        {
            // Arrange
            var additionalData = new { Key = "Value" };

            // Act
            var exception = new NotFoundException(additionalData);

            // Assert
            Assert.Equal(ApiResultStatusCode.NotFound, exception.ApiStatusCode);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(additionalData, exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithMessageAndAdditionalData_ShouldSetMessageAndAdditionalDataCorrectly()
        {
            // Arrange
            var message = "Resource not found";
            var additionalData = new { Key = "Value" };

            // Act
            var exception = new NotFoundException(message, additionalData);

            // Assert
            Assert.Equal(ApiResultStatusCode.NotFound, exception.ApiStatusCode);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(additionalData, exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithMessageAndException_ShouldSetMessageAndExceptionCorrectly()
        {
            // Arrange
            var message = "Resource not found";
            var innerException = new Exception("Inner exception");

            // Act
            var exception = new NotFoundException(message, innerException);

            // Assert
            Assert.Equal(ApiResultStatusCode.NotFound, exception.ApiStatusCode);
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
            Assert.Null(exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithMessageExceptionAndAdditionalData_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var message = "Resource not found";
            var innerException = new Exception("Inner exception");
            var additionalData = new { Key = "Value" };

            // Act
            var exception = new NotFoundException(message, innerException, additionalData);

            // Assert
            Assert.Equal(ApiResultStatusCode.NotFound, exception.ApiStatusCode);
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
            Assert.Equal(additionalData, exception.AdditionalData);
        }
    }
}
