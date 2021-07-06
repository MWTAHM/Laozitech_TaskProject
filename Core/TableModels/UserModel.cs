using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    class UserModel
    {
        UserModel()
        {
            UserId = new Guid().ToString();
        }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string PasswordEncoded { get; set; }
        public string CurrentProjectId { get; set; }

    }
}
