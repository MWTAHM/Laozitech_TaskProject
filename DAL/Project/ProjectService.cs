using Core.DTO;
using Core.Models;
using Core.TableModels;
using DAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Project
{
    public class ProjectService
    {
        public static bool InsertProject(ProjectModel project)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.CreateProject", connection))
            {
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@P_ProjectId", project.ProjectId);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectLastEdit", DateTime.Now);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectName", project.ProjectName);
                    sqlCmd.Parameters.AddWithValue("@P_CompanyName", project.CompanyName);
                    sqlCmd.Parameters.AddWithValue("@P_CompanyPhone", project.CompanyPhone);
                    sqlCmd.Parameters.AddWithValue("@P_CompanyEmail", project.CompanyEmail);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectEndTime", project.ProjectEndTime);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectWebsite", project.ProjectWebsite);
                    sqlCmd.Parameters.AddWithValue("@P_CompanyWebsite", project.CompanyWebsite);
                    sqlCmd.Parameters.AddWithValue("@P_CompanyLocation", project.CompanyLocation);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectStartTime", project.ProjectStartTime);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectManagerId", project.ProjectManagerId);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectTotaBudget", project.ProjectTotalBudget);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectSpentBudget", project.ProjectSpentBudget);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectDescription", project.ProjectDescription);
                    sqlCmd.Parameters.AddWithValue("@P_AchievedPercentage", project.AchievedPercentage);
                    sqlCmd.Parameters.AddWithValue("@P_ProjectManagerName", project.ProjectManagerName);
                    sqlCmd.Connection.Open();
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(InsertProject),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static string GetProjectName(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("[dbo].[GetProjectName]", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ProjectId", id);
                sqlCmd.Connection.Open();
                try
                {
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["ProjectName"].ToString();
                    }
                    return "No Task";
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(GetProjectName),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return "Error";
            }
        }

        public static bool IsValidProject(string projectId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.GetProjectById", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ProjectId", projectId);
                sqlCmd.Connection.Open();

                try
                {
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return !(reader["ProjectId"] is DBNull);
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(IsValidProject),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static bool UnArchiveProject(string projectId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.UnArchiveProject", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ProjectId", projectId);
                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(UnArchiveProject),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static bool ArchiveProject(string projectId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.ArchiveProject", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ProjectId", projectId);
                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(UnArchiveProject),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static ProjectModel GetProjectById(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.GetProjectById", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ProjectId", id);
                sqlCmd.Connection.Open();

                try
                {
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    reader.Read();
                    return new ProjectModel
                    {
                        ProjectId = reader["ProjectId"].ToString(),
                        ProjectName = reader["ProjectName"].ToString(),
                        ProjectDescription = reader["ProjectDescription"].ToString(),
                        ProjectStartTime = DateTime.Parse(reader["ProjectStartTime"].ToString()),
                        ProjectEndTime = DateTime.Parse(reader["ProjectEndTime"].ToString()),
                        ProjectLastEdit = DateTime.Parse(reader["ProjectLastEdit"].ToString()),
                        ProjectTotalBudget = double.Parse(reader["ProjectTotaBudget"].ToString()),
                        ProjectSpentBudget = double.Parse(reader["ProjectSpentBudget"].ToString()),
                        CompanyName = reader["CompanyName"].ToString(),
                        CompanyLocation = reader["CompanyLocation"].ToString(),
                        CompanyPhone = reader["CompanyPhone"].ToString(),
                        CompanyEmail = reader["CompanyEmail"].ToString(),
                        CompanyWebsite = reader["CompanyWebsite"].ToString(),
                        ProjectManagerId = reader["ProjectManagerId"].ToString(),
                        ProjectManagerName = reader["ProjectManagerName"].ToString(),
                        ProjectWebsite = reader["ProjectWebsite"].ToString(),
                        AchievedPercentage = int.Parse(reader["AchievedPercentage"].ToString()),
                        IsArchived = bool.Parse(reader["IsArchived"].ToString())
                    };
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(GetProjectById),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
            }
        }

        public static DTOProjectDetails GetProjectById_Details(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.GetProjectById", connection))
            {
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@P_ProjectId", id);
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Read();
                    return new DTOProjectDetails
                    {
                        ProjectName = reader["ProjectName"].ToString(),
                        ProjectEndTime = DateTime.Parse(reader["ProjectEndTime"].ToString())
                    };
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(GetProjectById_Details),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }

            }
        }

        public static bool UpdateProject(ProjectModel project)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand("dbo.UpdateProject", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ProjectId", project.ProjectId);
                sqlCmd.Parameters.AddWithValue("@P_ProjectName", project.ProjectName);
                sqlCmd.Parameters.AddWithValue("@P_CompanyName", project.CompanyName);
                sqlCmd.Parameters.AddWithValue("@P_CompanyPhone", project.CompanyPhone);
                sqlCmd.Parameters.AddWithValue("@P_CompanyEmail", project.CompanyEmail);
                sqlCmd.Parameters.AddWithValue("@P_ProjectEndTime", project.ProjectEndTime);
                sqlCmd.Parameters.AddWithValue("@P_ProjectWebsite", project.ProjectWebsite);
                sqlCmd.Parameters.AddWithValue("@P_CompanyWebsite", project.CompanyWebsite);
                sqlCmd.Parameters.AddWithValue("@P_ProjectLastEdit", DateTime.Now);
                sqlCmd.Parameters.AddWithValue("@P_CompanyLocation", project.CompanyLocation);
                sqlCmd.Parameters.AddWithValue("@P_ProjectStartTime", project.ProjectStartTime);
                sqlCmd.Parameters.AddWithValue("@P_ProjectManagerId", project.ProjectManagerId);
                sqlCmd.Parameters.AddWithValue("@P_ProjectTotaBudget", project.ProjectTotalBudget);
                sqlCmd.Parameters.AddWithValue("@P_ProjectSpentBudget", project.ProjectSpentBudget);
                sqlCmd.Parameters.AddWithValue("@P_ProjectDescription", project.ProjectDescription);
                sqlCmd.Parameters.AddWithValue("@P_AchievedPercentage", project.AchievedPercentage);
                sqlCmd.Parameters.AddWithValue("@P_ProjectManagerName", project.ProjectManagerName);

                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(UpdateProject),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static bool DeleteProject(string projectId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.DeleteProject", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@P_ProjectId", projectId);
                sqlCmd.Connection.Open();
                try
                {
                    return sqlCmd.ExecuteNonQuery() == 1;
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(DeleteProject),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                }
                return false;
            }
        }

        public static List<ProjectModel> GetAllProjects()
        {
            List<ProjectModel> OutProjects = new List<ProjectModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCmd = new SqlCommand($"dbo.SelectProjects", connection))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OutProjects.Add(new ProjectModel
                        {
                            ProjectId = reader["ProjectId"].ToString(),
                            ProjectName = reader["ProjectName"].ToString(),
                            ProjectDescription = reader["ProjectDescription"].ToString(),
                            ProjectStartTime = DateTime.Parse(reader["ProjectStartTime"].ToString()),
                            ProjectEndTime = DateTime.Parse(reader["ProjectEndTime"].ToString()),
                            ProjectLastEdit = DateTime.Parse(reader["ProjectEndTime"].ToString()),
                            ProjectTotalBudget = double.Parse(reader["ProjectTotaBudget"].ToString()),
                            ProjectSpentBudget = double.Parse(reader["ProjectSpentBudget"].ToString()),
                            CompanyName = reader["CompanyName"].ToString(),
                            CompanyLocation = reader["CompanyLocation"].ToString(),
                            CompanyPhone = reader["CompanyPhone"].ToString(),
                            CompanyEmail = reader["CompanyEmail"].ToString(),
                            CompanyWebsite = reader["CompanyWebsite"].ToString(),
                            ProjectManagerId = reader["ProjectManagerId"].ToString(),
                            ProjectManagerName = reader["ProjectManagerName"].ToString(),
                            ProjectWebsite = reader["ProjectWebsite"].ToString(),
                            AchievedPercentage = int.Parse(reader["AchievedPercentage"].ToString()),
                            IsArchived = bool.Parse(reader["IsArchived"].ToString())
                        });
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.InsertExcepton(new ExceptionModel
                    {
                        ClassName = nameof(ProjectService),
                        ExceptionObject = e,
                        MethodName = nameof(GetAllProjects),
                        Namespace = nameof(Project),
                        TimeHappened = DateTime.Now
                    });
                    return null;
                }
                return OutProjects;
            }
        }
    }
}
