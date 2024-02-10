using Common.Utilities;

namespace Common.Test.Utilities
{
    public class OdataUtilsTests
    {
        [Fact]
        public void GetSkipLimit_Returns_Correct_Values()
        {
            // Arrange
            var queries = "skip=10&top=5";
            var list = Enumerable.Range(1, 100).AsQueryable();

            // Act
            var result = OdataUtils.GetSkipLimit(queries, list);

            // Assert
            Xunit.Assert.Equal(10, result.Skip);
            Xunit.Assert.Equal(5, result.Limit);
            Xunit.Assert.Equal(100, result.TotalCount);
            Xunit.Assert.Equal(3, result.CurrentPageNumber);
        }
    }
}
