using Common.Exceptions;

namespace Common.Test.Exceptions
{
    public class LogicExceptionTests
    {
        [Fact]
        public void Constructor_DefaultValues_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var exception = new LogicException();

            // Assert
            Assert.Equal(ApiResultStatusCode.LogicError, exception.ApiStatusCode);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Null(exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithMessage_ShouldSetMessageCorrectly()
        {
            // Arrange
            var message = "Logic error occurred";

            // Act
            var exception = new LogicException(message);

            // Assert
            Assert.Equal(ApiResultStatusCode.LogicError, exception.ApiStatusCode);
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
            var exception = new LogicException(additionalData);

            // Assert
            Assert.Equal(ApiResultStatusCode.LogicError, exception.ApiStatusCode);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(additionalData, exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithMessageAndAdditionalData_ShouldSetMessageAndAdditionalDataCorrectly()
        {
            // Arrange
            var message = "Logic error occurred";
            var additionalData = new { Key = "Value" };

            // Act
            var exception = new LogicException(message, additionalData);

            // Assert
            Assert.Equal(ApiResultStatusCode.LogicError, exception.ApiStatusCode);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(additionalData, exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithMessageAndException_ShouldSetMessageAndExceptionCorrectly()
        {
            // Arrange
            var message = "Logic error occurred";
            var innerException = new Exception("Inner exception");

            // Act
            var exception = new LogicException(message, innerException);

            // Assert
            Assert.Equal(ApiResultStatusCode.LogicError, exception.ApiStatusCode);
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
            Assert.Null(exception.AdditionalData);
        }

        [Fact]
        public void Constructor_WithMessageExceptionAndAdditionalData_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var message = "Logic error occurred";
            var innerException = new Exception("Inner exception");
            var additionalData = new { Key = "Value" };

            // Act
            var exception = new LogicException(message, innerException, additionalData);

            // Assert
            Assert.Equal(ApiResultStatusCode.LogicError, exception.ApiStatusCode);
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
            Assert.Equal(additionalData, exception.AdditionalData);
        }
    }
}
