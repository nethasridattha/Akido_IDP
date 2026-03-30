using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class UserInfoDTO
    {
        public string suid { get; set; }
        public string birthdate { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string id_document_type { get; set; }
        public string id_document_number { get; set; }
        public string loa { get; set; }
        public string country { get; set; }
        public string login_type { get; set; }

        public string FullNameEn { get; set; }
        public string DateOfBirth { get; set; }
        public string CurrentNationality { get; set; }
        public string GenderEn { get; set; }
        public string OccupationEn { get; set; }
        public string EmiratesIdNumber { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string PassportType { get; set; }
        public string PassportNo { get; set; }
        public string DocumentNationalityAbbr { get; set; }
        public string DocumentNationality { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpiryDate { get; set; }
    }
}
