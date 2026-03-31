namespace Library.Core.Models
{
    public class Fine
    {
        public int FineId { get; set; }
        public int UserId { get; set; }
        public int BorrowingRecordId { get; set; }
        public int NumberOfLateDays { get; set; }
        public decimal FineAmount { get; set; }
    }
}
