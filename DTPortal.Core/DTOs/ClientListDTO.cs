using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class ClientListDTO
    {
        public int Id { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationType { get; set; }

        public string ApplicationUri { get; set; }

        public string ClientId { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
