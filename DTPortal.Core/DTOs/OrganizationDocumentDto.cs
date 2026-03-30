using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class OrganizationDocumentDto
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentData { get; set; }//Base 64
    }
}
