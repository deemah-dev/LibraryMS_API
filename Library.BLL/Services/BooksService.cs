using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class BooksService : IBooksService
    {
        private IBooksRepo booksRepo;

        public BooksService(IBooksRepo booksRepo)
        {
            this.booksRepo = booksRepo;
        }

        public int AddBook(Book book)
        {
            return booksRepo.InsertBook(book);
        }

        public bool UpdateBook(Book book)
        {
            return booksRepo.UpdateBook(book);
        }

        public bool RemoveBook(int bookId)
        {
            return booksRepo.DeleteBook(bookId);
        }

        public Book? GetBook(int bookId)
        {
            return booksRepo.GetBookById(bookId);
        }

        public IEnumerable<Book>? GetAllBooks()
        {
            return booksRepo.GetBooks();
        }
    }
}
