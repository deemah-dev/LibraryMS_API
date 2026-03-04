using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class BooksCategoriesService : IBooksCategoriesService
    {
        private IBooksCategoriesRepo categoriesRepo;

        public BooksCategoriesService(IBooksCategoriesRepo categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo;
        }

        public int AddCategory(BookCategory category)
        {
            return categoriesRepo.InsertBooksCategory(category);
        }

        public bool UpdateCategory(BookCategory category)
        {
            return categoriesRepo.UpdateBooksCategory(category);
        }

        public bool RemoveCategory(int categoryId)
        {
            return categoriesRepo.DeleteBooksCategory(categoryId);
        }

        public BookCategory? GetCategory(int categoryId)
        {
            return categoriesRepo.GetBooksCategoryById(categoryId);
        }

        public IEnumerable<BookCategory>? GetAllCategories()
        {
            return categoriesRepo.GetBooksCategories();
        }
    }
}
