using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        public string name {  get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string status { get; set; }
    }
}
