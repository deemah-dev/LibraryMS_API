using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.DAL.Repositories
{
    internal static class CommonRepos
    {
        public static bool CheckTruefalse(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(DbHelper.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand { CommandText = storedProcedureName, Connection = connection, CommandType = CommandType.StoredProcedure })
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    SqlParameter returnParameter = new SqlParameter
                    {
                        ParameterName = "@ReturnValue",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnParameter);

                    bool success = false;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        success = (int)returnParameter.Value == 1;
                    }
                    catch (Exception)
                    {
                        //string sourceName = "OnlineStoreManagementSystemApplication";
                        //if (!EventLog.SourceExists(sourceName))
                        //{
                        //    EventLog.CreateEventSource(sourceName, "Application");
                        //}
                    }
                    return success;
                }
            }
        }
        public static DataTable? GetAll(string storedProcedureName)
        {
            return GetAll(storedProcedureName, new SqlParameter[0]);
        }
        public static DataTable? GetAll(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(DbHelper.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand { CommandText = storedProcedureName, Connection = connection, CommandType = CommandType.StoredProcedure })
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dataTable = new();
                                dataTable.Load(reader);
                                return dataTable;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return null;
        }
        public static bool ExecuteNonQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(DbHelper.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand { CommandText = storedProcedureName, Connection = connection, CommandType = CommandType.StoredProcedure })
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    bool success = false;
                    try
                    {
                        connection.Open();
                        success = command.ExecuteNonQuery() > 0;
                    }
                    catch (Exception)
                    {
                        //string sourceName = "OnlineStoreManagementSystemApplication";
                        //if (!EventLog.SourceExists(sourceName))
                        //{
                        //    EventLog.CreateEventSource(sourceName, "Application");
                        //}
                    }
                    return success;
                }

            }
        }
        internal static int ReturnValue(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(DbHelper.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand { CommandText = storedProcedureName, Connection = connection, CommandType = CommandType.StoredProcedure })
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    SqlParameter returnParameter = new SqlParameter
                    {
                        ParameterName = "@ReturnValue",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnParameter);

                    int returnedValue = -1;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        returnedValue = (int)returnParameter.Value;
                    }
                    catch (Exception)
                    {
                        //string sourceName = "OnlineStoreManagementSystemApplication";
                        //if (!EventLog.SourceExists(sourceName))
                        //{
                        //    EventLog.CreateEventSource(sourceName, "Application");
                        //}
                    }
                    return returnedValue;
                }
            }
        }
        internal static int ReturnValue(string storedProcedureName)
        {
            return ReturnValue(storedProcedureName, new SqlParameter[0]);
        }
    }
}
