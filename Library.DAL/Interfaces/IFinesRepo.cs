using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IFinesRepo
    {
        IEnumerable<Fine>? GetFines();
    }
}
