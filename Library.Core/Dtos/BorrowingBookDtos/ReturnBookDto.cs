using Library.Core.Models;

namespace Library.Core.Dtos.BorrowingBookDtos
{
    public class ReturnBookDto
    {
        public int BorrowingRecordId { get; set; }
        public int UserId { get; set; }
        public DateTime ActualReturnDate { get; set; }
    }
}
