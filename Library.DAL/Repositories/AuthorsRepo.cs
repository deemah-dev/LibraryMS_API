using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class AuthorsRepo : IAuthorsRepo
    {
        public int InsertAuthor(Author author)
        {
            string storedProcedureName = "SP_InsertAuthor";
            SqlParameter nameParameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = author.Name };

            return CommonRepos.ReturnValue(storedProcedureName, nameParameter);
        }

        public bool UpdateAuthor(Author author)
        {
            string storedProcedureName = "SP_UpdateAuthor";
            SqlParameter authorIdParameter = new SqlParameter("@AuthorId", SqlDbType.Int) { Value = author.AuthorId };
            SqlParameter nameParameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = author.Name };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, authorIdParameter, nameParameter);
        }

        public Author? GetAuthorByName(string name)
        {
            string storedProcedureName = "SP_GetAuthorByName";
            SqlParameter nameParameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };

            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName, nameParameter);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new Author
                {
                    AuthorId = (int)row["AuthorId"],
                    Name = (string)row["Name"]
                };
            }
            return null;
        }

        public IEnumerable<Author>? GetAuthors()
        {
            string storedProcedureName = "SP_GetAuthors";
            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<Author> authors = new List<Author>();
                foreach (DataRow row in dataTable.Rows)
                {
                    authors.Add(new Author
                    {
                        AuthorId = (int)row["AuthorId"],
                        Name = (string)row["Name"]
                    });
                }
                return authors;
            }
            return null;
        }
    }
}
