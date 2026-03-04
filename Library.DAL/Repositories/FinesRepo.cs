using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class FinesRepo : IFinesRepo
    {
        public IEnumerable<Fine>? GetFines()
        {
            string storedProcedureName = "SP_GetFines";
            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<Fine> fines = new List<Fine>();
                foreach (DataRow row in dataTable.Rows)
                {
                    fines.Add(new Fine
                    {
                        FineId = (int)row["FineId"],
                        UserId = (int)row["UserId"],
                        BorrowingRecordId = (int)row["BorrowingRecordId"],
                        NumberOfLateDays = (int)row["NumberOfLateDays"],
                        FineAmount = (decimal)row["FineAmount"]
                    });
                }
                return fines;
            }
            return null;
        }
    }
}
