using BLL.Project;
using System;
using System.Web.UI;

namespace TaskProject.Pages.Projects
{
    public partial class ConfirmDelete : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && !string.IsNullOrWhiteSpace(Id.Text))
            {
                ProjectController.DeleteProject(Id.Text);
                Response.Redirect("List");
            }
            else if (Request.QueryString["Id"] != null)
            {
                var projectName = ProjectController.GetProjectName(Request.QueryString["Id"]);
                if(projectName != null)
                {
                    DeleteText.Text = $"Delete Project {projectName}?";
                    Id.Text = Request.QueryString["Id"];
                    return;
                }
            }
            Response.Redirect("List");
        }
    }
}