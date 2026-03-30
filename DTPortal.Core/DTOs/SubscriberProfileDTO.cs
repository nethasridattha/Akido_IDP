using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class SubscriberProfileDTO
    {
        public string Idn { get; set; }
        public string FullnameEN { get; set; }
        public string FullnameAR { get; set; }
        public string FirstnameEN { get; set; }
        public string FirstnameAR { get; set; }
        public string LastnameEN { get; set; }
        public string LastnameAR { get; set; }
        public string NationalityEN { get; set; }
        public string NationalityAR { get; set; }
        public string Gender { get; set; }
        public string IdType { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfileType { get; set; }
        public string Suid { get; set; }
        public string Loa { get; set; }
        public string UnifiedId { get; set; }
        public string PassportNumber { get; set; }
    }
}
