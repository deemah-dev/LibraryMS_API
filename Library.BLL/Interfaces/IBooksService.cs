using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IBooksService
    {
        int AddBook(Book book);
        bool UpdateBook(Book book);
        bool RemoveBook(int bookId);
        Book? GetBook(int bookId);
        IEnumerable<Book>? GetAllBooks();
    }
}
