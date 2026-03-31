namespace Library.Core.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? SubTitle { get; set; }
        public int AuthorId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string? AdditionalDetails { get; set; }
    }
}
