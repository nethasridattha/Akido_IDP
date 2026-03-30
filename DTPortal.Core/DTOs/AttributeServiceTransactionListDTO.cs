using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class AttributeServiceTransactionListDTO
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public string ClientName { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string RequestProfile { get; set; }
    }
}
