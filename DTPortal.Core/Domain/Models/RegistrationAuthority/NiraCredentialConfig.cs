using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class NiraCredentialConfig
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? NoOfDaysLeft { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
