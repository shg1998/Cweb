using Common.Utilities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Test.Utilities
{
    public class EnumExtensionsTests
    {
        public enum TestEnum
        {
            [Description("First Description")]
            [Display(Name = "First Name")]
            First,

            [Description("Second Description")]
            [Display(Name = "Second Name")]
            Second,

            [Description("Third Description")]
            [Display(Name = "Third Name")]
            Third
        }

        [Fact]
        public void GetEnumValues_Returns_All_Enum_Values()
        {
            // Act
            var result = TestEnum.First.GetEnumValues();

            // Assert
            Xunit.Assert.Equal(3, result.Count());
            Xunit.Assert.Contains(TestEnum.First, result);
            Xunit.Assert.Contains(TestEnum.Second, result);
            Xunit.Assert.Contains(TestEnum.Third, result);
        }

        [Fact]
        public void GetEnumFlags_Returns_Correct_Enum_Flags()
        {
            // Arrange
            TestEnum input = TestEnum.First | TestEnum.Third;

            // Act
            var result = input.GetEnumFlags();

            // Assert
            Xunit.Assert.Equal(2, result.Count());
            Xunit.Assert.Contains(TestEnum.First, result);
            Xunit.Assert.Contains(TestEnum.Third, result);
        }

        [Theory]
        [InlineData(TestEnum.First, "First Name")]
        [InlineData(TestEnum.Second, "Second Name")]
        [InlineData(TestEnum.Third, "Third Name")]
        public void ToDisplay_Returns_Correct_Display_Name(TestEnum value, string expectedDisplayName)
        {
            // Act
            var result = value.ToDisplay(DisplayProperty.Name);

            // Assert
            Xunit.Assert.Equal(expectedDisplayName, result);
        }

        [Fact]
        public void ToDictionary_Returns_Correct_Dictionary()
        {
            // Act
            var result = TestEnum.First.ToDictionary();

            // Assert
            Xunit.Assert.Equal(3, result.Count);
            Xunit.Assert.True(result.ContainsKey(0));
            Xunit.Assert.True(result.ContainsKey(1));
            Xunit.Assert.True(result.ContainsKey(2));
            Xunit.Assert.Equal("First Name", result[0]);
            Xunit.Assert.Equal("Second Name", result[1]);
            Xunit.Assert.Equal("Third Name", result[2]);
        }
    }
}
