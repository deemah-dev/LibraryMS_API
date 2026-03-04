using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface IAuthorsRepo
    {
        int InsertAuthor(Author author);
        bool UpdateAuthor(Author author);
        Author? GetAuthorByName(string name);
        IEnumerable<Author>? GetAuthors();
    }
}
