using BLL.Project;
using System;
using System.Web.UI;

namespace TaskProject.Pages.Users
{
    public partial class ConfirmDelete : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && !string.IsNullOrWhiteSpace(Id.Text))
            {
                UserController.DeleteUser(Id.Text);
                Response.Redirect("List");
            }
            else if (Request.QueryString["Id"] != null)
            {
                var userName = UserController.GetUserName(Request.QueryString["Id"]);
                if(userName != null)
                {
                    DeleteText.Text = $"Delete User {userName}?";
                    Id.Text = Request.QueryString["Id"];
                    return;
                }
            }
            Response.Redirect("List");
        }
    }
}