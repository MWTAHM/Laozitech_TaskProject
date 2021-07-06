using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using DAL.Project;
using DAL.Task;

namespace BLL.Project
{
    public class TaskController
    {
        public static bool NewTask(TaskModel task)
        {
            if (TaskService.InsertTask(task))
            {
                if (task.Files != null)
                {
                    foreach (var file in task.Files)
                    {
                        FileService.InsertFile(file);
                    }
                }
                if (task.Images != null)
                {
                    foreach (var img in task.Images)
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

        public static bool DeleteTask(string projectId)
        {
            return TaskService.DeleteTask(projectId);
        }

        public static bool DeleteTask(TaskModel project)
        {
            return TaskService.DeleteTask(project.TaskId);
        }

        public static bool UpdateTask(TaskModel project)
        {
            return TaskService.UpdateTask(project);
        }

        public static List<TaskModel> GetAllTasks()
        {
            return TaskService.GetAllTasks();
        }

        public static TaskModel GetTaskById(string Id)
        {
            return TaskService.GetTaskById(Id);
        }

        public static List<TaskModel> GetProjectTasks(string projectId)
        {
            return TaskService.GetProjectTasks(projectId);
        }

        public static List<TaskModel> GetProjectTasks(ProjectModel project)
        {
            return TaskService.GetProjectTasks(project.ProjectId);
        }

        public static bool AssignTaskToUser(string TaskId, string UserId)
        {
            return TaskService.AssignTaskToUser(TaskId, UserId);
        }

        public static string GetTaskName(string taskId)
        {
            return string.IsNullOrWhiteSpace(taskId)?"No Task":TaskService.GetTaskName(taskId);
        }
    }
}
