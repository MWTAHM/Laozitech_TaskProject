using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DAL.Exceptions;
using Core.TableModels;

namespace DAL.Task
{
    public class TaskService
    {
        public static bool InsertTask(TaskModel task)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand("[dbo].[CreateTask]", new SqlConnection(ConnectionString)))
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

        public static string GetTaskName(string taskId)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand("[dbo].[GetTaskName]", new SqlConnection(ConnectionString)))
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
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"[dbo].[AssignTaskToUser]", new SqlConnection(ConnectionString)))
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
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand("dbo.GetProjectsTasks", new SqlConnection(ConnectionString)))
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
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand("dbo.GetTaskById", new SqlConnection(ConnectionString)))
            {

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_TaskId", taskId);
                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    reader.Read();

                    DateTime? lastEdit;
                    if(reader["TaskLastEdit"] is DBNull)
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
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.UpdateTask", new SqlConnection(ConnectionString)))
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
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.DeleteTask", new SqlConnection(ConnectionString)))
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
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"dbo.GetAllTasks", new SqlConnection(ConnectionString)))
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
