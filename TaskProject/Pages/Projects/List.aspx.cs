using BLL.Project;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskProject.Pages.ProjectManager
{
    public partial class List : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var allProjects = ProjectController.GetAllProjects();
                var projects = allProjects.Where(x => !x.IsArchived);
                var archivedProjects = allProjects.Where(x => x.IsArchived);

                FillTable(projects);
                FillArchivedTable(archivedProjects);
            }
        }

        private void FillArchivedTable(IEnumerable<ProjectModel> archivedProjects)
        {
            foreach (var archived in archivedProjects)
            {
                var RowToAdd = new TableRow();
                RowToAdd.Cells.AddRange(
                    new TableCell[] {
                    new TableCell{Text = archived.ProjectName}, // Name
                    new TableCell{Text = archived.ProjectTotalBudget.ToString()}, // Budget
                    new TableCell{Text = archived.ProjectManagerName}, // Manager
                    new TableCell{Text = $"{archived.AchievedPercentage} %"}, // Achieved %
                    new TableCell{Text = $"<i class=\"clickable blue bi-archive\" onclick=\"UnArchive('{archived.ProjectId}')\"></i>"}  // Controls
                    }
                );
                ArchivedTable.Rows.Add(RowToAdd);
            }
        }
        private void FillTable(IEnumerable<ProjectModel> projects)
        {
            var headers = new string[] { "Name", "Description", "Times", "Budget", "Website", "Manager", "Company", "Achieved %", "Controls" };
            StringBuilder html = new StringBuilder();

            html.Append("<table id=\"pagination\" class=\"table table-hover\" >");
            html.Append("<thead scope=\"col\">");
            html.Append("<tr>");
            foreach (var header in headers)
            {
                html.Append("<th>");
                html.Append(header);
                html.Append("</th>");
            }
            html.Append("</tr></thead>");
            html.Append("<tbody>");
            foreach (var project in projects)
            {
                html.Append($"<tr>");
                html.Append($"<td>{project.ProjectName}</td>");
                html.Append($"<td>{project.ProjectDescription}</td>");
                html.Append($"<td>Start: {project.ProjectStartTime:dd-MM-yyyy}<br />End: {project.ProjectEndTime:dd-MM-yyyy}</td>"); // Times
                html.Append($"<td>{project.ProjectTotalBudget}</td>");
                html.Append($"<td>{project.ProjectWebsite}</td>");
                html.Append($"<td>{project.ProjectManagerName}</td>");
                html.Append($"<td>{project.CompanyName}</td>");
                html.Append($"<td>{project.AchievedPercentage}%</td>");
                html.Append($"<td>");
                html.Append($"<i class=\"red clickable bi-trash\" onclick=\"Delete('{project.ProjectId}')\"></i>");
                html.Append($"<i class=\"blue clickable bi-pencil-square\" onclick=\"window.location.href= 'AddOrEdit?Id={project.ProjectId}'\"></i>");
                html.Append($"<i class=\"blue clickable bi-list\" onclick=\"window.location.href='ProjectTasks?projectId={project.ProjectId}'\"></i>");
                html.Append($"<i class=\"red clickable bi-archive-fill\" onclick=\"Archive('{project.ProjectId}')\"></i>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            //html.Append("</tbody>");
            html.Append("</table>");
            ProjectsTable.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
}