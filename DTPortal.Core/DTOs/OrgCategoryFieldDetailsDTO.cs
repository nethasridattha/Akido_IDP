using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class OrgCategoryFieldDetailsDTO
    {
        public string OrgCategoryName { get; set; }
        public int OrgCategoryId { get; set; }

        public List<SelfServiceFieldDTO> organisationFieldDtos { get; set; }
    }
}
