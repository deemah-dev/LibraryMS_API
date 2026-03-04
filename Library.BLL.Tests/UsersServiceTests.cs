using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class UsersServiceTests
    {
        //AddUser
        [Fact]
        public void AddUser_WhenCalled_ReturnsUserId()
        {
            Mock<IUsersRepo> mockRepo = new();

            User user = new User
            {
                Username = "ahmed_ali",
                PasswordHash = "hashed_password_123",
                RoleId = 1
            };

            mockRepo.Setup(repo => repo.InsertUser(user)).Returns(1);

            UsersService service = new(mockRepo.Object);

            var result = service.AddUser(user);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertUser(user), Times.Once);
        }


        //UpdateUser
        [Fact]
        public void UpdateUser_WhenCalled_ReturnsTrue()
        {
            Mock<IUsersRepo> mockRepo = new();

            User user = new User
            {
                UserId = 1,
                Username = "ahmed_ali",
                PasswordHash = "new_hashed_password",
                RoleId = 2
            };

            mockRepo.Setup(repo => repo.UpdateUser(user)).Returns(true);

            UsersService service = new(mockRepo.Object);

            var result = service.UpdateUser(user);

            Assert.True(result);
            mockRepo.Verify(repo => repo.UpdateUser(user), Times.Once);
        }


        //RemoveUser
        [Fact]
        public void RemoveUser_WhenCalled_ReturnsTrue()
        {
            Mock<IUsersRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.DeleteUser(1)).Returns(true);

            UsersService service = new(mockRepo.Object);

            var result = service.RemoveUser(1);

            Assert.True(result);
            mockRepo.Verify(repo => repo.DeleteUser(1), Times.Once);
        }


        //GetUserById
        [Fact]
        public void GetUser_WhenUserExists_ReturnsUser()
        {
            Mock<IUsersRepo> mockRepo = new();

            User user = new User
            {
                UserId = 1,
                Username = "ahmed_ali",
                PasswordHash = "hashed_password_123",
                RoleId = 1
            };

            mockRepo.Setup(repo => repo.GetUserById(1)).Returns(user);

            UsersService service = new(mockRepo.Object);

            var result = service.GetUser(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("ahmed_ali", result.Username);
            mockRepo.Verify(repo => repo.GetUserById(1), Times.Once);
        }

        [Fact]
        public void GetUser_WhenUserDoesNotExist_ReturnsNull()
        {
            Mock<IUsersRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetUserById(999)).Returns((User?)null);

            UsersService service = new(mockRepo.Object);

            var result = service.GetUser(999);

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetUserById(999), Times.Once);
        }


        //GetUserByUsername
        [Fact]
        public void GetUserByUsername_WhenUserExists_ReturnsUser()
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

            UsersService service = new(mockRepo.Object);

            var result = service.GetUserByUsername("ahmed_ali");

            Assert.NotNull(result);
            Assert.Equal("ahmed_ali", result.Username);
            mockRepo.Verify(repo => repo.GetUserByUsername("ahmed_ali"), Times.Once);
        }

        [Fact]
        public void GetUserByUsername_WhenUserDoesNotExist_ReturnsNull()
        {
            Mock<IUsersRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetUserByUsername("nonexistent")).Returns((User?)null);

            UsersService service = new(mockRepo.Object);

            var result = service.GetUserByUsername("nonexistent");

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetUserByUsername("nonexistent"), Times.Once);
        }


        //GetAllUsers
        [Fact]
        public void GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            Mock<IUsersRepo> mockRepo = new();

            List<User> users = new()
            {
                new User { UserId = 1, Username = "ahmed_ali", PasswordHash = "hash1", RoleId = 1 },
                new User { UserId = 2, Username = "fatima_hassan", PasswordHash = "hash2", RoleId = 2 },
                new User { UserId = 3, Username = "admin_user", PasswordHash = "hash3", RoleId = 3 }
            };

            mockRepo.Setup(repo => repo.GetUsers()).Returns(users);

            UsersService service = new(mockRepo.Object);

            var result = service.GetAllUsers();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal("ahmed_ali", result.First().Username);
            mockRepo.Verify(repo => repo.GetUsers(), Times.Once);
        }

        [Fact]
        public void GetAllUsers_WhenNoUsersExist_ReturnsNull()
        {
            Mock<IUsersRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetUsers()).Returns((IEnumerable<User>?)null);

            UsersService service = new(mockRepo.Object);

            var result = service.GetAllUsers();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetUsers(), Times.Once);
        }
    }
}
