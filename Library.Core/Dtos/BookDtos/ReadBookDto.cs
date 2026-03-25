using Library.Core.Models;

namespace Library.Core.Dtos.BookDtos
{
    public class ReadBookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? SubTitle { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public DateTime? PublicationDate { get; set; }
        public string? AdditionalDetails { get; set; }
        public BookCategory? Category { get; set; }
        public Author? Author { get; set; }
    }
}
