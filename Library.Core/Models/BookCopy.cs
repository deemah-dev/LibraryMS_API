namespace Library.Core.Models
{
    public class BookCopy
    {
        public int CopyId { get; set; }
        public int BookId { get; set; }
        public bool IsAvailable { get; set; }

        // Navigation
        public Book? Book { get; set; }
    }
}
