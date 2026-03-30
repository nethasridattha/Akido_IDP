using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public  class SelfOrganizationNewDTO
    {
        public int OrgDetailsId { get; set; }
        public string CreatedBy { get; set; }
        public string OrgName { get; set; }
        public string Ouid { get; set; }
        public string OrgNo { get; set; }
        public string RegNo { get; set; }

        public string OrgType { get; set; }
        public string TaxNumber { get; set; }

        public string Status { get; set; }
        public string SpocName { get; set; }
        public string SpocOfficialEmail { get; set; }

        public string SpocDocumentNumber { get; set; }

        public string AuditorName { get; set; }

        public string AuditorDocumentNumber { get; set; }

        public string AuditorOfficialEmail { get; set; }
        public string CreatedOn { get; set; }
        public string Address { get; set; }
        public string OrgEmail { get; set; }
        public List<OrganizationDocumentDto> Documents { get; set; }

    }
}
