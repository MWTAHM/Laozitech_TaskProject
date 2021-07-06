using BLL.Project;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskProject.Pages.Projects
{
    public partial class ConfirmArchive : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack && !string.IsNullOrWhiteSpace(Id.Text))
            {
                ProjectController.SetProjectArchived(Id.Text);
                Response.Redirect("List");
            }
            else if (Request.QueryString["Id"] != null)
            {
                var projectName = ProjectController.GetProjectName(Request.QueryString["Id"]);
                DeleteText.Text = $"Archvie Project {projectName}?";
                Id.Text = Request.QueryString["Id"];
                return;
            }
            Response.Redirect("List");
        }
    }
}