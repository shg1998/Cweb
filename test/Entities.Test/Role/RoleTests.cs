namespace Entities.Test.Role
{
    public class RoleTests
    {
        [Fact]
        public void Create_SetsPropertiesCorrectly()
        {
            // Arrange
            var name ="name";
            var description = "description";

            // Act
            var role = new Entities.Role.Role()
            {
                Name = name,
                Description = description,
            };

            // Assert
            Assert.Equal(name, role.Name);
            Assert.Equal(description, role.Description);
        }
    }
}
