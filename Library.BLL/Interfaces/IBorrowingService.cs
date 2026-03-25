using Library.Core.Dtos.BorrowingBookDtos;
using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IBorrowingService
    {
        int BorrowBook(BorrowBookDto borrowingRecord);
        bool ReturnBook(ReturnBookDto returnBook);
        decimal HasLateFine(int borrowingRecordId, DateTime actualReturnDate);
        ReadBorrowingRecordDto? GetBorrowingRecord(int borrowingRecordId);
        IEnumerable<ReadBorrowingRecordDto>? GetAllBorrowingRecords();
    }
}
