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
    public class ImageService
    {
        public static bool InsertImage(ImageModel Image)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"[dbo].[CreateImage]", connection))
            {
                if (Image.ProjectId == null && Image.TaskId == null)
                {
                    return false;
                }

                object UserId = Image.UserId;
                if (Image.UserId == null)
                {
                    UserId = DBNull.Value;
                }

                object ProjectId = Image.ProjectId;
                if (Image.ProjectId == null)
                {
                    ProjectId = DBNull.Value;
                }

                object TaskId = Image.TaskId;
                if (Image.TaskId == null)
                {
                    TaskId = DBNull.Value;
                }

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ImageFileName", Image.ImageFileName);
                sqlCmd.Parameters.AddWithValue("@P_ImageFileExtension", Image.ImageFileExtension);
                sqlCmd.Parameters.AddWithValue("@P_ImageFileData", Image.ImageFileData);
                sqlCmd.Parameters.AddWithValue("@P_ImageAddingTime", Image.ImageAddingTime);
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
                        MethodName = nameof(InsertImage),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static bool DeleteImage(/*Must Contain User Id*/string ImageId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"[dbo].[DeleteImage]", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ImageId", ImageId);
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
                        MethodName = nameof(DeleteImage),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        /*Id can be user, project, or task Id*/
        public static List<ImageModel> GetImagesById(string Id, EFileOwnerType ImageOwnerType)
        {
            List<ImageModel> OutImages = new List<ImageModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.GetImagesById", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_OwnerId", Id);
                sqlCmd.Parameters.AddWithValue("@P_OwnerType", ImageOwnerType.ToString());

                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OutImages.Add(new ImageModel
                        {
                            ImageId = reader["ImageId"].ToString(),
                            ImageFileName = reader["ImageFileName"].ToString(),
                            ImageFileExtension = reader["ImageFileExtension"].ToString(),
                            ImageAddingTime = DateTime.Parse(reader["ImageAddingTime"].ToString()),
                            DataFromBytes = Encoding.ASCII.GetBytes(reader["ImageFileData"].ToString()),
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
                        MethodName = nameof(GetImagesById),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
                return OutImages;
            }
        }
    }
}
