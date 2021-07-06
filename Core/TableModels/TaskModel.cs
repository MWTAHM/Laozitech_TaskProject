using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TableModels
{
    class TaskModel
    {
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskStartTime { get; set; }
        public DateTime DueTime { get; set; }
        public string ProjectId { get; set; }

    }
}
