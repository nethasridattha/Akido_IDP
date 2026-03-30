using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class QrCredentialDTO
    {
        public int Id { get; set; }

        public string credentialName { get; set; }
        public string displayName { get; set; }

        public string credentialUId { get; set; }

        public string remarks { get; set; }

        public QrAttributesDTO dataAttributes { get; set; }

        public string organizationId { get; set; }

        public string credentialOffer { get; set; }

        public DateTime createdDate { get; set; }

        public bool portraitVerificationRequired {  get; set; }

        public string status { get; set; }
    }
    public class QrAttributesDTO
    {
        public List<DataAttributesDTO> publicAttributes { get; set; }
        public List<DataAttributesDTO> privateAttributes { get; set; }
    }
}
