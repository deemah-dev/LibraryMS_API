using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface IAuthorsService
    {
        int AddAuthor(Author author);
        bool UpdateAuthor(Author author);
        Author? GetAuthorByName(string name);
        IEnumerable<Author>? GetAllAuthors();
        Author? GetAuthor(int authorId);
    }
}
