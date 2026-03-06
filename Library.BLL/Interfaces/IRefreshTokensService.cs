using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IRefreshTokensService
    {
        RefreshToken? GetRefreshToken(string refreshTokenHash);
        int AddRefreshToken(RefreshToken refreshToken);
        bool RevokeRefreshToken(string refreshTokenHash, DateTime revokedAt);
    }
}
