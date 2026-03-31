using Library.Core.Dtos.BookCopyDtos;
using Library.Core.Models;

namespace Library.Core.Dtos.BorrowingBookDtos
{
    public class ReadBorrowingRecordDto
    {
        public int BorrowingRecordId { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string? Status { get; set; }

        public User? BorrowUser { get; set; }
        public User? ReturnUser { get; set; }
        public ReadBookCopyDto? Copy { get; set; }
    }
}