using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class BooksCopiesRepo : IBooksCopiesRepo
    {
        public int InsertBookCopy(BookCopy copy)
        {
            string storedProcedureName = "SP_InsertBookCopy";
            SqlParameter bookIdParameter = new SqlParameter("@BookId", SqlDbType.Int) { Value = copy.BookId };

            return CommonRepos.ReturnValue(storedProcedureName, bookIdParameter);
        }

        public bool DeleteBookCopy(int copyId)
        {
            string storedProcedureName = "SP_DeleteBookCopy";
            SqlParameter copyIdParameter = new SqlParameter("@CopyId", SqlDbType.Int) { Value = copyId };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, copyIdParameter);
        }

        public int? GetBookCopyId(int bookId)
        {
            string storedProcedureName = "SP_GetBookCopyId";
            SqlParameter bookIdParameter = new SqlParameter("@BookId", SqlDbType.Int) { Value = bookId };

            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName, bookIdParameter);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return (int)row["CopyId"];
            }
            return null;
        }

        public IEnumerable<BookCopy>? GetBookCopies()
        {
            string storedProcedureName = "SP_GetBookCopies";
            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<BookCopy> copies = new List<BookCopy>();
                foreach (DataRow row in dataTable.Rows)
                {
                    copies.Add(new BookCopy
                    {
                        CopyId = (int)row["CopyId"],
                        BookId = (int)row["BookId"],
                        IsAvailable = (bool)row["IsAvailable"]
                    });
                }
                return copies;
            }
            return null;
        }
    }
}
