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
                FillTable(allUsers);
            }
        }

        private void FillTable(List<Core.Models.UserModel> allUsers)
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

            foreach (var users in allUsers)
            {
                html.Append($"<tr>");
                html.Append($"<td>{users.UserName}</td>");
                html.Append($"<td>{users.FullName}</td>");
                html.Append($"<td>{users.EmailAddress}</td>");
                html.Append($"<td>{users.UserRegistratonTime:dd-MM-yyyy}</td>"); // Times
                html.Append($"<td>{users.Country}</td>");
                html.Append($"<td>{TaskController.GetTaskName(users.CurrentTaskId)}</td>");
                html.Append($"<td>");
                html.Append($"<i class=\"red clickable bi-trash\" onclick=\"Delete('{users.UserId}')\"></i>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            html.Append("</table>");
            ProjectsTable.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
}