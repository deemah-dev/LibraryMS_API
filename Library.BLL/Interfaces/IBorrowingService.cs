using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IBorrowingService
    {
        int BorrowBook(BorrowingRecord borrowingRecord);
        bool ReturnBook(int borrowingRecordId, int userId, DateTime actualReturnDate);
        bool HasLateFine(int borrowingRecordId, DateTime actualReturnDate);
        IEnumerable<BorrowingRecord>? GetAllBorrowingRecords();
    }
}
