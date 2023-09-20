using LevelUp.Models;

namespace LevelUpTesting
{
    [Collection("Controller Tests")]
    public class ModelTests
    {
        [Fact]
        public void User_Constructor_CreatesUserObject()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123" };
            user.Password = user.Encrypt(user.Password);

            Assert.Equal("John Doe", user.Name);
            Assert.Equal("jdoe", user.Username);
            Assert.Equal(user.Encrypt("123"), user.Password);
        }
    }
}