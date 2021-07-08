using BLL.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskProject.Pages.Users
{
    public partial class List : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var allUsers = UserController.GetAllUsers();
                FillUsersTable(allUsers);
            }
        }

        private void FillUsersTable(List<Core.Models.UserModel> allUsers)
        {
            var headers = new string[] { "Full Name", "UserName", "Email", "Created At", "Country", "Task", "Controls" };
            StringBuilder html = new StringBuilder();

            html.Append("<table id=\"UsersTable\" class=\"table table-hover\" ><thead scope=\"col\">");
            html.Append("<tr>");
            foreach (var header in headers)
            {
                html.Append("<th>");
                html.Append(header);
                html.Append("</th>");
            }
            html.Append("</tr></thead>");

            foreach (var user in allUsers)
            {
                html.Append($"<tr>");
                html.Append($"<td>{user.UserName}</td>");
                html.Append($"<td>{user.FullName}</td>");
                html.Append($"<td>{user.EmailAddress}</td>");
                html.Append($"<td>{user.UserRegistratonTime:dd-MM-yyyy}</td>"); // Times
                html.Append($"<td>{user.Country}</td>");
                html.Append($"<td>{TaskController.GetTaskName(user.CurrentTaskId)}</td>");
                html.Append($"<td>");
                html.Append($"<i title=\"Delete\" class=\"red clickable bi-trash\" onclick=\"Delete('{user.UserId}')\"></i>");
                html.Append($"<i title=\"UnAssign Task From User\" class=\"red clickable bi-file-earmark-x\" onclick=\"UnAssignTask('{user.CurrentTaskId}','{user.UserName}','{user.UserId}')\"></i>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            html.Append("</table>");
            ProjectsTable.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
}