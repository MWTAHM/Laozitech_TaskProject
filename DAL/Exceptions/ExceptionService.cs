using Core.TableModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Exceptions
{
    class ExceptionService
    {
        public static bool InsertExcepton(ExceptionModel exception)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectconnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.CreateException", connection))
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
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectconnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.GetExceptionById", connection))
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

            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectconnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.SelectExceptions", connection))
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
