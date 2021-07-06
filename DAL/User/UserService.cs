using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DAL.Exceptions;
using Core.TableModels;
using Core.DTO;

namespace DAL.Project
{
    public class UserService
    {
        public static bool InsertUser(UserModel user)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.RegisterUser", new SqlConnection(ConnectionString)))
            {
                sqlCmd.Parameters.AddWithValue("@P_UserName", user.UserName);
                sqlCmd.Parameters.AddWithValue("@P_FullName", user.FullName);
                sqlCmd.Parameters.AddWithValue("@P_Email", user.EmailAddress);
                sqlCmd.Parameters.AddWithValue("@P_BirthDate", user.BirthDate);
                sqlCmd.Parameters.AddWithValue("@P_PasswordEncoded", user.PasswordEncoded);
                sqlCmd.Parameters.AddWithValue("@P_PhoneNumber", user.PhoneNumber);
                sqlCmd.Parameters.AddWithValue("@P_Country", user.Country);
                sqlCmd.Parameters.AddWithValue("@P_City", user.City);
                sqlCmd.Parameters.AddWithValue("@P_Address1", user.Address1);
                sqlCmd.Parameters.AddWithValue("@P_Address2", user.Address2);
                sqlCmd.Parameters.AddWithValue("@P_Postal", user.Postal);

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(UserService),
                        ExceptionObject = e,
                        MethodName = nameof(InsertUser),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static string GetUserName(string id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand("[dbo].[GetUserName]", new SqlConnection(ConnectionString)))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_UserId", id);
                sqlCmd.Connection.Open();
                try
                {
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["UserName"].ToString();
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(UserService),
                        ExceptionObject = e,
                        MethodName = nameof(GetUserName),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return "Error";
            }
        }

        public static List<DTO_UserInfo_DropDown> GetAllUsersDropDownInfo()
        {
            List<DTO_UserInfo_DropDown> OutUsers = new List<DTO_UserInfo_DropDown>();
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.GetAllUsers", new SqlConnection(ConnectionString)))
            {
                try
                {
                    sqlCmd.Connection.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OutUsers.Add(new DTO_UserInfo_DropDown
                        {
                            UserId = reader["UserId"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            HasTask = !(reader["CurrentTaskId"] is DBNull)
                        });
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(UserService),
                        ExceptionObject = e,
                        MethodName = nameof(GetAllUsersDropDownInfo),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
                return OutUsers;
            }
        }

        public static UserModel GetUserById(string id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand("dbo.GetUserById", new SqlConnection(ConnectionString)))
            {

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_UserId", id);
                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    DateTime? birthdate;
                    if (reader["BirthDate"] is DBNull)
                    {
                        birthdate = null;
                    }
                    else
                    {
                        birthdate = DateTime.Parse(reader["BirthDate"].ToString());
                    }

                    return new UserModel
                    {
                        UserName = reader["UserName"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        EmailAddress = reader["Email"].ToString(),
                        IsEmailConfirmed = bool.Parse(reader["IsEmailConfirmed"].ToString()),
                        BirthDate = birthdate,
                        UserRegistratonTime = DateTime.Parse(reader["UserRegistratonTime"].ToString()),
                        PasswordEncoded = reader["PasswordEncoded"].ToString(),
                        CurrentProjectId = reader["CurrentProjectId"].ToString(),
                        CurrentTaskId = reader["CurrentTaskId"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Country = reader["Country"].ToString(),
                        City = reader["City"].ToString(),
                        Address1 = reader["Address1"].ToString(),
                        Address2 = reader["Address2"].ToString(),
                        Postal = reader["Postal"].ToString()
                    };
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(UserService),
                        ExceptionObject = e,
                        MethodName = nameof(GetUserById),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
            }
        }

        public static bool UpdateUser(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.UserId))
                return false;

            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.UpdateUser", new SqlConnection(ConnectionString)))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@P_City", user.City);
                sqlCmd.Parameters.AddWithValue("@P_Postal", user.Postal);
                sqlCmd.Parameters.AddWithValue("@P_UserId", user.UserId);
                sqlCmd.Parameters.AddWithValue("@P_Country", user.Country);
                sqlCmd.Parameters.AddWithValue("@P_UserName", user.UserName);
                sqlCmd.Parameters.AddWithValue("@P_Address1", user.Address1);
                sqlCmd.Parameters.AddWithValue("@P_Address2", user.Address2);
                sqlCmd.Parameters.AddWithValue("@P_FullName", user.FullName);
                sqlCmd.Parameters.AddWithValue("@P_Email", user.EmailAddress);
                sqlCmd.Parameters.AddWithValue("@P_BirthDate", user.BirthDate);
                sqlCmd.Parameters.AddWithValue("@P_PhoneNumber", user.PhoneNumber);
                sqlCmd.Parameters.AddWithValue("@P_PasswordEncoded", user.PasswordEncoded);

                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(UserService),
                        ExceptionObject = e,
                        MethodName = nameof(UpdateUser),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static bool DeleteUser(string userId)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.DeleteUser", new SqlConnection(ConnectionString)))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_UserId", userId);
                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(UserService),
                        ExceptionObject = e,
                        MethodName = nameof(DeleteUser),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static List<UserModel> GetAllUsers()
        {
            List<UserModel> OutUsers = new List<UserModel>();
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.GetAllUsers", new SqlConnection(ConnectionString)))
            {
                try
                {
                    sqlCmd.Connection.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var dateString = string.IsNullOrEmpty(reader["BirthDate"].ToString()) ? null : (DateTime?)DateTime.Parse(reader["BirthDate"].ToString());
                        OutUsers.Add(new UserModel
                        {
                            UserId = reader["UserId"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            EmailAddress = reader["Email"].ToString(),
                            IsEmailConfirmed = bool.Parse(reader["IsEmailConfirmed"].ToString()),
                            BirthDate = dateString,
                            UserRegistratonTime = DateTime.Parse(reader["UserRegistratonTime"].ToString()),
                            PasswordEncoded = reader["PasswordEncoded"].ToString(),
                            CurrentProjectId = reader["CurrentProjectId"].ToString(),
                            CurrentTaskId = reader["CurrentTaskId"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Country = reader["Country"].ToString(),
                            City = reader["City"].ToString(),
                            Address1 = reader["Address1"].ToString(),
                            Address2 = reader["Address2"].ToString(),
                            Postal = reader["Postal"].ToString()
                        });
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(UserService),
                        ExceptionObject = e,
                        MethodName = nameof(GetAllUsers),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
                return OutUsers;
            }
        }

        
    }
}
