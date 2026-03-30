using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Lookups
{
    public class UserLookupItem : LookupItem
    {
        public string Suid { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Status { get; set; }
        public string DeviceToken { get; set; }
        public string DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string NationalId { get; set; }
        public string Nationality { get; set; }
        public string LanguagePreference { get; set; }
        public IList<LoginProfile> LoginProfile { get; set; }

    }
}
