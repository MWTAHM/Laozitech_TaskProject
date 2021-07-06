using BLL.Project;
using Core.Models;
using Core.TableModels;
using System;
using System.Web.UI;

namespace TaskProject.Pages.Task
{
    public partial class AddOrEdit : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                /* Server Side Validation */
                if (!ValidateForm(out DateTime start, out DateTime end))
                {
                    return;
                }

                var _TaskId = string.IsNullOrWhiteSpace(TaskId.Text) ? Guid.NewGuid().ToString() : TaskId.Text;
                var newTask = new TaskModel
                {
                    TaskId = _TaskId,
                    ParentProjectId = ParentId.Text,
                    TaskDescription = TaskDesc.Text,
                    TaskEndTime = end,
                    TaskName = TaskName.Text,
                    TaskStartTime = start
                };

                FillFilesAndImages(newTask);

                if (!string.IsNullOrWhiteSpace(TaskId.Text))
                {
                    TaskController.UpdateTask(newTask);
                }
                else
                {
                    TaskController.NewTask(newTask);
                }
                Response.Redirect($"/Pages/Projects/Details?projectId={newTask.ParentProjectId}"); // TODO: Return To Project
            }
            else if (Request.QueryString["ParentId"] != null)
            {
                TaskId.Text = Request.QueryString["Id"];
                TitleLabel.Text = TaskId.Text == null ? "New Project" : "Edit Project";

                if (ProjectController.IsValidProject(Request.QueryString["ParentId"]))
                {
                    ParentId.Text = Request.QueryString["ParentId"];
                }
                else
                {
                    Response.Redirect("~/");
                }

                var taskToEdit = TaskController.GetTaskById(TaskId.Text);
                if (taskToEdit != null)
                {
                    TaskName.Text = taskToEdit.TaskName;
                    TaskDesc.Text = taskToEdit.TaskDescription;
                    var e_strt = taskToEdit.TaskStartTime;
                    var e_end = taskToEdit.TaskEndTime;
                    StartDate.Text = e_strt.ToString("yyyy-MM-dd") + "T" + e_strt.ToString("hh:mm:ss.fff");
                    EndDate.Text = e_end.ToString("yyyy-MM-dd") + "T" + e_end.ToString("hh:mm:ss.fff");
                    StartDate.ReadOnly = true;
                }
            }
        }

        private void FillFilesAndImages(TaskModel newTask)
        {
            if (images.HasFiles)
            {
                foreach (var postedImage in images.PostedFiles)
                {
                    var img = new ImageModel
                    {
                        FullImageName = postedImage.FileName,
                        ImageAddingTime = DateTime.Now,
                        TaskId = newTask.TaskId
                    };
                    img.DataFromStream(postedImage.InputStream, postedImage.ContentLength);
                    newTask.Images.Add(img);
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
                        TaskId = newTask.TaskId
                    };
                    file.DataFromStream(postedFile.InputStream, postedFile.ContentLength);
                    newTask.Files.Add(file);
                }
            }
        }
        private bool ValidateForm(out DateTime start, out DateTime end)
        {
            bool IsValid = true;
            bool IsEditMode = !string.IsNullOrWhiteSpace(TaskId.Text);

            if (string.IsNullOrWhiteSpace(TaskId.Text))
            {
                TaskId.Text = null;
            }

            validationMessages.Text = "Check ";

            if (string.IsNullOrWhiteSpace(TaskName.Text))
            {
                validationMessages.Text += "Project Name, ";
                IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(ParentId.Text))
            {
                validationMessages.Text += "Form, ";
                IsValid = false;
            }

            start = default;
            end = default;
            try
            {
                start = DateTime.Parse(StartDate.Text);
            }
            catch (Exception)
            {
                validationMessages.Text += "Start Date, ";
                IsValid = false;
            }

            try
            {
                end = DateTime.Parse(EndDate.Text);
            }
            catch (Exception)
            {
                validationMessages.Text += "End Date, ";
                IsValid = false;
            }

            if ((!IsEditMode && start < DateTime.Now) || end < start)
            {
                validationMessages.Text += "Dates, ";
                IsValid = false;
            }

            if (!IsValid)
            {
                validationMessages.Text = validationMessages.Text.Remove(validationMessages.Text.LastIndexOf(','));
            }
            return IsValid;
        }
    }
}
