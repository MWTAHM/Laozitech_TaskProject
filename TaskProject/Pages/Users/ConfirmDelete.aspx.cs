using BLL.Project;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                DeleteText.Text = $"Delete User {userName}?";
                Id.Text = Request.QueryString["Id"];
                return;
            }
            Response.Redirect("List");
        }
    }
}