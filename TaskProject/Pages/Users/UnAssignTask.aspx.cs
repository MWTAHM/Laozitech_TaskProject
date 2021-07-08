using BLL.Project;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskProject.Pages.Users
{
    public partial class TaskUnAssign : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["TaskId"]) 
                && !string.IsNullOrWhiteSpace(Request.QueryString["username"]) 
                && !string.IsNullOrWhiteSpace(Request.QueryString["UserId"]))
            {
                var taskName = TaskController.GetTaskName(Request.QueryString["TaskId"]);
                taskText.Text = $"UnAssign Task {taskName} From {Request.QueryString["username"]}";
                TaskId.Text = Request.QueryString["TaskId"];
                UserId.Text = Request.QueryString["UserId"];
            }
            else
            {
                MainPlaceholder.Controls.Clear();
                MainPlaceholder.Controls.Add(new Label { Text = "This User Has No Task", CssClass = "text-danger" });
            }
        }

        protected void Confirme_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskId.Text))
            {
                TaskController.UnAssignTaskFromUser(UserId.Text);
                Response.Redirect($"List");
            }
        }
    }
}