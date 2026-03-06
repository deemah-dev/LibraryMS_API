namespace Library.Core.Models
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }
        public string RefreshTokenHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? RevokedAt { get; set; }

        // Navigation
        public User? User { get; set; }
    }
}
