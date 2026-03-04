using Library.Core.Models;
using Library.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    public class UsersRepo : IUsersRepo
    {
        public int InsertUser(User user)
        {
            string storedProcedureName = "SP_InsertUser";
            SqlParameter usernameParameter = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = user.Username };
            SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.NVarChar) { Value = user.PasswordHash };
            SqlParameter roleIdParameter = new SqlParameter("@RoleId", SqlDbType.Int) { Value = user.RoleId };

            return CommonRepos.ReturnValue(storedProcedureName, usernameParameter, passwordHashParameter, roleIdParameter);
        }

        public bool UpdateUser(User user)
        {
            string storedProcedureName = "SP_UpdateUser";
            SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.Int) { Value = user.UserId };
            SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.NVarChar) { Value = user.PasswordHash };
            SqlParameter roleIdParameter = new SqlParameter("@RoleId", SqlDbType.Int) { Value = user.RoleId };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, userIdParameter, passwordHashParameter, roleIdParameter);
        }

        public bool DeleteUser(int userId)
        {
            string storedProcedureName = "SP_DeleteUser";
            SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };

            return CommonRepos.ExecuteNonQuery(storedProcedureName, userIdParameter);
        }

        public User? GetUserById(int userId)
        {
            string storedProcedureName = "SP_GetUserById";
            SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };

            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName, userIdParameter);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new User
                {
                    UserId = (int)row["UserId"],
                    Username = (string)row["Username"],
                    PasswordHash = (string)row["PasswordHash"],
                    RoleId = (int)row["RoleId"]
                };
            }
            return null;
        }

        public User? GetUserByUsername(string username)
        {
            string storedProcedureName = "SP_GetUserByUsername";
            SqlParameter usernameParameter = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName, usernameParameter);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new User
                {
                    UserId = (int)row["UserId"],
                    Username = (string)row["Username"],
                    PasswordHash = (string)row["PasswordHash"],
                    RoleId = (int)row["RoleId"]
                };
            }
            return null;
        }

        public IEnumerable<User>? GetUsers()
        {
            string storedProcedureName = "SP_GetUsers";
            DataTable? dataTable = CommonRepos.GetAll(storedProcedureName);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<User> users = new List<User>();
                foreach (DataRow row in dataTable.Rows)
                {
                    users.Add(new User
                    {
                        UserId = (int)row["UserId"],
                        Username = (string)row["Username"],
                        PasswordHash = (string)row["PasswordHash"],
                        RoleId = (int)row["RoleId"]
                    });
                }
                return users;
            }
            return null;
        }
    }
}
