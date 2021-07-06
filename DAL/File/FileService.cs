using Core.TableModels;
using DAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Project
{
    public enum EFileOwnerType
    {
        user = 0,
        project = 1,
        task = 2
    }

    public class FileService
    {
        public static bool InsertFile(FileModel file)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"[dbo].[CreateFile]", connection))
            {
                if (file.ProjectId == null && file.TaskId == null)
                {
                    return false;
                }

                object UserId = file.UserId;
                if (file.UserId == null)
                {
                    UserId = DBNull.Value;
                }

                object ProjectId = file.ProjectId;
                if (file.ProjectId == null)
                {
                    ProjectId = DBNull.Value;
                }

                object TaskId = file.TaskId;
                if (file.TaskId == null)
                {
                    TaskId = DBNull.Value;
                }

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_FileName", file.FileName);
                sqlCmd.Parameters.AddWithValue("@P_FileExtension", file.FileExtension);
                sqlCmd.Parameters.AddWithValue("@P_FileData", file.FileData);
                sqlCmd.Parameters.AddWithValue("@P_FileAddingTime", file.FileAddingTime);
                sqlCmd.Parameters.AddWithValue("@P_UserId", UserId);
                sqlCmd.Parameters.AddWithValue("@P_TaskId", TaskId);
                sqlCmd.Parameters.AddWithValue("@P_ProjectId", ProjectId);
                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ImageService),
                        ExceptionObject = e,
                        MethodName = nameof(InsertFile),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static bool DeleteFile(string fileId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"[dbo].[DeleteFile]", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_FileId", fileId);
                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ImageService),
                        ExceptionObject = e,
                        MethodName = nameof(DeleteFile),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        /*Id can be user, project, or task Id*/
        public static List<FileModel> GetFilesById(string Id, EFileOwnerType fileOwnerType)
        {
            List<FileModel> OutFiles = new List<FileModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.GetFilesById", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_OwnerId", Id);
                sqlCmd.Parameters.AddWithValue("@P_OwnerType", fileOwnerType.ToString());

                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OutFiles.Add(new FileModel
                        {
                            FileId = reader["FileId"].ToString(),
                            FileName = reader["FileName"].ToString(),
                            FileExtension = reader["FileExtension"].ToString(),
                            FileAddingTime = DateTime.Parse(reader["FileAddingTime"].ToString()),
                            DataFromBytes = Encoding.ASCII.GetBytes(reader["FileName"].ToString()),
                            ProjectId = reader["ProjectId"].ToString(),
                            TaskId = reader["TaskId"].ToString(),
                            UserId = reader["UserId"].ToString(),

                        });
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ImageService),
                        ExceptionObject = e,
                        MethodName = nameof(GetFilesById),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
                return OutFiles;

            }
        }
    }
}
