namespace Library.Core.Models
{
    public class Fine
    {
        public int FineId { get; set; }
        public int UserId { get; set; }
        public int BorrowingRecordId { get; set; }
        public int NumberOfLateDays { get; set; }
        public decimal FineAmount { get; set; }

        // Navigation
        public User? User { get; set; }
        public BorrowingRecord? BorrowingRecord { get; set; }
    }
}
