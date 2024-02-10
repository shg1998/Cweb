using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test.Utilities
{
    public class ContentTypesTests
    {
        [Theory]
        [InlineData(".png", "image/png")]
        [InlineData(".jpg", "image/jpeg")]
        [InlineData(".jpeg", "image/jpeg")]
        public void GetContentType_Returns_Correct_ContentType(string extension, string expectedContentType)
        {
            // Arrange
            var path = $"test{extension}";

            // Act
            var result = ContentTypes.GetContentType(path);

            // Assert
            Xunit.Assert.Equal(expectedContentType, result);
        }

        [Fact]
        public void GetContentType_Throws_Exception_For_Unknown_Extension()
        {
            // Arrange
            var path = "test.xyz";

            // Act
            Action act = () => ContentTypes.GetContentType(path);

            // Assert
            Xunit.Assert.Throws<KeyNotFoundException>(act);
        }
    }
}
