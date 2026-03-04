using Library.Core.Models;
namespace Library.BLL.Interfaces
{
    public interface IAuthService
    {
        public User? Authenticate(string username);
    }
}
