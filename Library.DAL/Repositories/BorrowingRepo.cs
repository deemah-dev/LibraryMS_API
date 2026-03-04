using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class BorrowingRepo : IBorrowingRepo
    {
        public int InsertBorrowingRecord(BorrowingRecord borrowingRecord)
        {
            string storedProcedureName = "SP_InsertBorrowingRecord";
            SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.Int) { Value = borrowingRecord.UserId };
            SqlParameter copyIdParameter = new SqlParameter("@CopyId", SqlDbType.Int) { Value = borrowingRecord.CopyId };
            SqlParameter borrowingDateParameter = new SqlParameter("@BorrowingDate", SqlDbType.Date) { Value = borrowingRecord.BorrowingDate };

            return CommonRepos.ReturnValue(storedProcedureName, userIdParameter, copyIdParameter, borrowingDateParameter);
        }

        public bool ReturnBook(int borrowingRecordId, int userId, DateTime actualReturnDate)
        {
            string storedProcedureName = "SP_ReturnBook";
            SqlParameter borrowingRecordIdParameter = new SqlParameter("@BorrowingRecordId", SqlDbType.Int) { Value = borrowingRecordId };
            SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };
            SqlParameter actualReturnDateParameter = new SqlParameter("@ActualReturnDate", SqlDbType.Date) { Value = actualReturnDate };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, borrowingRecordIdParameter, userIdParameter, actualReturnDateParameter);
        }

        public bool CheckBorrowFine(int borrowingRecordId, DateTime actualReturnDate)
        {
            string storedProcedureName = "SP_CheckBorrowFine";
            SqlParameter borrowingRecordIdParameter = new SqlParameter("@BorrowingRecordId", SqlDbType.Int) { Value = borrowingRecordId };
            SqlParameter actualReturnDateParameter = new SqlParameter("@ActualReturnDate", SqlDbType.Date) { Value = actualReturnDate };

            return CommonRepos.CheckTruefalse(storedProcedureName, borrowingRecordIdParameter, actualReturnDateParameter);
        }

        public IEnumerable<BorrowingRecord>? GetBorrowingRecords()
        {
            string storedProcedureName = "SP_GetBorrowingRecords";
            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<BorrowingRecord> records = new List<BorrowingRecord>();
                foreach (DataRow row in dataTable.Rows)
                {
                    records.Add(new BorrowingRecord
                    {
                        BorrowingRecordId = (int)row["BorrowingRecordId"],
                        UserId = (int)row["UserId"],
                        CopyId = (int)row["CopyId"],
                        BorrowingDate = (DateTime)row["BorrowingDate"],
                        DueDate = (DateTime)row["DueDate"],
                        ActualReturnDate = row["ActualReturnDate"] != DBNull.Value ? (DateTime)row["ActualReturnDate"] : null
                    });
                }
                return records;
            }
            return null;
        }
    }
}
