namespace Library.API.DTOs.Auth
{
    public class RefreshRequest
    {
        public string RefreshToken { get; set; }
        public string Username { get; set; }
    }
}
