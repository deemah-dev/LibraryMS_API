namespace Library.API.DTOs.Auth
{
    public class LogoutRequest
    {
        public string Username { get; set; }
        public string RefreshToken { get; set; }
    }
}
