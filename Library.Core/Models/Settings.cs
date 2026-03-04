namespace Library.Core.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public int DefaultBorrowDays { get; set; }
        public decimal DefaultFinePerDay { get; set; }
    }
}
