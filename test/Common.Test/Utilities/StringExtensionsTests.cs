using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test.Utilities
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("abc", true)]
        public void HasValue_ReturnsExpected(string input, bool expected)
        {
            // Act
            var result = input.HasValue();

            // Assert
            Xunit.Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("123", 123)]
        [InlineData("-456", -456)]
        public void ToInt_ReturnsExpected(string input, int expected)
        {
            // Act
            var result = input.ToInt();

            // Assert
            Xunit.Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData("123.45", 123.45)]
        [InlineData("-67.89", -67.89)]
        public void ToDecimal_ReturnsExpected(string input, decimal expected)
        {
            // Act
            var result = input.ToDecimal();

            // Assert
            Xunit.Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(123, "123")]
        [InlineData(-456, "-456")]
        public void ToNumeric_Int_ReturnsExpected(int input, string expected)
        {
            // Act
            var result = input.ToNumeric();

            // Assert
            Xunit.Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(123.45, "123")]
        [InlineData(-67.89, "-68")]
        public void ToNumeric_Decimal_ReturnsExpected(decimal input, string expected)
        {
            // Act
            var result = input.ToNumeric();

            // Assert
            Xunit.Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(123456, "$123,456")]
        [InlineData(-789012, "($789,012)")]
        public void ToCurrency_Int_ReturnsExpected(int input, string expected)
        {
            // Act
            var result = input.ToCurrency();

            // Assert
            Xunit.Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(123456.78, "$123,457")]
        [InlineData(-90123.45, "($90,123)")]
        public void ToCurrency_Decimal_ReturnsExpected(decimal input, string expected)
        {
            // Act
            var result = input.ToCurrency();

            // Assert
            Xunit.Assert.Equal(expected, result);
        }

    }
}
