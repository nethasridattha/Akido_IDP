using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class TrustedSpocListDTO
    {
        public int Id { get; set; }
        public string SpocName { get; set; }
        public string SpocSuid { get; set; }
        public string MobileNumber { get; set; }
        public string IdDocumentNo { get; set; }
        public string SpocEmail { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime InvitedOn { get; set; }
        public bool ReInvite { get; set; }
    }
}
