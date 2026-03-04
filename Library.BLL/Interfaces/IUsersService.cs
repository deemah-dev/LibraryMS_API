using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IUsersService
    {
        int AddUser(User user);
        bool UpdateUser(User user);
        bool RemoveUser(int userId);
        User? GetUser(int userId);
        User? GetUserByUsername(string username);
        IEnumerable<User>? GetAllUsers();
    }
}
