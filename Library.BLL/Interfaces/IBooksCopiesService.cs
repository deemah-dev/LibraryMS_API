using Library.Core.Dtos.BookCopyDtos;
using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IBooksCopiesService
    {
        int AddCopy(AddBookCopyDto copy);
        bool RemoveCopy(int copyId);
        int? GetCopyIdByBookId(int bookId);
        IEnumerable<ReadBookCopyDto>? GetAllCopies();
        ReadBookCopyDto? GetCopy(int copyId);
    }
}
