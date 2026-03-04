using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IBooksRepo
    {
        int InsertBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(int bookId);
        Book? GetBookById(int bookId);
        IEnumerable<Book>? GetBooks();
    }
}
