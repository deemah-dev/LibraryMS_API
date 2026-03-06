using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class RefreshTokensService : IRefreshTokensService
    {
        private IRefreshTokensRepo refreshTokensRepo;
        private IUsersService usersService;

        public RefreshTokensService(IRefreshTokensRepo refreshTokensRepo, IUsersService usersService)
        {
            this.refreshTokensRepo = refreshTokensRepo;
            this.usersService = usersService;
        }

        public RefreshToken? GetRefreshToken(string refreshTokenHash)
        {
            RefreshToken? refreshToken = refreshTokensRepo.GetRefreshToken(refreshTokenHash);
            if(refreshToken is not null)
                refreshToken.User = usersService.GetUser(refreshToken.UserId);
            return refreshToken;
        }

        public int AddRefreshToken(RefreshToken refreshToken)
        {
            return refreshTokensRepo.InsertRefreshToken(refreshToken);
        }

        public bool RevokeRefreshToken(string refreshTokenHash, DateTime revokedAt)
        {
            return refreshTokensRepo.RevokeRefreshToken(refreshTokenHash, revokedAt);
        }
    }
}
