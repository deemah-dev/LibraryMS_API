using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class UsersService : IUsersService
    {
        private IUsersRepo usersRepo;

        public UsersService(IUsersRepo usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public int AddUser(User user)
        {
            return usersRepo.InsertUser(user);
        }

        public bool UpdateUser(User user)
        {
            return usersRepo.UpdateUser(user);
        }

        public bool RemoveUser(int userId)
        {
            return usersRepo.DeleteUser(userId);
        }

        public User? GetUser(int userId)
        {
            return usersRepo.GetUserById(userId);
        }

        public User? GetUserByUsername(string username)
        {
            return usersRepo.GetUserByUsername(username);
        }

        public IEnumerable<User>? GetAllUsers()
        {
            return usersRepo.GetUsers();
        }
    }
}
