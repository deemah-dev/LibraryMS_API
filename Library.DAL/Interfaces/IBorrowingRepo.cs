using Library.Core.Dtos.BorrowingBookDtos;
using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IBorrowingRepo
    {
        int InsertBorrowingRecord(BorrowingRecord borrowingRecord);
        bool ReturnBook(BorrowingRecord borrowingRecord);
        decimal CheckBorrowFine(int borrowingRecordId, DateTime actualReturnDate);
        IEnumerable<BorrowingRecord>? GetBorrowingRecords();
        BorrowingRecord? GetBorrowingRecord(int borrowingRecordId);
    }
}
