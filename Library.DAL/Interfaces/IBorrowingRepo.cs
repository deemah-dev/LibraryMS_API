using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IBorrowingRepo
    {
        int InsertBorrowingRecord(BorrowingRecord borrowingRecord);
        bool ReturnBook(int borrowingRecordId, int userId, DateTime actualReturnDate);
        bool CheckBorrowFine(int borrowingRecordId, DateTime actualReturnDate);
        IEnumerable<BorrowingRecord>? GetBorrowingRecords();
    }
}
