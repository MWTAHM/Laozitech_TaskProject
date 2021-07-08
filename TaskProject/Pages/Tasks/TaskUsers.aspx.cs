using BLL.Project;
using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskProject.Pages.Tasks
{
    public partial class TaskUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !string.IsNullOrWhiteSpace(Request.QueryString["TaskId"]))
            {
                List<DTO_TaskUserInfo> TaskUsers = TaskController.GetUsersWorkingOnTask(Request.QueryString["TaskId"]);
                TitleLabel.Text = $"Users Working On {TaskController.GetTaskName(Request.QueryString["TaskId"]) ?? "Task"}";
                FillUsersTable(TaskUsers, Request.QueryString["TaskId"]);
            }
        }

        private void FillUsersTable(List<DTO_TaskUserInfo> users, string TaskId)
        {
            foreach (var user in users)
            {
                var RowToAdd = new TableRow();
                RowToAdd.Cells.AddRange(
                    new TableCell[] {
                    new TableCell{Text = user.UserName}, // UserName
                    new TableCell{Text = user.FullName}, // FullName
                    new TableCell{Text = user.Email}, // Email
                    new TableCell{Text = user.Country}, // Country
                    }
                );
                UsersTable.Rows.Add(RowToAdd);
            }
        }
    }
}