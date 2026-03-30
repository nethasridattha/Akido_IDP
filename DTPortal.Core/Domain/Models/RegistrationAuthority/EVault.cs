using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class EVault
    {
        public int Id { get; set; }
        public string Suid { get; set; }
        public int? DocId { get; set; }
    }
}
