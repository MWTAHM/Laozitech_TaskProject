using BLL.Project;
using Core.Models;
using Core.TableModels;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskProject.Pages.ProjectManager
{
    public partial class AddOrEdit : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ManagerId = "";
            if (IsPostBack)
            {
                if (!ValidateForm(out double budget, out DateTime start, out DateTime end))
                {
                    return;
                }

                var ProjectId = string.IsNullOrWhiteSpace(PId.Text) ? Guid.NewGuid().ToString() : PId.Text;
                var newProject = new ProjectModel
                {
                    ProjectId = ProjectId,
                    AchievedPercentage = 0,
                    CompanyEmail = PCompanyEmail.Text,
                    CompanyLocation = PCompanyLocation.Text,
                    CompanyName = PCompanyName.Text,
                    CompanyPhone = PCompanyPhone.Text,
                    CompanyWebsite = PCompanyWebsite.Text,
                    ProjectDescription = PDescription.Text,
                    ProjectName = ProjectName.Text,
                    ProjectTotalBudget = budget,
                    ProjectWebsite = PWebsite.Text,
                    ProjectEndTime = end,
                    ProjectStartTime = start,
                    ProjectManagerId = UsersList.SelectedItem.Value,
                    ProjectManagerName = UsersList.SelectedItem.Text,
                    Images = new List<ImageModel>(),
                    Files = new List<FileModel>()
                };

                AddImagesToProject(newProject);

                if (!string.IsNullOrWhiteSpace(PId.Text))
                {
                    ProjectController.UpdateProject(newProject);
                }
                else
                {
                    ProjectController.NewProject(newProject);
                }
                Response.Redirect("List");
            }
            else if (Request.QueryString["Id"] != null)
            {
                EditProject(out ManagerId);
            }
            FillUsersList(ManagerId);
        }

        private void FillUsersList(string ManagerId)
        {
            var AllUsers = UserController.GetAllUsersDropDownInfo();
            foreach (var user in AllUsers)
            {
                UsersList.Items.Add(new ListItem { Text = user.FullName, Value = user.UserId, Selected = ManagerId == user.UserId });
            }
        }
        private void EditProject(out string ManagerId)
        {
            ManagerId = "";
            PId.Text = Request.QueryString["Id"];
            TitleLabel.Text = Request.QueryString["Id"] == null ? "New Project" : "Edit Project";

            var projectToEdit = ProjectController.GetProjectById(PId.Text);
            if (projectToEdit != null)
            {
                ProjectName.Text = projectToEdit.ProjectName;
                PDescription.Text = projectToEdit.ProjectDescription;
                var e_strt = projectToEdit.ProjectStartTime; // 2014-01-02T11:42:13.510
                var e_end = projectToEdit.ProjectEndTime;
                StartDate.Text = e_strt.ToString("yyyy-MM-dd") + "T" + e_strt.ToString("hh:mm:ss.fff");
                EndDate.Text = e_end.ToString("yyyy-MM-dd") + "T" + e_end.ToString("hh:mm:ss.fff");
                PBudget.Text = projectToEdit.ProjectTotalBudget.ToString();
                PCompanyName.Text = projectToEdit.CompanyName;
                PCompanyLocation.Text = projectToEdit.CompanyLocation;
                PCompanyPhone.Text = projectToEdit.CompanyPhone;
                PCompanyEmail.Text = projectToEdit.CompanyEmail;
                PCompanyWebsite.Text = projectToEdit.CompanyWebsite;
                UsersList.SelectedValue = projectToEdit.ProjectManagerName;
                ManagerId = projectToEdit.ProjectManagerId;
                PWebsite.Text = projectToEdit.ProjectWebsite;
                StartDate.ReadOnly = true;
            }
        }
        private bool ValidateForm(out double budget, out DateTime start, out DateTime end)
        {
            /* Server Side Validation */
            bool IsValid = true;
            budget = 0;
            start = default;
            end = default;

            validationMessages.Text = "Check ";

            if (string.IsNullOrWhiteSpace(ProjectName.Text))
            {
                validationMessages.Text += "Project Name, ";
                IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(PCompanyName.Text))
            {
                validationMessages.Text += "Company Name, ";
                IsValid = false;
            }

            try
            {
                budget = double.Parse(PBudget.Text);
            }
            catch (Exception)
            {
                validationMessages.Text += "Budget,";
                IsValid = false;
            }

            try
            {
                start = DateTime.Parse(StartDate.Text);
            }
            catch (Exception)
            {
                validationMessages.Text += "Start Date,";
                IsValid = false;
            }

            try
            {
                end = DateTime.Parse(EndDate.Text);
            }
            catch (Exception)
            {
                validationMessages.Text += "End Date,";
                IsValid = false;
            }

            if (start < DateTime.Now || end < start)
            {
                validationMessages.Text += "Dates,";
                IsValid = false;
            }


            if (UsersList.SelectedIndex < 0)
            {
                validationMessages.Text += "Manager,";
                IsValid = false;
            }

            if (!IsValid)
            {
                validationMessages.Text = validationMessages.Text.Remove(validationMessages.Text.LastIndexOf(','));
                return false;
            }
            return true;
        }
        private void AddImagesToProject(ProjectModel project)
        {
            if (images.HasFiles)
            {
                foreach (var postedImage in images.PostedFiles)
                {
                    var img = new ImageModel
                    {
                        FullImageName = postedImage.FileName,
                        ImageAddingTime = DateTime.Now,
                        ProjectId = project.ProjectId
                    };
                    img.DataFromStream(postedImage.InputStream, postedImage.ContentLength);
                    project.Images.Add(img);
                }
            }

            if (files.HasFiles)
            {
                foreach (var postedFile in files.PostedFiles)
                {
                    var file = new FileModel
                    {
                        FullFileName = postedFile.FileName,
                        FileAddingTime = DateTime.Now,
                        ProjectId = project.ProjectId
                    };
                    file.DataFromStream(postedFile.InputStream, postedFile.ContentLength);
                    project.Files.Add(file);
                }
            }
        }

    }
}