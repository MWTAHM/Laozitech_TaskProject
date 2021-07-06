using Core.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserModel
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public DateTime? BirthDate { get; set; }
        public DateTime UserRegistratonTime { get; set; }
        public string PasswordEncoded { get; set; }
        public string CurrentProjectId { get; set; }
        public string CurrentTaskId { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Postal { get; set; }

        public List<FileModel> Files { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
