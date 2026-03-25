using AutoMapper;
using Library.BLL.Interfaces;
using Library.Core.Dtos.BookCopyDtos;
using Library.Core.Dtos.BookDtos;
using Library.Core.Models;
using Library.DAL.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.BLL.Services
{
    public class BooksCopiesService : IBooksCopiesService
    {
        private IBooksCopiesRepo copiesRepo;
        private IMapper mapper;
        private IBooksService booksService;

        public BooksCopiesService(IBooksCopiesRepo copiesRepo, IMapper mapper, IBooksService booksService)
        {
            this.copiesRepo = copiesRepo;
            this.mapper = mapper;
            this.booksService = booksService;
        }

        public int AddCopy(AddBookCopyDto copy)
        {
            BookCopy bookCopyEntity = mapper.Map<BookCopy>(copy);
            return copiesRepo.InsertBookCopy(bookCopyEntity);
        }

        public bool RemoveCopy(int copyId)
        {
            return copiesRepo.DeleteBookCopy(copyId);
        }

        public int? GetCopyIdByBookId(int bookId)
        {
            return copiesRepo.GetBookCopyId(bookId);
        }

        public IEnumerable<ReadBookCopyDto>? GetAllCopies()
        {
            IEnumerable<BookCopy>? bookCopies = copiesRepo.GetBookCopies();
            if (bookCopies is null)
                return null;
            return mapper.Map<IEnumerable<ReadBookCopyDto>>(bookCopies);
        }

        public ReadBookCopyDto? GetCopy(int copyId)
        {
            BookCopy? bookCopyEntity = copiesRepo.GetCopyById(copyId);
            if (bookCopyEntity is null)
                return null;

            ReadBookCopyDto bookCopy = mapper.Map<ReadBookCopyDto>(bookCopyEntity);
            bookCopy.Book = booksService.GetBook(bookCopyEntity.BookId);
            return bookCopy;
        }
    }
}
