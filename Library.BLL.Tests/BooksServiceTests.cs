using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class BooksServiceTests
    {
        //AddBook
        [Fact]
        public void AddBook_WhenCalled_ReturnsBookId()
        {
            Mock<IBooksRepo> mockRepo = new();

            Book book = new Book
            {
                Title = "The Great Gatsby",
                SubTitle = "A Classic Novel",
                AuthorId = 1,
                ISBN = "978-0743273565",
                CategoryId = 1,
                PublicationDate = new DateTime(1925, 4, 10),
                AdditionalDetails = "Fiction"
            };

            mockRepo.Setup(repo => repo.InsertBook(book)).Returns(1);

            BooksService service = new(mockRepo.Object);

            var result = service.AddBook(book);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertBook(book), Times.Once);
        }


        //UpdateBook
        [Fact]
        public void UpdateBook_WhenCalled_ReturnsTrue()
        {
            Mock<IBooksRepo> mockRepo = new();

            Book book = new Book
            {
                BookId = 1,
                Title = "The Great Gatsby - Updated",
                AuthorId = 1,
                CategoryId = 1
            };

            mockRepo.Setup(repo => repo.UpdateBook(book)).Returns(true);

            BooksService service = new(mockRepo.Object);

            var result = service.UpdateBook(book);

            Assert.True(result);
            mockRepo.Verify(repo => repo.UpdateBook(book), Times.Once);
        }


        //RemoveBook
        [Fact]
        public void RemoveBook_WhenCalled_ReturnsTrue()
        {
            Mock<IBooksRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.DeleteBook(1)).Returns(true);

            BooksService service = new(mockRepo.Object);

            var result = service.RemoveBook(1);

            Assert.True(result);
            mockRepo.Verify(repo => repo.DeleteBook(1), Times.Once);
        }


        //GetBook
        [Fact]
        public void GetBook_WhenBookExists_ReturnsBook()
        {
            Mock<IBooksRepo> mockRepo = new();

            Book book = new Book
            {
                BookId = 1,
                Title = "The Great Gatsby",
                AuthorId = 1,
                CategoryId = 1
            };

            mockRepo.Setup(repo => repo.GetBookById(1)).Returns(book);

            BooksService service = new(mockRepo.Object);

            var result = service.GetBook(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.BookId);
            Assert.Equal("The Great Gatsby", result.Title);
            mockRepo.Verify(repo => repo.GetBookById(1), Times.Once);
        }

        [Fact]
        public void GetBook_WhenBookDoesNotExist_ReturnsNull()
        {
            Mock<IBooksRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBookById(999)).Returns((Book?)null);

            BooksService service = new(mockRepo.Object);

            var result = service.GetBook(999);

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBookById(999), Times.Once);
        }


        //GetAllBooks
        [Fact]
        public void GetAllBooks_WhenCalled_ReturnsListOfBooks()
        {
            Mock<IBooksRepo> mockRepo = new();

            List<Book> books = new()
            {
                new Book { BookId = 1, Title = "The Great Gatsby", AuthorId = 1, CategoryId = 1 },
                new Book { BookId = 2, Title = "1984", AuthorId = 2, CategoryId = 1 },
                new Book { BookId = 3, Title = "To Kill a Mockingbird", AuthorId = 3, CategoryId = 1 }
            };

            mockRepo.Setup(repo => repo.GetBooks()).Returns(books);

            BooksService service = new(mockRepo.Object);

            var result = service.GetAllBooks();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal("The Great Gatsby", result.First().Title);
            mockRepo.Verify(repo => repo.GetBooks(), Times.Once);
        }

        [Fact]
        public void GetAllBooks_WhenNoBooksExist_ReturnsNull()
        {
            Mock<IBooksRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBooks()).Returns((IEnumerable<Book>?)null);

            BooksService service = new(mockRepo.Object);

            var result = service.GetAllBooks();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBooks(), Times.Once);
        }
    }
}
