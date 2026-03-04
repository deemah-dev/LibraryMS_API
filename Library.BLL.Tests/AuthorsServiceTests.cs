using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class AuthorsServiceTests
    {
        //AddAuthor
        [Fact]
        public void AddAuthor_WhenCalled_ReturnsAuthorId()
        {
            Mock<IAuthorsRepo> mockRepo = new();

            Author author = new Author
            {
                Name = "Ahmed Ali"
            };

            mockRepo.Setup(repo => repo.InsertAuthor(author)).Returns(1);

            AuthorsService service = new(mockRepo.Object);

            var result = service.AddAuthor(author);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertAuthor(author), Times.Once);
        }


        //UpdateAuthor
        [Fact]
        public void UpdateAuthor_WhenCalled_ReturnsTrue()
        {
            Mock<IAuthorsRepo> mockRepo = new();

            Author author = new Author
            {
                AuthorId = 1,
                Name = "Mohamed Ali"
            };

            mockRepo.Setup(repo => repo.UpdateAuthor(author)).Returns(true);

            AuthorsService service = new(mockRepo.Object);

            var result = service.UpdateAuthor(author);

            Assert.True(result);
            mockRepo.Verify(repo => repo.UpdateAuthor(author), Times.Once);
        }

        [Fact]
        public void UpdateAuthor_WhenFailed_ReturnsFalse()
        {
            Mock<IAuthorsRepo> mockRepo = new();

            Author author = new Author
            {
                AuthorId = 999,
                Name = "Non-existent Author"
            };

            mockRepo.Setup(repo => repo.UpdateAuthor(author)).Returns(false);

            AuthorsService service = new(mockRepo.Object);

            var result = service.UpdateAuthor(author);

            Assert.False(result);
            mockRepo.Verify(repo => repo.UpdateAuthor(author), Times.Once);
        }


        //GetAuthorByName
        [Fact]
        public void GetAuthorByName_WhenAuthorExists_ReturnsAuthor()
        {
            Mock<IAuthorsRepo> mockRepo = new();

            Author author = new Author
            {
                AuthorId = 1,
                Name = "Ahmed Ali"
            };

            mockRepo.Setup(repo => repo.GetAuthorByName("Ahmed Ali")).Returns(author);

            AuthorsService service = new(mockRepo.Object);

            var result = service.GetAuthorByName("Ahmed Ali");

            Assert.NotNull(result);
            Assert.Equal(1, result.AuthorId);
            Assert.Equal("Ahmed Ali", result.Name);
            mockRepo.Verify(repo => repo.GetAuthorByName("Ahmed Ali"), Times.Once);
        }

        [Fact]
        public void GetAuthorByName_WhenAuthorDoesNotExist_ReturnsNull()
        {
            Mock<IAuthorsRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetAuthorByName("Non-existent")).Returns((Author?)null);

            AuthorsService service = new(mockRepo.Object);

            var result = service.GetAuthorByName("Non-existent");

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetAuthorByName("Non-existent"), Times.Once);
        }


        //GetAllAuthors
        [Fact]
        public void GetAllAuthors_WhenCalled_ReturnsListOfAuthors()
        {
            Mock<IAuthorsRepo> mockRepo = new();

            List<Author> authors = new()
            {
                new Author { AuthorId = 1, Name = "Ahmed Ali" },
                new Author { AuthorId = 2, Name = "Mohamed Ali" },
                new Author { AuthorId = 3, Name = "Fatima Hassan" }
            };

            mockRepo.Setup(repo => repo.GetAuthors()).Returns(authors);

            AuthorsService service = new(mockRepo.Object);

            var result = service.GetAllAuthors();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal("Ahmed Ali", result.First().Name);
            mockRepo.Verify(repo => repo.GetAuthors(), Times.Once);
        }

        [Fact]
        public void GetAllAuthors_WhenNoAuthorsExist_ReturnsNull()
        {
            Mock<IAuthorsRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetAuthors()).Returns((IEnumerable<Author>?)null);

            AuthorsService service = new(mockRepo.Object);

            var result = service.GetAllAuthors();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetAuthors(), Times.Once);
        }
    }
}
