using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IFinesService
    {
        IEnumerable<Fine>? GetAllFines();
    }
}
