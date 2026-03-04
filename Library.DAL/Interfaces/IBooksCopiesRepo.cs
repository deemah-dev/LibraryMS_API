using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IBooksCopiesRepo
    {
        int InsertBookCopy(BookCopy copy);
        bool DeleteBookCopy(int copyId);
        int? GetBookCopyId(int bookId);
        IEnumerable<BookCopy>? GetBookCopies();
    }
}
