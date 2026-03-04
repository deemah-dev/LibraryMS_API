using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IUsersRepo
    {
        int InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        User? GetUserById(int userId);
        User? GetUserByUsername(string username);
        IEnumerable<User>? GetUsers();
    }
}
