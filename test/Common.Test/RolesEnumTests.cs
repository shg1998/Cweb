using System.ComponentModel.DataAnnotations;

namespace Common.Test
{
    public class RolesEnumTests
    {
        [Fact]
        public void Roles_Enum_Values_Should_Be_Correct()
        {
            Assert.Equal(0, (int)RolesEnum.SuperAdmin);
            Assert.Equal(1, (int)RolesEnum.Admin);
            Assert.Equal(2, (int)RolesEnum.Doctor);
            Assert.Equal(3, (int)RolesEnum.Central);
        }

        [Fact]
        public void Enum_Values_Should_Have_Correct_Display_Names()
        {
            // Arrange
            var expectedDisplayNames = new string[]
            {
            "SuperAdmin",
            "Admin",
            "Doctor",
            "Central"
            };

            // Act
            var enumValues = Enum.GetValues(typeof(RolesEnum));

            // Assert
            Assert.Equal(expectedDisplayNames.Length, enumValues.Length);
            for (int i = 0; i < enumValues.Length; i++)
            {
                var enumValue = (RolesEnum)enumValues.GetValue(i);
                var displayNameAttribute = typeof(RolesEnum).GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    as DisplayAttribute[];

                Assert.NotNull(displayNameAttribute);
                Assert.NotEmpty(displayNameAttribute);
                Assert.Equal(expectedDisplayNames[i], displayNameAttribute.First().Name);
            }
        }
    }
}
