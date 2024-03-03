using ConsoleApp.Model;
using ConsoleApp.Service;
using Moq;

namespace ConsoleApp.Test
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void AddUser_ValidUser_UserAddedSuccessfully()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var userService = new UserService(mockService.Object);
            var user = new User { Id = 1, Name = "John", Age = 30 };

            // Act
            userService.AddUser(user);

            //Assert
            mockService.Verify(repo => repo.AddUser(user),Times.Once);
        }

        [TestMethod]
        public void GetUserById_ValidId_ReturnsUser()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var userService = new UserService(mockService.Object);
            var user = new User { Id = 1, Name = "John", Age = 30 };
            mockService.Setup(repo => repo.GetUserById(1)).Returns(user);

            // Act
            var result = userService.GetUserById(1);

            // Assert
            Assert.AreEqual(user, result);
        }
    }
}
