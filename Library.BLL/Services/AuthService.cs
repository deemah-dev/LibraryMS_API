using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUsersRepo usersRepo;

        public AuthService(IUsersRepo usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public User? Authenticate(string username)
        {
            return usersRepo.GetUserByUsername(username);
        }
    }
}
