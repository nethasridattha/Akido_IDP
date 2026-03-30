using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class CreateEConsentClientDetailsDTO
    {
        public int ClientId { get; set; }

        public string Scopes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string Status { get; set; }

        public string Purposes { get; set; }
    }
}
