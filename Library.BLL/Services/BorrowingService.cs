using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class BorrowingService : IBorrowingService
    {
        private IBorrowingRepo borrowingRepo;

        public BorrowingService(IBorrowingRepo borrowingRepo)
        {
            this.borrowingRepo = borrowingRepo;
        }

        public int BorrowBook(BorrowingRecord borrowingRecord)
        {
            return borrowingRepo.InsertBorrowingRecord(borrowingRecord);
        }

        public bool ReturnBook(int borrowingRecordId, int userId, DateTime actualReturnDate)
        {
            return borrowingRepo.ReturnBook(borrowingRecordId, userId, actualReturnDate);
        }

        public decimal HasLateFine(int borrowingRecordId, DateTime actualReturnDate)
        {
            return borrowingRepo.CheckBorrowFine(borrowingRecordId, actualReturnDate);
        }

        public IEnumerable<BorrowingRecord>? GetAllBorrowingRecords()
        {
            return borrowingRepo.GetBorrowingRecords();
        }
    }
}
