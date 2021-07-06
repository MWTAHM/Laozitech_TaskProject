using Core.DTO;
using Core.Models;
using DAL.Project;
using System.Collections.Generic;
namespace BLL.Project
{
    public class ProjectController
    {
        public static bool NewProject(ProjectModel project)
        {
            if (ProjectService.InsertProject(project))
            {
                if (project.Files != null)
                {
                    foreach (var file in project.Files)
                    {
                        FileService.InsertFile(file);
                    }
                }
                if (project.Images != null)
                {
                    foreach (var img in project.Images)
                    {
                        ImageService.InsertImage(img);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DeleteProject(string projectId)
        {
            return ProjectService.DeleteProject(projectId);
        }

        public static bool DeleteProject(ProjectModel project)
        {
            return ProjectService.DeleteProject(project.ProjectId);
        }

        public static bool UpdateProject(ProjectModel project)
        {
            return ProjectService.UpdateProject(project);
        }

        public static List<ProjectModel> GetAllProjects()
        {
            return ProjectService.GetAllProjects();
        }

        public static ProjectModel GetProjectById(string Id)
        {
            return ProjectService.GetProjectById(Id);
        }
        public static DTOProjectDetails GetProjectById_Details(string Id)
        {
            return ProjectService.GetProjectById_Details(Id);
        }


        public static bool SetProjectArchived(ProjectModel project, bool bIsArchived)
        {
            return SetProjectArchived(project.ProjectId, bIsArchived);
        }

        public static bool SetProjectArchived(string projectId, bool bIsArchived = true)
        {
            return bIsArchived ? ProjectService.ArchiveProject(projectId) : ProjectService.UnArchiveProject(projectId);
        }

        public static bool IsValidProject(string projectId)
        {
            return ProjectService.IsValidProject(projectId);
        }

        public static string GetProjectName(string Id)
        {
            return ProjectService.GetProjectName(Id);
        }
    }
}
