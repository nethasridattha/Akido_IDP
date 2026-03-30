using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class NiraPassword
    {
        public int Id { get; set; }
        public string EncryptedUsername { get; set; }
        public string EncryptedPassword { get; set; }
        public int? NoOfDaysLeft { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
