namespace Library.Core.Dtos.BookDtos
{
    public class UpdateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string? SubTitle { get; set; }
        public int AuthorId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? AdditionalDetails { get; set; }
    }
}