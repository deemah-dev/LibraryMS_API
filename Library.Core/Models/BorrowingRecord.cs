namespace Library.Core.Models
{
    public class BorrowingRecord
    {
        public int BorrowingRecordId { get; set; }
        public int UserId { get; set; }
        public int CopyId { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }

        // Navigation
        public User? User { get; set; }
        public BookCopy? Copy { get; set; }
    }
}
