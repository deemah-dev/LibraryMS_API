using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class SettingsRepo : ISettingsRepo
    {
        public Settings? GetSettings()
        {
            string storedProcedureName = "SP_GetSettings";
            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new Settings
                {
                    Id = (int)row["Id"],
                    DefaultBorrowDays = (int)row["DefaultBorrowDays"],
                    DefaultFinePerDay = (decimal)row["DefaultFinePerDay"]
                };
            }
            return null;
        }

        public bool UpdateDefaultBorrowDaysSettings(int defaultBorrowDays)
        {
            string storedProcedureName = "SP_UpdateDefaultBorrowDaysSettings";
            SqlParameter defaultBorrowDaysParameter = new SqlParameter("@DefaultBorrowDays", SqlDbType.Int) { Value = defaultBorrowDays };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, defaultBorrowDaysParameter);
        }

        public bool UpdateDefaultFinePerDaySettings(decimal defaultFinePerDay)
        {
            string storedProcedureName = "SP_UpdateDefaultFinePerDaySettings";
            SqlParameter defaultFinePerDayParameter = new SqlParameter("@DefaultFinePerDay", SqlDbType.SmallMoney) { Value = defaultFinePerDay };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, defaultFinePerDayParameter);
        }
    }
}
