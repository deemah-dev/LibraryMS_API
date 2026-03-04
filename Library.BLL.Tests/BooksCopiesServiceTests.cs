using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class BooksCopiesServiceTests
    {
        //AddCopy
        [Fact]
        public void AddCopy_WhenCalled_ReturnsCopyId()
        {
            Mock<IBooksCopiesRepo> mockRepo = new();

            BookCopy copy = new BookCopy
            {
                BookId = 1,
                IsAvailable = true
            };

            mockRepo.Setup(repo => repo.InsertBookCopy(copy)).Returns(1);

            BooksCopiesService service = new(mockRepo.Object);

            var result = service.AddCopy(copy);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertBookCopy(copy), Times.Once);
        }


        //RemoveCopy
        [Fact]
        public void RemoveCopy_WhenCalled_ReturnsTrue()
        {
            Mock<IBooksCopiesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.DeleteBookCopy(1)).Returns(true);

            BooksCopiesService service = new(mockRepo.Object);

            var result = service.RemoveCopy(1);

            Assert.True(result);
            mockRepo.Verify(repo => repo.DeleteBookCopy(1), Times.Once);
        }


        //GetCopyIdByBook
        [Fact]
        public void GetCopyIdByBook_WhenCopyExists_ReturnsCopyId()
        {
            Mock<IBooksCopiesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBookCopyId(1)).Returns(1);

            BooksCopiesService service = new(mockRepo.Object);

            var result = service.GetCopyIdByBook(1);

            Assert.NotNull(result);
            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.GetBookCopyId(1), Times.Once);
        }

        [Fact]
        public void GetCopyIdByBook_WhenCopyDoesNotExist_ReturnsNull()
        {
            Mock<IBooksCopiesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBookCopyId(999)).Returns((int?)null);

            BooksCopiesService service = new(mockRepo.Object);

            var result = service.GetCopyIdByBook(999);

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBookCopyId(999), Times.Once);
        }


        //GetAllCopies
        [Fact]
        public void GetAllCopies_WhenCalled_ReturnsListOfCopies()
        {
            Mock<IBooksCopiesRepo> mockRepo = new();

            List<BookCopy> copies = new()
            {
                new BookCopy { CopyId = 1, BookId = 1, IsAvailable = true },
                new BookCopy { CopyId = 2, BookId = 1, IsAvailable = false },
                new BookCopy { CopyId = 3, BookId = 2, IsAvailable = true }
            };

            mockRepo.Setup(repo => repo.GetBookCopies()).Returns(copies);

            BooksCopiesService service = new(mockRepo.Object);

            var result = service.GetAllCopies();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.True(result.First().IsAvailable);
            mockRepo.Verify(repo => repo.GetBookCopies(), Times.Once);
        }

        [Fact]
        public void GetAllCopies_WhenNoCopiesExist_ReturnsNull()
        {
            Mock<IBooksCopiesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBookCopies()).Returns((IEnumerable<BookCopy>?)null);

            BooksCopiesService service = new(mockRepo.Object);

            var result = service.GetAllCopies();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBookCopies(), Times.Once);
        }
    }
}
