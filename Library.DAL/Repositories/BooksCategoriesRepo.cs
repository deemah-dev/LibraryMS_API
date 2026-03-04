using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class BooksCategoriesRepo : IBooksCategoriesRepo
    {
        public int InsertBooksCategory(BookCategory category)
        {
            string storedProcedureName = "SP_InsertBooksCategory";
            SqlParameter nameParameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = category.Name };

            return CommonRepos.ReturnValue(storedProcedureName, nameParameter);
        }

        public bool UpdateBooksCategory(BookCategory category)
        {
            string storedProcedureName = "SP_UpdateBooksCategory";
            SqlParameter categoryIdParameter = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = category.CategoryId };
            SqlParameter nameParameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = category.Name };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, categoryIdParameter, nameParameter);
        }

        public bool DeleteBooksCategory(int categoryId)
        {
            string storedProcedureName = "SP_DeleteBooksCategory";
            SqlParameter categoryIdParameter = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = categoryId };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, categoryIdParameter);
        }

        public BookCategory? GetBooksCategoryById(int categoryId)
        {
            string storedProcedureName = "SP_GetBooksCategoryById";
            SqlParameter categoryIdParameter = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = categoryId };

            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName, categoryIdParameter);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new BookCategory
                {
                    CategoryId = (int)row["CategoryId"],
                    Name = (string)row["Name"]
                };
            }
            return null;
        }

        public IEnumerable<BookCategory>? GetBooksCategories()
        {
            string storedProcedureName = "SP_GetBooksCategories";
            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<BookCategory> categories = new List<BookCategory>();
                foreach (DataRow row in dataTable.Rows)
                {
                    categories.Add(new BookCategory
                    {
                        CategoryId = (int)row["CategoryId"],
                        Name = (string)row["Name"]
                    });
                }
                return categories;
            }
            return null;
        }
    }
}
