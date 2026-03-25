namespace Library.Core.Dtos.BorrowingBookDtos
{
    public class BorrowBookDto
    {
        public int UserId { get; set; }
        public int CopyId { get; set; }
        public DateTime BorrowingDate { get; set; }
    }
}
