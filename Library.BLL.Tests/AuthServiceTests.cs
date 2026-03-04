using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class AuthServiceTests
    {
        [Fact]
        public void Authenticate_WhenUserExists_ReturnsUser()
        {
            Mock<IUsersRepo> mockRepo = new();

            User user = new User
            {
                UserId = 1,
                Username = "ahmed_ali",
                PasswordHash = "hashed_password_123",
                RoleId = 1
            };

            mockRepo.Setup(repo => repo.GetUserByUsername("ahmed_ali")).Returns(user);

            AuthService service = new(mockRepo.Object);

            var result = service.Authenticate("ahmed_ali");

            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("ahmed_ali", result.Username);
            mockRepo.Verify(repo => repo.GetUserByUsername("ahmed_ali"), Times.Once);
        }

        [Fact]
        public void Authenticate_WhenUserDoesNotExist_ReturnsNull()
        {
            Mock<IUsersRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetUserByUsername("nonexistent")).Returns((User?)null);

            AuthService service = new(mockRepo.Object);

            var result = service.Authenticate("nonexistent");

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetUserByUsername("nonexistent"), Times.Once);
        }
    }
}
