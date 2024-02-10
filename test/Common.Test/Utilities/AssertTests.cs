namespace Common.Test.Utilities
{
    public class AssertTests
    {
        [Fact]
        public void NotNull_Throws_Exception_For_Null_Object()
        {
            // Arrange
            string obj = null;
            string name = "obj";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Common.Utilities.Assert.NotNull(obj, name));
        }

        [Fact]
        public void NotNull_Does_Not_Throw_Exception_For_Non_Null_Object()
        {
            // Arrange
            string obj = "not null";
            string name = "obj";

            // Act & Assert
            Assert.Null(Record.Exception(() => Common.Utilities.Assert.NotNull(obj, name)));
        }

        [Fact]
        public void NotEmpty_Throws_Exception_For_Empty_String()
        {
            // Arrange
            string obj = "";
            string name = "obj";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Common.Utilities.Assert.NotEmpty(obj, name));
        }

        [Fact]
        public void NotEmpty_Throws_Exception_For_Null_Object()
        {
            // Arrange
            string obj = null;
            string name = "obj";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Common.Utilities.Assert.NotEmpty(obj, name));
        }

        [Fact]
        public void NotEmpty_Throws_Exception_For_Empty_List()
        {
            // Arrange
            var obj = new int[] { };
            string name = "obj";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Common.Utilities.Assert.NotEmpty(obj, name));
        }

        [Fact]
        public void NotEmpty_Does_Not_Throw_Exception_For_Non_Empty_Object()
        {
            // Arrange
            var obj = new int[] { 1, 2, 3 };
            string name = "obj";

            // Act & Assert
            Assert.Null(Record.Exception(() => Common.Utilities.Assert.NotEmpty(obj, name)));
        }
    }
}
