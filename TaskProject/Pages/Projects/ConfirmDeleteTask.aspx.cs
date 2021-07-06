using BLL.Project;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            else if (Request.QueryString["Id"]!=null)
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