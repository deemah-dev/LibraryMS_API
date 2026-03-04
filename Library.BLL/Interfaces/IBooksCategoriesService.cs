using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IBooksCategoriesService
    {
        int AddCategory(BookCategory category);
        bool UpdateCategory(BookCategory category);
        bool RemoveCategory(int categoryId);
        BookCategory? GetCategory(int categoryId);
        IEnumerable<BookCategory>? GetAllCategories();
    }
}
