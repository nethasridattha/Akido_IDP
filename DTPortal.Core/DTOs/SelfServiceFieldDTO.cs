using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class SelfServiceFieldDTO
    {
        public string fieldName { get; set; }
        public string labelName { get; set; }
        public int fieldId { get; set; }
        public bool visibility { get; set; }
        public bool mandatory { get; set; }

    }
}
