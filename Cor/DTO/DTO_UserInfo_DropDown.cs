using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class DTO_UserInfo_DropDown
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool HasTask { get; set; }
    }
}
