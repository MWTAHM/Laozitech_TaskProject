using BLL.Project;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskProject.Pages.Tasks
{
    public partial class TaskAssign : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                var taskName = TaskController.GetTaskName(Request.QueryString["Id"]);
                taskText.Text = $"Assign Task {taskName} To: ";
                Id.Text = Request.QueryString["Id"];

                // The task can be assigned to the users that only have no tasks
                var NoTaskUsers = UserController.GetAllUsersDropDownInfo().Where(x => !x.HasTask);
                if (NoTaskUsers != null && NoTaskUsers.Any())
                {
                    foreach (var user in NoTaskUsers)
                    {
                        Users.Items.Add(new ListItem { Text = user.FullName, Value = user.UserId });
                    }
                }
                else
                {
                    Placeholder.Controls.Clear();
                    Placeholder.Controls.Add(new Label { Text = "There is no user with no task", CssClass="text-danger" });
                }
            }
        }

        protected void Confirme_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Id.Text) && Users.SelectedIndex > -1)
            {
                TaskController.AssignTaskToUser(Id.Text, Users.SelectedItem.Value);
                var ProjectId = ProjectController.GetProjectIdFromTaskId(Id.Text);
                Response.Redirect($"ProjectTasks?projectId={ProjectId}");
            }
        }
    }
}