using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TableModels
{
    class TaskAssignModel
    {
        public string TaskAssignId { get; set; }
        public string TaskId { get; set; }
        public string UserId { get; set; }

        public bool IsValid 
            => TaskId != null && UserId != null;
    }
}

