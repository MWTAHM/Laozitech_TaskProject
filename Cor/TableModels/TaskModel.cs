using Core.TableModels;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class TaskModel
    {
        public TaskModel()
        {
            TaskId = Guid.NewGuid().ToString();
            Files = new List<FileModel>();
            Images = new List<ImageModel>();
        }

        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskStartTime { get; set; }
        public DateTime TaskEndTime { get; set; }
        public string ParentProjectId { get; set; }
        public DateTime? TaskLastEdit { get; set; }

        public List<FileModel> Files { get; set; }
        public List<ImageModel> Images { get; set; }

        public TimeSpan TimeLeft
            => TaskEndTime - TaskStartTime;

    }
}
