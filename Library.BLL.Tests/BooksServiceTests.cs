using Library.BLL.Services;
using Library.Core.Dtos.BookDtos;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;
using AutoMapper;
using Library.BLL.Interfaces;

namespace Library.BLL.Tests
{
    public class BooksServiceTests
    {
        //AddBook
        [Fact]
        public void AddBook_WhenCalled_ReturnsBookId()
        {
            var mockRepo = new Mock<IBooksRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthors = new Mock<IAuthorsService>();
            var mockCategories = new Mock<IBooksCategoriesService>();

            AddBookDTO addDto = new AddBookDTO
            {
                Title = "The Great Gatsby",
                SubTitle = "A Classic Novel",
                AuthorId = 1,
                ISBN = "978-0743273565",
                CategoryId = 1,
                PublicationDate = new DateTime(1925, 4, 10),
                AdditionalDetails = "Fiction"
            };

            Book mapped = new Book
            {
                Title = addDto.Title,
                SubTitle = addDto.SubTitle,
                AuthorId = addDto.AuthorId,
                ISBN = addDto.ISBN,
                CategoryId = addDto.CategoryId,
                PublicationDate = addDto.PublicationDate,
                AdditionalDetails = addDto.AdditionalDetails
            };

            mockMapper.Setup(m => m.Map<Book>(It.IsAny<AddBookDTO>())).Returns(mapped);
            mockRepo.Setup(r => r.InsertBook(mapped)).Returns(1);

            BooksService service = new(mockRepo.Object, mockMapper.Object, mockAuthors.Object, mockCategories.Object);

            var result = service.AddBook(addDto);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertBook(mapped), Times.Once);
        }


        //UpdateBook
        [Fact]
        public void UpdateBook_WhenCalled_ReturnsTrue()
        {
            var mockRepo = new Mock<IBooksRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthors = new Mock<IAuthorsService>();
            var mockCategories = new Mock<IBooksCategoriesService>();

            UpdateBookDto updateDto = new UpdateBookDto
            {
                Title = "The Great Gatsby - Updated",
                AuthorId = 1,
                CategoryId = 1
            };

            Book mapped = new Book
            {
                Title = updateDto.Title,
                AuthorId = updateDto.AuthorId,
                CategoryId = updateDto.CategoryId
            };

            mockMapper.Setup(m => m.Map<Book>(It.IsAny<UpdateBookDto>())).Returns(mapped);
            mockRepo.Setup(r => r.UpdateBook(It.IsAny<Book>())).Returns(true);

            BooksService service = new(mockRepo.Object, mockMapper.Object, mockAuthors.Object, mockCategories.Object);

            var result = service.UpdateBook(1, updateDto);

            Assert.True(result);
            mockRepo.Verify(repo => repo.UpdateBook(It.Is<Book>(b => b.BookId == 1 && b.Title == updateDto.Title)), Times.Once);
        }


        //RemoveBook
        [Fact]
        public void RemoveBook_WhenCalled_ReturnsTrue()
        {
            var mockRepo = new Mock<IBooksRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthors = new Mock<IAuthorsService>();
            var mockCategories = new Mock<IBooksCategoriesService>();

            mockRepo.Setup(repo => repo.DeleteBook(1)).Returns(true);

            BooksService service = new(mockRepo.Object, mockMapper.Object, mockAuthors.Object, mockCategories.Object);

            var result = service.RemoveBook(1);

            Assert.True(result);
            mockRepo.Verify(repo => repo.DeleteBook(1), Times.Once);
        }


        //GetBook
        [Fact]
        public void GetBook_WhenBookExists_ReturnsBookDto()
        {
            var mockRepo = new Mock<IBooksRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthors = new Mock<IAuthorsService>();
            var mockCategories = new Mock<IBooksCategoriesService>();

            Book book = new Book
            {
                BookId = 1,
                Title = "The Great Gatsby",
                AuthorId = 1,
                CategoryId = 1
            };

            ReadBookDto readDto = new ReadBookDto { BookId = 1, Title = "The Great Gatsby" };

            mockRepo.Setup(r => r.GetBookById(1)).Returns(book);
            mockMapper.Setup(m => m.Map<ReadBookDto>(book)).Returns(readDto);
            mockAuthors.Setup(a => a.GetAuthor(book.AuthorId)).Returns(new Author { AuthorId = 1, Name = "Author" });
            mockCategories.Setup(c => c.GetCategory(book.CategoryId)).Returns(new BookCategory { CategoryId = 1, Name = "Category" });

            BooksService service = new(mockRepo.Object, mockMapper.Object, mockAuthors.Object, mockCategories.Object);

            var result = service.GetBook(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.BookId);
            Assert.Equal("The Great Gatsby", result.Title);
            mockRepo.Verify(repo => repo.GetBookById(1), Times.Once);
        }

        [Fact]
        public void GetBook_WhenBookDoesNotExist_ReturnsNull()
        {
            var mockRepo = new Mock<IBooksRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthors = new Mock<IAuthorsService>();
            var mockCategories = new Mock<IBooksCategoriesService>();

            mockRepo.Setup(repo => repo.GetBookById(999)).Returns((Book?)null);

            BooksService service = new(mockRepo.Object, mockMapper.Object, mockAuthors.Object, mockCategories.Object);

            var result = service.GetBook(999);

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBookById(999), Times.Once);
        }


        //GetAllBooks
        [Fact]
        public void GetAllBooks_WhenCalled_ReturnsListOfBooks()
        {
            var mockRepo = new Mock<IBooksRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthors = new Mock<IAuthorsService>();
            var mockCategories = new Mock<IBooksCategoriesService>();

            List<Book> books = new()
            {
                new Book { BookId = 1, Title = "The Great Gatsby", AuthorId = 1, CategoryId = 1 },
                new Book { BookId = 2, Title = "1984", AuthorId = 2, CategoryId = 1 },
                new Book { BookId = 3, Title = "To Kill a Mockingbird", AuthorId = 3, CategoryId = 1 }
            };

            var dtoList = new List<ReadBookDto>
            {
                new ReadBookDto { BookId = 1, Title = "The Great Gatsby" },
                new ReadBookDto { BookId = 2, Title = "1984" },
                new ReadBookDto { BookId = 3, Title = "To Kill a Mockingbird" }
            };

            mockRepo.Setup(repo => repo.GetBooks()).Returns(books);
            mockMapper.Setup(m => m.Map<IEnumerable<ReadBookDto>>(It.IsAny<IEnumerable<Book>>())).Returns(dtoList);

            BooksService service = new(mockRepo.Object, mockMapper.Object, mockAuthors.Object, mockCategories.Object);

            var result = service.GetAllBooks();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal("The Great Gatsby", result.First().Title);
            mockRepo.Verify(repo => repo.GetBooks(), Times.Once);
        }

        [Fact]
        public void GetAllBooks_WhenNoBooksExist_ReturnsNull()
        {
            var mockRepo = new Mock<IBooksRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthors = new Mock<IAuthorsService>();
            var mockCategories = new Mock<IBooksCategoriesService>();

            mockRepo.Setup(repo => repo.GetBooks()).Returns((IEnumerable<Book>?)null);
            mockMapper.Setup(m => m.Map<IEnumerable<ReadBookDto>>(It.IsAny<IEnumerable<Book>>())).Returns((IEnumerable<ReadBookDto>?)null);

            BooksService service = new(mockRepo.Object, mockMapper.Object, mockAuthors.Object, mockCategories.Object);

            var result = service.GetAllBooks();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBooks(), Times.Once);
        }
    }
}
