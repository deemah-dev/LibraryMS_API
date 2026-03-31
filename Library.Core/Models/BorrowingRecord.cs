namespace Library.Core.Models
{
    public class BorrowingRecord
    {
        public int BorrowingRecordId { get; set; }
        public int BorrowUserId { get; set; }
        public int CopyId { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public int? ReturnUserId { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string? Status { get; set; }
    }
}
