using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Core.TableModels;

namespace DAL.Exceptions
{
    class ExceptionService
    {
        public static bool InsertExcepton(ExceptionModel exception)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand("dbo.CreateException", new SqlConnection(ConnectionString)))
            {
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@P_ExceptionObjectSerialized", exception.SerializedExceptionObject);
                    sqlCmd.Parameters.AddWithValue("@P_TimeHappened", exception.TimeHappened);
                    sqlCmd.Parameters.AddWithValue("@P_Namespace", exception.Namespace);
                    sqlCmd.Parameters.AddWithValue("@P_ClassName", exception.ClassName);
                    sqlCmd.Parameters.AddWithValue("@P_MethodName", exception.MethodName);

                    sqlCmd.Connection.Open();
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception)
                {
                }
                return false;

            }
        }

        public static ExceptionModel GetExceptionById(string id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand("dbo.GetExceptionById", new SqlConnection(ConnectionString)))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ExceptionId", id);
                sqlCmd.Connection.Open();

                try
                {
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new ExceptionModel
                        {
                            Id = reader["Id"].ToString(),
                            ClassName = reader["ClassName"].ToString(),
                            MethodName = reader["MethodName"].ToString(),
                            Namespace = reader["Namespace"].ToString(),
                            SerializedExceptionObject = reader["ExceptionObjectSerialized"].ToString(),
                            TimeHappened = Convert.ToDateTime(reader["TimeHappened"].ToString())
                        };
                    }
                }
                catch (Exception)
                {
                }
                return null;
            }
        }

        public static List<ExceptionModel> GetAllExceptions()
        {
            List<ExceptionModel> OutExceptions = new List<ExceptionModel>();

            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.SelectExceptions", new SqlConnection(ConnectionString)))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OutExceptions.Add(new ExceptionModel
                        {
                            Id = reader["Id"].ToString(),
                            ClassName = reader["ClassName"].ToString(),
                            MethodName = reader["MethodName"].ToString(),
                            Namespace = reader["Namespace"].ToString(),
                            SerializedExceptionObject = reader["ExceptionObjectSerialized"].ToString(),
                            TimeHappened = Convert.ToDateTime(reader["TimeHappened"].ToString())
                        });
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return OutExceptions;
            }
        }
    }
}
