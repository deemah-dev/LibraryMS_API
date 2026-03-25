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

            return CommonRepos.ReturnValue_int(storedProcedureName, userIdParameter, copyIdParameter, borrowingDateParameter);
        }

        public bool ReturnBook(BorrowingRecord borrowingRecord)
        {
            string storedProcedureName = "SP_ReturnBook";
            SqlParameter borrowingRecordIdParameter = new SqlParameter("@BorrowingRecordId", SqlDbType.Int) { Value = borrowingRecord.BorrowingRecordId};
            SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.Int) { Value = borrowingRecord.UserId };
            SqlParameter actualReturnDateParameter = new SqlParameter("@ActualReturnDate", SqlDbType.Date) { Value = borrowingRecord.ActualReturnDate };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, borrowingRecordIdParameter, userIdParameter, actualReturnDateParameter);
        }

        public decimal CheckBorrowFine(int borrowingRecordId, DateTime actualReturnDate)
        {
            string storedProcedureName = "SP_CheckBorrowFine";
            SqlParameter borrowingRecordIdParameter = new SqlParameter("@BorrowingRecordId", SqlDbType.Int) { Value = borrowingRecordId };
            SqlParameter actualReturnDateParameter = new SqlParameter("@ActualReturnDate", SqlDbType.Date) { Value = actualReturnDate };

            return CommonRepos.ReturnValue_dec(storedProcedureName, borrowingRecordIdParameter, actualReturnDateParameter);
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

        public BorrowingRecord? GetBorrowingRecord(int borrowingRecordId)
        {
            string storedProcedureName = "SP_GetBorrowingRecordById";
            SqlParameter borrowingRecordIdParameter = new SqlParameter("@BorrowingRecordId", SqlDbType.Int) { Value = borrowingRecordId };

            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName, borrowingRecordIdParameter);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new BorrowingRecord
                    {
                        BorrowingRecordId = (int)row["BorrowingRecordId"],
                        UserId = (int)row["UserId"],
                        CopyId = (int)row["CopyId"],
                        BorrowingDate = (DateTime)row["BorrowingDate"],
                        DueDate = (DateTime)row["DueDate"],
                        ActualReturnDate = row["ActualReturnDate"] != DBNull.Value ? (DateTime)row["ActualReturnDate"] : null
                    };
            }
            return null;
        }
    }
}
