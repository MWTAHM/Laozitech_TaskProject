using BLL.Project;
using System;
using System.Web.UI.WebControls;

namespace TaskProject.Pages.Projects
{
    public partial class ProjectTasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string projectId = Request.QueryString["projectId"];
                var project = ProjectController.GetProjectById_Details(projectId);
                if (project != null)
                {
                    ProjectIdTextBox.Text = projectId;
                    TitleLabel.Text = $"{project.ProjectName} Tasks";
                }

                var projectTasks = TaskController.GetProjectTasks(projectId);
                if (projectTasks != null)
                {
                    foreach (var item in projectTasks)
                    {
                        var taskRow = new TableRow();
                        taskRow.Cells.Add(new TableCell() { Text = item.TaskName });
                        taskRow.Cells.Add(new TableCell() { Text = item.TaskDescription });
                        taskRow.Cells.Add(new TableCell() { Text = $"Start Time:{item.TaskStartTime:dd-MM-yyyy}<br />End Time:{item.TaskEndTime:dd-MM-yyyy}" });
                        taskRow.Cells.Add(new TableCell()
                        {
                            Text = $"<i title=\"Delete\" class=\"clickable red bi-trash\" onclick=\"DeleteTask('{item.TaskId}')\"></i>" +
                                   $"<i title=\"Edit\" class=\"clickable blue bi-pencil-fill\" onclick=\"window.location.href='/Pages/Tasks/AddOrEdit?Id={item.TaskId}&ParentId={item.ParentProjectId}'\"></i>" +
                                   $"<i title=\"Assign Task To User\" onclick=\"AssignTask('{item.TaskId}')\" class=\"clickable blue bi-arrow-right-short\"></i>" +
                                   $"<i title=\"Show Users Working On Task\" onclick=\"window.location.href='/Pages/Tasks/TaskUsers?TaskId={item.TaskId}'\" class=\"clickable blue bi-people-fill\"></i>"
                        });
                        Tasks.Rows.Add(taskRow);
                    }
                }
            }
        }

        protected void NewTask(object sender, EventArgs e)
        {
            Response.Redirect($"~/Pages/Tasks/AddOrEdit?ParentId={ProjectIdTextBox.Text}");
        }
    }
}