using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IBooksCopiesService
    {
        int AddCopy(BookCopy copy);
        bool RemoveCopy(int copyId);
        int? GetCopyIdByBook(int bookId);
        IEnumerable<BookCopy>? GetAllCopies();
    }
}
