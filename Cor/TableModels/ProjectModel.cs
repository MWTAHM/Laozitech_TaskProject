using Core.TableModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
   public class ProjectModel
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime ProjectStartTime { get; set; }
        public DateTime ProjectEndTime { get; set; }
        public DateTime? ProjectLastEdit { get; set; }
        public double ProjectTotalBudget { get; set; }
        public double ProjectSpentBudget { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLocation { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebsite { get; set; }
        public string ProjectManagerId { get; set; }
        public string ProjectWebsite { get; set; }
        public int AchievedPercentage { get; set; }
        public string ProjectManagerName { get; set; }
        public bool IsArchived { get; set; }

        public List<FileModel> Files { get; set; }
        public List<ImageModel> Images { get; set; }

        public double SpentBudgetPercentage => ProjectSpentBudget / ProjectTotalBudget * 100;
        public double LeftBudget => ProjectTotalBudget - ProjectSpentBudget;
        public double LeftBudgetPercentage => LeftBudget / ProjectTotalBudget * 100;
        public TimeSpan TimeLeft  => ProjectEndTime - ProjectStartTime;
    }
}
