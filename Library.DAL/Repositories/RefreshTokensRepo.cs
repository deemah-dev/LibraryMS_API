using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class RefreshTokensRepo : IRefreshTokensRepo
    {
        public RefreshToken? GetRefreshToken(string refreshTokenHash)
        {
            string storedProcedureName = "SP_GetRefreshToken";
            SqlParameter refreshTokenHashParameter = new SqlParameter("@RefreshTokenHash", SqlDbType.NVarChar) { Value = refreshTokenHash };

            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName, refreshTokenHashParameter);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new RefreshToken
                {
                    RefreshTokenId = (int)row["RefreshTokenId"],
                    UserId = (int)row["UserId"],
                    RefreshTokenHash = (string)row["RefreshTokenHash"],
                    CreatedAt = (DateTime)row["CreatedAt"],
                    ExpirationDate = (DateTime)row["ExpirationDate"],
                    RevokedAt = row["RevokedAt"] != DBNull.Value ? (DateTime)row["RevokedAt"] : null
                };
            }
            return null;
        }

        public int InsertRefreshToken(RefreshToken refreshToken)
        {
            string storedProcedureName = "SP_InsertRefreshToken";
            SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.Int) { Value = refreshToken.UserId };
            SqlParameter refreshTokenHashParameter = new SqlParameter("@RefreshTokenHash", SqlDbType.NVarChar) { Value = refreshToken.RefreshTokenHash };
            SqlParameter createdAtParameter = new SqlParameter("@CreatedAt", SqlDbType.Date) { Value = refreshToken.CreatedAt };
            SqlParameter expirationDateParameter = new SqlParameter("@ExpirationDate", SqlDbType.Date) { Value = refreshToken.ExpirationDate };
            SqlParameter revokedAtParameter = new SqlParameter("@RevokedAt", SqlDbType.Date) { Value = refreshToken.RevokedAt ?? (object)DBNull.Value };

            return CommonRepos.ReturnValue_int(storedProcedureName, userIdParameter, refreshTokenHashParameter, createdAtParameter, expirationDateParameter, revokedAtParameter);
        }

        public bool RevokeRefreshToken(string refreshTokenHash, DateTime revokedAt)
        {
            string storedProcedureName = "SP_RevokeRefreshToken";
            SqlParameter refreshTokenHashParameter = new SqlParameter("@RefreshTokenHash", SqlDbType.NVarChar) { Value = refreshTokenHash };
            SqlParameter revokedAtParameter = new SqlParameter("@RevokedAt", SqlDbType.Date) { Value = revokedAt };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, refreshTokenHashParameter, revokedAtParameter);
        }
    }
}

