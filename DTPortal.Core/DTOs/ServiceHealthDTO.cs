using System.Collections.Generic;

namespace DTPortal.Core.DTOs
{
    public class ServiceHealthDTO
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }

        public string Status { get; set; }

        public bool Clustered { get; set; }

        public string ServiceDescription { get; set; }

        public string Address { get; set; }

        public IEnumerable<ServiceMemberDTO> Members { get; set; }
    }
}
