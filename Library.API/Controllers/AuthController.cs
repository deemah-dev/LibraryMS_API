using Library.API.DTOs.Auth;
using Library.BLL.Interfaces;
using Library.BLL.Services;
using Library.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Library.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;
        private IConfiguration configuration;
        private IRefreshTokensService refreshTokensService;

        public AuthController(IAuthService authService, IConfiguration configuration, IRefreshTokensService refreshTokensService)
        {
            this.authService = authService;
            this.configuration = configuration;
            this.refreshTokensService = refreshTokensService;
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private static string SHA256Hash(string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            byte[] hashBytes = SHA256.HashData(textBytes);

            return Convert.ToBase64String(hashBytes);
        }

        private IActionResult GenerateAccessToken(User user)
        {
            Claim[] claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            ];

            string? secretKey = configuration["JWT_SECRET_KEY"];

            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("JWT secret key is not configured.");

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secretKey));

            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(

                issuer: "Library.API",
                audience: "LibraryUsers",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler tokenHandler = new();
            string accessTokenString = tokenHandler.WriteToken(token);

            string refreshTokenString = GenerateRefreshToken();

            RefreshToken refreshToken = new()
            {
                UserId = user.UserId,
                RefreshTokenHash = SHA256Hash(refreshTokenString),
                CreatedAt = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(7),
                RevokedAt = null
            };
            refreshTokensService.AddRefreshToken(refreshToken);

            TokenResponse tokenResponse = new() { AccessToken = accessTokenString, RefreshToken = refreshTokenString };

            return Ok(tokenResponse);
        }

        //________________________________________________________________

        [HttpPost("Login")]
        [EnableRateLimiting("AuthLimiter")]
        public IActionResult Login(LoginRequest request)
        {
            User? user = authService.Authenticate(request.Username);

            if (user is null)
                return Unauthorized();

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isValidPassword)
                return Unauthorized();

            return GenerateAccessToken(user);
        }

        //________________________________________________________________

        [HttpPost("RefreshTokens")]
        [EnableRateLimiting("AuthLimiter")]
        public IActionResult RefreshTokens(RefreshRequest request)
        {
            string refreshTokenHash = SHA256Hash(request.RefreshToken);
            RefreshToken? refreshToken = refreshTokensService.GetRefreshToken(refreshTokenHash);
            if (refreshToken is null)
                return Unauthorized("Invalid refresh request");

            if (refreshToken.User is null)
                return Unauthorized("Invalid refresh request");

            if (DateTime.Compare(refreshToken.ExpirationDate, DateTime.UtcNow) < 0)
                return Unauthorized("Refresh token expired");

            if (refreshToken.RevokedAt is not null)
                return Unauthorized("Refresh token is revoked");

            refreshTokensService.RevokeRefreshToken(refreshToken.RefreshTokenHash, DateTime.UtcNow);

            return GenerateAccessToken(refreshToken.User);
        }

        [HttpPost("Logout")]
        public IActionResult Logout(LogoutRequest request)
        {
            string refreshTokenHash = SHA256Hash(request.RefreshToken);
            RefreshToken? refreshToken = refreshTokensService.GetRefreshToken(refreshTokenHash);
            if (refreshToken is null
                || refreshToken.RevokedAt is not null
                || DateTime.Compare(refreshToken.ExpirationDate, DateTime.UtcNow) < 0
                || refreshToken.User is null)
                return Ok();

            refreshTokensService.RevokeRefreshToken(refreshToken.RefreshTokenHash, DateTime.UtcNow);
            return Ok("Logged out successfully");
        }
    }
}
