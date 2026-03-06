using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IRefreshTokensRepo
    {
        RefreshToken? GetRefreshToken(string refreshTokenHash);
        int InsertRefreshToken(RefreshToken refreshToken);
        bool RevokeRefreshToken(string refreshTokenHash, DateTime revokedAt);
    }
}
