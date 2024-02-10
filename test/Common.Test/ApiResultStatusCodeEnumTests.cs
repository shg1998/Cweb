using System.ComponentModel.DataAnnotations;

namespace Common.Test
{
    public class ApiResultStatusCodeEnumTests
    {
        [Fact]
        public void ApiResultStatusCode_Enum_Values_Should_Be_Correct()
        {
            Assert.Equal(0, (int)ApiResultStatusCode.Success);
            Assert.Equal(1, (int)ApiResultStatusCode.ServerError);
            Assert.Equal(2, (int)ApiResultStatusCode.BadRequest);
            Assert.Equal(3, (int)ApiResultStatusCode.NotFound);
            Assert.Equal(4, (int)ApiResultStatusCode.ListEmpty);
            Assert.Equal(5, (int)ApiResultStatusCode.LogicError);
            Assert.Equal(6, (int)ApiResultStatusCode.UnAuthorized);
        }


        [Fact]
        public void Enum_Values_Should_Have_Correct_Display_Names()
        {
            // Arrange
            var expectedDisplayNames = new string[]
            {
            "Mission accomplished !",
            "An error has occurred on the server",
            "The parameters sent are not valid",
            "Not found",
            "The list is empty",
            "A processing error occurred",
            "Authentication error"
            };

            // Act
            var enumValues = Enum.GetValues(typeof(ApiResultStatusCode));

            // Assert
            Assert.Equal(expectedDisplayNames.Length, enumValues.Length);
            for (int i = 0; i < enumValues.Length; i++)
            {
                ApiResultStatusCode enumValue = (ApiResultStatusCode)enumValues.GetValue(i);
                var displayNameAttribute = typeof(ApiResultStatusCode).GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    as DisplayAttribute[];

                Assert.NotNull(displayNameAttribute);
                Assert.NotEmpty(displayNameAttribute);
                Assert.Equal(expectedDisplayNames[i], displayNameAttribute.First().Name);
            }
        }
    }
}
