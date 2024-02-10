using Common.Exceptions;

namespace Common.Test.Exceptions
{
    public class BadRequestExceptionTests
    {
            [Fact]
            public void Constructor_DefaultValues_ShouldSetPropertiesCorrectly()
            {
                // Arrange & Act
                var exception = new BadRequestException();

                // Assert
                Assert.Equal(ApiResultStatusCode.BadRequest, exception.ApiStatusCode);
                Assert.NotNull(exception.Message);
                Assert.Null(exception.InnerException);
                Assert.Null(exception.AdditionalData);
            }

            [Fact]
            public void Constructor_WithMessage_ShouldSetMessageCorrectly()
            {
                // Arrange
                var message = "Bad request occurred";

                // Act
                var exception = new BadRequestException(message);

                // Assert
                Assert.Equal(ApiResultStatusCode.BadRequest, exception.ApiStatusCode);
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
                var exception = new BadRequestException(additionalData);

                // Assert
                Assert.Equal(ApiResultStatusCode.BadRequest, exception.ApiStatusCode);
                Assert.NotNull(exception.Message);
                Assert.Null(exception.InnerException);
                Assert.Equal(additionalData, exception.AdditionalData);
            }

            [Fact]
            public void Constructor_WithMessageAndAdditionalData_ShouldSetMessageAndAdditionalDataCorrectly()
            {
                // Arrange
                var message = "Bad request occurred";
                var additionalData = new { Key = "Value" };

                // Act
                var exception = new BadRequestException(message, additionalData);

                // Assert
                Assert.Equal(ApiResultStatusCode.BadRequest, exception.ApiStatusCode);
                Assert.Equal(message, exception.Message);
                Assert.Null(exception.InnerException);
                Assert.Equal(additionalData, exception.AdditionalData);
            }

            [Fact]
            public void Constructor_WithMessageAndException_ShouldSetMessageAndExceptionCorrectly()
            {
                // Arrange
                var message = "Bad request occurred";
                var innerException = new Exception("Inner exception");

                // Act
                var exception = new BadRequestException(message, innerException);

                // Assert
                Assert.Equal(ApiResultStatusCode.BadRequest, exception.ApiStatusCode);
                Assert.Equal(message, exception.Message);
                Assert.Equal(innerException, exception.InnerException);
                Assert.Null(exception.AdditionalData);
            }

            [Fact]
            public void Constructor_WithMessageExceptionAndAdditionalData_ShouldSetAllPropertiesCorrectly()
            {
                // Arrange
                var message = "Bad request occurred";
                var innerException = new Exception("Inner exception");
                var additionalData = new { Key = "Value" };

                // Act
                var exception = new BadRequestException(message, innerException, additionalData);

                // Assert
                Assert.Equal(ApiResultStatusCode.BadRequest, exception.ApiStatusCode);
                Assert.Equal(message, exception.Message);
                Assert.Equal(innerException, exception.InnerException);
                Assert.Equal(additionalData, exception.AdditionalData);
            }
        }
    }
