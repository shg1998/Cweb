namespace Entities.Test.UserCentral
{
    public class UserCentralTests
    {
        [Fact]
        public void UserCentralPropertiesTest()
        {
            // Arrange
            var userCentral = new Entities.UserCentral.UserCentral
            {
                UserId = 1,
                CentralId = 2,
                User = new Entities.User.User { Id = 10, UserName = "Alice" },
                Central = new Entities.User.User { Id = 20, UserName = "Bob" }
            };

            // Assert
            Assert.Equal(1, userCentral.UserId);
            Assert.Equal(2, userCentral.CentralId);
            Assert.NotNull(userCentral.User);
            Assert.NotNull(userCentral.Central);
            Assert.Equal(10, userCentral.User.Id);
            Assert.Equal("Alice", userCentral.User.UserName);
            Assert.Equal(20, userCentral.Central.Id);
            Assert.Equal("Bob", userCentral.Central.UserName);
        }
    }
}
