using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class BooksRepo : IBooksRepo
    {
        public int InsertBook(Book book)
        {
            string storedProcedureName = "SP_InsertBook";
            SqlParameter titleParameter = new SqlParameter("@Title", SqlDbType.NVarChar) { Value = book.Title };
            SqlParameter subTitleParameter = new SqlParameter("@SubTitle", SqlDbType.NVarChar) { Value = book.SubTitle ?? (object)DBNull.Value };
            SqlParameter authorIdParameter = new SqlParameter("@AuthorId", SqlDbType.Int) { Value = book.AuthorId };
            SqlParameter isbnParameter = new SqlParameter("@ISBN", SqlDbType.NVarChar) { Value = book.ISBN ?? (object)DBNull.Value };
            SqlParameter categoryIdParameter = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = book.CategoryId };
            SqlParameter publicationDateParameter = new SqlParameter("@PublicationDate", SqlDbType.Date) { Value = book.PublicationDate ?? (object)DBNull.Value };
            SqlParameter additionalDetailsParameter = new SqlParameter("@AdditionalDetails", SqlDbType.NVarChar) { Value = book.AdditionalDetails ?? (object)DBNull.Value };

            return CommonRepos.ReturnValue(storedProcedureName, titleParameter, subTitleParameter, authorIdParameter, isbnParameter, categoryIdParameter, publicationDateParameter, additionalDetailsParameter);
        }

        public bool UpdateBook(Book book)
        {
            string storedProcedureName = "SP_UpdateBook";
            SqlParameter bookIdParameter = new SqlParameter("@BookId", SqlDbType.Int) { Value = book.BookId };
            SqlParameter titleParameter = new SqlParameter("@Title", SqlDbType.NVarChar) { Value = book.Title };
            SqlParameter subTitleParameter = new SqlParameter("@SubTitle", SqlDbType.NVarChar) { Value = book.SubTitle ?? (object)DBNull.Value };
            SqlParameter authorIdParameter = new SqlParameter("@AuthorId", SqlDbType.Int) { Value = book.AuthorId };
            SqlParameter isbnParameter = new SqlParameter("@ISBN", SqlDbType.NVarChar) { Value = book.ISBN ?? (object)DBNull.Value };
            SqlParameter categoryIdParameter = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = book.CategoryId };
            SqlParameter publicationDateParameter = new SqlParameter("@PublicationDate", SqlDbType.Date) { Value = book.PublicationDate ?? (object)DBNull.Value };
            SqlParameter additionalDetailsParameter = new SqlParameter("@AdditionalDetails", SqlDbType.NVarChar) { Value = book.AdditionalDetails ?? (object)DBNull.Value };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, bookIdParameter, titleParameter, subTitleParameter, authorIdParameter, isbnParameter, categoryIdParameter, publicationDateParameter, additionalDetailsParameter);
        }

        public bool DeleteBook(int bookId)
        {
            string storedProcedureName = "SP_DeleteBook";
            SqlParameter bookIdParameter = new SqlParameter("@BookId", SqlDbType.Int) { Value = bookId };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, bookIdParameter);
        }

        public Book? GetBookById(int bookId)
        {
            string storedProcedureName = "SP_GetBookById";
            SqlParameter bookIdParameter = new SqlParameter("@BookId", SqlDbType.Int) { Value = bookId };

            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName, bookIdParameter);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new Book
                {
                    BookId = (int)row["BookId"],
                    Title = (string)row["Title"],
                    SubTitle = row["SubTitle"] != DBNull.Value ? (string)row["SubTitle"] : null,
                    AuthorId = (int)row["AuthorId"],
                    ISBN = row["ISBN"] != DBNull.Value ? (string)row["ISBN"] : null,
                    CategoryId = (int)row["CategoryId"],
                    PublicationDate = row["PublicationDate"] != DBNull.Value ? (DateTime)row["PublicationDate"] : null,
                    AdditionalDetails = row["AdditionalDetails"] != DBNull.Value ? (string)row["AdditionalDetails"] : null
                };
            }
            return null;
        }

        public IEnumerable<Book>? GetBooks()
        {
            string storedProcedureName = "SP_GetBooks";
            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<Book> books = new List<Book>();
                foreach (DataRow row in dataTable.Rows)
                {
                    books.Add(new Book
                    {
                        BookId = (int)row["BookId"],
                        Title = (string)row["Title"],
                        SubTitle = row["SubTitle"] != DBNull.Value ? (string)row["SubTitle"] : null,
                        AuthorId = (int)row["AuthorId"],
                        ISBN = row["ISBN"] != DBNull.Value ? (string)row["ISBN"] : null,
                        CategoryId = (int)row["CategoryId"],
                        PublicationDate = row["PublicationDate"] != DBNull.Value ? (DateTime)row["PublicationDate"] : null,
                        AdditionalDetails = row["AdditionalDetails"] != DBNull.Value ? (string)row["AdditionalDetails"] : null
                    });
                }
                return books;
            }
            return null;
        }
    }
}
