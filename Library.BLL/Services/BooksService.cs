using AutoMapper;
using Library.BLL.Interfaces;
using Library.Core.Dtos.BookDtos;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class BooksService : IBooksService
    {
        private IBooksRepo booksRepo;
        private IAuthorsService authorsService;
        private IBooksCategoriesService booksCategoriesService;
        private IMapper mapper;

        public BooksService(IBooksRepo booksRepo, IMapper mapper, IAuthorsService authorsService, IBooksCategoriesService booksCategoriesService)
        {
            this.booksRepo = booksRepo;
            this.mapper = mapper;
            this.authorsService = authorsService;
            this.booksCategoriesService = booksCategoriesService;
        }

        public int AddBook(AddBookDTO book)
        {
            Book bookEntity = mapper.Map<Book>(book);
            return booksRepo.InsertBook(bookEntity);
        }

        public bool UpdateBook(int bookId, UpdateBookDto book)
        {
            Book bookEntity = mapper.Map<Book>(book);
            bookEntity.BookId = bookId;
            return booksRepo.UpdateBook(bookEntity);
        }

        public bool RemoveBook(int bookId)
        {
            return booksRepo.DeleteBook(bookId);
        }

        public ReadBookDto? GetBook(int bookId)
        {
            Book? book = booksRepo.GetBookById(bookId);

            if (book is null)
                return null;

            ReadBookDto bookDto = mapper.Map<ReadBookDto>(book);
            bookDto.Author = authorsService.GetAuthor(book.AuthorId);
            bookDto.Category = booksCategoriesService.GetCategory(book.CategoryId);
            return bookDto;
        }

        public IEnumerable<ReadBookDto>? GetAllBooks()
        {
            IEnumerable<Book>? books = booksRepo.GetBooks();

            if (books is null)
                return null;

            return mapper.Map<IEnumerable<ReadBookDto>>(books);
        }
    }
}
