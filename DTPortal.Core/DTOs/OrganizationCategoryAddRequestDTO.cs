using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class OrganizationCategoryAddRequestDTO
    {
        public string OrgCategoryName { get; set; }
        public List<OrganisationFieldAddDto> OrganisationFieldDtos { get; set; }
    }
    public class OrganisationFieldAddDto
    {
        public string fieldName { get; set; }
        // string? labelName { get; set; }
        public int fieldId { get; set; }
        public bool visibility { get; set; }
        public bool mandatory { get; set; }

    }

}



