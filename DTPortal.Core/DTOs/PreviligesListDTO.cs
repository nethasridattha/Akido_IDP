using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class PreviligeDetails
    {
        public int id { get; set; }
        public string organizationId { get; set; }
        public string organizationName { get; set; }
        public string privilege { get; set; }
        public string createdBy { get; set; }
        //public DateTime createdOn { get; set; }
        public string status { get; set; }
        public string modifiedBy { get; set; }
        //public DateTime modifiedOn { get; set; }
    }

}
