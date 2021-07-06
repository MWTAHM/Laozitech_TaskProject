using BLL.Project;
using System;

namespace TaskProject.Pages.Tasks
{
    public partial class ConfirmDeleteTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && !string.IsNullOrWhiteSpace(Id.Text))
            {
                TaskController.DeleteTask(Id.Text);
                Response.Redirect("List");
            }
            else if (Request.QueryString["Id"] != null)
            {
                var taskName = TaskController.GetTaskName(Request.QueryString["Id"]);
                if (taskName != null)
                {
                    DeleteText.Text = $"Delete Task {taskName}?";
                    Id.Text = Request.QueryString["Id"];
                }
                return;
            }
            Response.Redirect("List");
        }
    }
}