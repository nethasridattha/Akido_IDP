using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.DTOs
{
    public class CreateUserDTO
    {
        public string Uuid { get; set; }
        public string FullName { get; set; }
        public string MailId { get; set; }
        public string MobileNo { get; set; }
        public DateTime? Dob { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
