using Library.Core.Dtos.BookDtos;
using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IBooksService
    {
        int AddBook(AddBookDTO book);
        bool UpdateBook(int bookId, UpdateBookDto book);
        bool RemoveBook(int bookId);
        ReadBookDto? GetBook(int bookId);
        IEnumerable<ReadBookDto>? GetAllBooks();
    }
}
