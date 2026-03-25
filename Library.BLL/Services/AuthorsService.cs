using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepo authorsRepo;

        public AuthorsService(IAuthorsRepo authorsRepo)
        {
            this.authorsRepo = authorsRepo;
        }

        public int AddAuthor(Author author)
        {
            return authorsRepo.InsertAuthor(author);
        }

        public bool UpdateAuthor(Author author)
        {
            return authorsRepo.UpdateAuthor(author);
        }

        public Author? GetAuthorByName(string name)
        {
            return authorsRepo.GetAuthorByName(name);
        }

        public IEnumerable<Author>? GetAllAuthors()
        {
            return authorsRepo.GetAuthors();
        }

        public Author? GetAuthor(int authorId)
        {
            return authorsRepo.GetAuthorById(authorId);
        }
    }
}
