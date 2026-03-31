using Library.BLL.Services;
using Library.Core.Dtos.BookCopyDtos;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;
using AutoMapper;
using Library.BLL.Interfaces;

namespace Library.BLL.Tests
{
    public class BooksCopiesServiceTests
    {
        //AddCopy
        [Fact]
        public void AddCopy_WhenCalled_ReturnsCopyId()
        {
            var mockRepo = new Mock<IBooksCopiesRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockBooks = new Mock<IBooksService>();

            AddBookCopyDto addDto = new AddBookCopyDto { BookId = 1 };
            BookCopy mapped = new BookCopy { BookId = 1 };

            mockMapper.Setup(m => m.Map<BookCopy>(It.IsAny<AddBookCopyDto>())).Returns(mapped);
            mockRepo.Setup(r => r.InsertBookCopy(mapped)).Returns(1);

            BooksCopiesService service = new(mockRepo.Object, mockMapper.Object, mockBooks.Object);

            var result = service.AddCopy(addDto);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertBookCopy(mapped), Times.Once);
        }


        //RemoveCopy
        [Fact]
        public void RemoveCopy_WhenCalled_ReturnsTrue()
        {
            var mockRepo = new Mock<IBooksCopiesRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockBooks = new Mock<IBooksService>();

            mockRepo.Setup(repo => repo.DeleteBookCopy(1)).Returns(true);

            BooksCopiesService service = new(mockRepo.Object, mockMapper.Object, mockBooks.Object);

            var result = service.RemoveCopy(1);

            Assert.True(result);
            mockRepo.Verify(repo => repo.DeleteBookCopy(1), Times.Once);
        }


        //GetCopyIdByBook
        [Fact]
        public void GetCopyIdByBook_WhenCopyExists_ReturnsCopyId()
        {
            var mockRepo = new Mock<IBooksCopiesRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockBooks = new Mock<IBooksService>();

            mockRepo.Setup(repo => repo.GetBookCopyId(1)).Returns(1);

            BooksCopiesService service = new(mockRepo.Object, mockMapper.Object, mockBooks.Object);

            var result = service.GetCopyIdByBookId(1);

            Assert.NotNull(result);
            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.GetBookCopyId(1), Times.Once);
        }

        [Fact]
        public void GetCopyIdByBook_WhenCopyDoesNotExist_ReturnsNull()
        {
            var mockRepo = new Mock<IBooksCopiesRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockBooks = new Mock<IBooksService>();

            mockRepo.Setup(repo => repo.GetBookCopyId(999)).Returns((int?)null);

            BooksCopiesService service = new(mockRepo.Object, mockMapper.Object, mockBooks.Object);

            var result = service.GetCopyIdByBookId(999);

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBookCopyId(999), Times.Once);
        }


        //GetAllCopies
        [Fact]
        public void GetAllCopies_WhenCalled_ReturnsListOfCopies()
        {
            var mockRepo = new Mock<IBooksCopiesRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockBooks = new Mock<IBooksService>();

            List<BookCopy> copies = new()
            {
                new BookCopy { CopyId = 1, BookId = 1, IsAvailable = true },
                new BookCopy { CopyId = 2, BookId = 1, IsAvailable = false },
                new BookCopy { CopyId = 3, BookId = 2, IsAvailable = true }
            };

            var dtoList = new List<ReadBookCopyDto>
            {
                new ReadBookCopyDto { CopyId = 1, IsAvailable = true },
                new ReadBookCopyDto { CopyId = 2, IsAvailable = false },
                new ReadBookCopyDto { CopyId = 3, IsAvailable = true }
            };

            mockRepo.Setup(repo => repo.GetBookCopies()).Returns(copies);
            mockMapper.Setup(m => m.Map<IEnumerable<ReadBookCopyDto>>(It.IsAny<IEnumerable<BookCopy>>())).Returns(dtoList);

            BooksCopiesService service = new(mockRepo.Object, mockMapper.Object, mockBooks.Object);

            var result = service.GetAllCopies();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.True(result.First().IsAvailable);
            mockRepo.Verify(repo => repo.GetBookCopies(), Times.Once);
        }

        [Fact]
        public void GetAllCopies_WhenNoCopiesExist_ReturnsNull()
        {
            var mockRepo = new Mock<IBooksCopiesRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockBooks = new Mock<IBooksService>();

            mockRepo.Setup(repo => repo.GetBookCopies()).Returns((IEnumerable<BookCopy>?)null);
            mockMapper.Setup(m => m.Map<IEnumerable<ReadBookCopyDto>>(It.IsAny<IEnumerable<BookCopy>>())).Returns((IEnumerable<ReadBookCopyDto>?)null);

            BooksCopiesService service = new(mockRepo.Object, mockMapper.Object, mockBooks.Object);

            var result = service.GetAllCopies();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBookCopies(), Times.Once);
        }
    }
}
