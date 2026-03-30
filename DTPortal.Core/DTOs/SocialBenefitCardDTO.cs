using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class SocialBenefitCardDTO
    {
        public string address { get; set; }
        public string suid { get; set; }
        public string age { get; set; }
        public string dateofBirth { get; set; }
        public string email { get; set; }
        public double income { get; set; }
        public string idDocNumber { get; set; }
        public string name { get; set; }
        public string mobileNumber { get; set; }
        public string occupation { get; set; }
        public string martialStatus { get; set; }
        public string programs { get; set; }
        public string photo { get; set; }
    }
}
