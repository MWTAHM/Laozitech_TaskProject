using Core.DTO;
using Core.Models;
using Core.TableModels;
using DAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Task
{
    public class TaskService
    {
        public static bool InsertTask(TaskModel task)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("[dbo].[CreateTask]", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_TaskId", task.TaskId);
                sqlCmd.Parameters.AddWithValue("@P_TaskName", task.TaskName);
                sqlCmd.Parameters.AddWithValue("@P_TaskDescription", task.TaskDescription);
                sqlCmd.Parameters.AddWithValue("@P_TaskStartTime", task.TaskStartTime);
                sqlCmd.Parameters.AddWithValue("@P_TaskEndTime", task.TaskEndTime);
                sqlCmd.Parameters.AddWithValue("@P_ParentProjectId", task.ParentProjectId);
                sqlCmd.Parameters.AddWithValue("@P_TaskLastEdit", DateTime.Now.ToString());

                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(InsertTask),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static List<DTO_TaskUserInfo> GetUsersWorkingOnTask(string id)
        {
            var OutTasks = new List<DTO_TaskUserInfo>();
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.GetUsersWorkingOnTask", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_TaskId", id);
                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OutTasks.Add(new DTO_TaskUserInfo
                        {
                            Email = reader["Email"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            Country = reader["Country"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            UserName = reader["UserName"].ToString(),
                        });
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(GetUsersWorkingOnTask),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
                return OutTasks;
            }
        }

        public static bool UnAssignTaskFromUser(string userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"[dbo].[UnAssignTaskFromUser]", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_UserId", userId);                
                try
                {
                    sqlCmd.Connection.Open();
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(AssignTaskToUser),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static string GetTaskName(string taskId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("[dbo].[GetTaskName]", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_TaskId", taskId);
                sqlCmd.Connection.Open();
                try
                {
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["TaskName"].ToString();
                    }
                    return "No Task";
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(InsertTask),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return "Error";
            }
        }

        public static bool AssignTaskToUser(string taskId, string userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"[dbo].[AssignTaskToUser]", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_User_Id", userId);
                sqlCmd.Parameters.AddWithValue("@P_TaskId", taskId);
                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(AssignTaskToUser),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static List<TaskModel> GetProjectTasks(string projectId)
        {
            List<TaskModel> outTasks = new List<TaskModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.GetProjectsTasks", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ParentProjectId", projectId);
                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime? lastEdit;
                        try
                        {
                            lastEdit = DateTime.Parse(reader["TaskLastEdit"].ToString());
                        }
                        catch (Exception)
                        {
                            lastEdit = null;
                        }
                        outTasks.Add(new TaskModel
                        {
                            TaskId = reader["TaskId"].ToString(),
                            TaskName = reader["TaskName"].ToString(),
                            TaskDescription = reader["TaskDescription"].ToString(),
                            TaskStartTime = DateTime.Parse(reader["TaskStartTime"].ToString()),
                            TaskEndTime = DateTime.Parse(reader["TaskEndTime"].ToString()),
                            ParentProjectId = projectId,
                            TaskLastEdit = lastEdit
                        });
                    }
                    return outTasks;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(GetProjectTasks),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
            }
        }

        public static TaskModel GetTaskById(string taskId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.GetTaskById", connection))
            {

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_TaskId", taskId);
                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    reader.Read();

                    DateTime? lastEdit;
                    if (reader["TaskLastEdit"] is DBNull)
                    {
                        lastEdit = null;
                    }
                    else
                    {
                        lastEdit = DateTime.Parse(reader["TaskLastEdit"].ToString());
                    }

                    return new TaskModel
                    {
                        TaskId = reader["TaskId"].ToString(),
                        TaskName = reader["TaskName"].ToString(),
                        TaskDescription = reader["TaskDescription"].ToString(),
                        TaskStartTime = DateTime.Parse(reader["TaskStartTime"].ToString()),
                        TaskEndTime = DateTime.Parse(reader["TaskEndTime"].ToString()),
                        ParentProjectId = reader["ParentProjectId"].ToString(),
                        TaskLastEdit = lastEdit
                    };
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(GetTaskById),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
            }
        }

        public static bool UpdateTask(TaskModel task)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.UpdateTask", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_TaskId", task.TaskId);
                sqlCmd.Parameters.AddWithValue("@P_TaskName", task.TaskName);
                sqlCmd.Parameters.AddWithValue("@P_TaskDescription", task.TaskDescription);
                sqlCmd.Parameters.AddWithValue("@P_TaskEndTime", task.TaskEndTime);
                sqlCmd.Parameters.AddWithValue("@P_TaskLastEdit", DateTime.Now.ToString());

                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(UpdateTask),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static bool DeleteTask(string taskId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.DeleteTask", connection))
            {
                sqlCmd.Parameters.AddWithValue("@P_TaskId", taskId);
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
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(DeleteTask),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static List<TaskModel> GetAllTasks()
        {
            List<TaskModel> OutTasks = new List<TaskModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.GetAllTasks", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OutTasks.Add(new TaskModel
                        {
                            TaskId = reader["TaskId"].ToString(),
                            TaskName = reader["TaskName"].ToString(),
                            TaskDescription = reader["TaskDescription"].ToString(),
                            TaskStartTime = DateTime.Parse(reader["TaskStartTime"].ToString()),
                            TaskEndTime = DateTime.Parse(reader["TaskEndTime"].ToString()),
                            ParentProjectId = reader["ParentProjectId"].ToString(),
                            TaskLastEdit = DateTime.Parse(reader["TaskLastEdit"].ToString())
                        });
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(TaskService),
                        ExceptionObject = e,
                        MethodName = nameof(GetAllTasks),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
                return OutTasks;
            }
        }
    }
}
