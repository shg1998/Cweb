namespace Entities.Test.User
{
    public class UserTests
    { 

        [Fact]
        public void User_FullName_CanBeSetAndGet()
        {
            // Arrange
            var user = new Entities.User.User
            {
                // Act
                FullName = "John Doe"
            };

            // Assert
            Assert.Equal("John Doe", user.FullName);
        }

        [Fact]
        public void User_FullName_CanBeNull()
        {
            // Arrange
            var user = new Entities.User.User
            {
                // Act
                FullName = null
            };

            // Assert
            Assert.Null(user.FullName);
        }

        [Fact]
        public void User_IsActive_CanBeSetAndGet()
        {
            // Arrange
            var user = new Entities.User.User
            {
                // Act
                IsActive = true
            };

            // Assert
            Assert.True(user.IsActive);

            // Act
            user.IsActive = false;

            // Assert
            Assert.False(user.IsActive);
        }


        [Fact]
        public void User_LastLoginDate_CanBeSetAndGet()
        {
            // Arrange
            var user = new Entities.User.User();
            var now = DateTimeOffset.Now;

            // Act
            user.LastLoginDate = now;

            // Assert
            Assert.Equal(now, user.LastLoginDate);
        }

        [Fact]
        public void User_LastLoginDate_CanBeNull()
        {
            // Arrange
            var user = new Entities.User.User
            {
                // Act
                LastLoginDate = null
            };

            // Assert
            Assert.Null(user.LastLoginDate);
        }

    }
}
