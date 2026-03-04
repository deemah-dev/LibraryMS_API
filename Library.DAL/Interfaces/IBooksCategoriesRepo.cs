using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IBooksCategoriesRepo
    {
        int InsertBooksCategory(BookCategory category);
        bool UpdateBooksCategory(BookCategory category);
        bool DeleteBooksCategory(int categoryId);
        BookCategory? GetBooksCategoryById(int categoryId);
        IEnumerable<BookCategory>? GetBooksCategories();
    }
}
