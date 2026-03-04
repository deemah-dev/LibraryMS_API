using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class BooksCopiesService : IBooksCopiesService
    {
        private IBooksCopiesRepo copiesRepo;

        public BooksCopiesService(IBooksCopiesRepo copiesRepo)
        {
            this.copiesRepo = copiesRepo;
        }

        public int AddCopy(BookCopy copy)
        {
            return copiesRepo.InsertBookCopy(copy);
        }

        public bool RemoveCopy(int copyId)
        {
            return copiesRepo.DeleteBookCopy(copyId);
        }

        public int? GetCopyIdByBook(int bookId)
        {
            return copiesRepo.GetBookCopyId(bookId);
        }

        public IEnumerable<BookCopy>? GetAllCopies()
        {
            return copiesRepo.GetBookCopies();
        }
    }
}
