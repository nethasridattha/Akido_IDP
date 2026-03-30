using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class UserProfileDTO
    {
        public string birthDate { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string gender { get; set; }
        public string photo { get; set; }
        public string country { get; set; }
        public string subscriberCard {  get; set; }
        public string documentNumber { get; set; }
        public string documentType { get; set; }
    }
}
