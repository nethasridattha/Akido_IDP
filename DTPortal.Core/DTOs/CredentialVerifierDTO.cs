using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class CredentialVerifierDTO
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string credentialId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string organizationId { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string credentialName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string organizationName { get; set; } = string.Empty;

        public List<DataAttributes> attributes { get; set; } = new();

        public List<CredentialConfig> configuration { get; set; } = new();

        public List<string> emails { get; set; } = new();

        [Required]
        [StringLength(20)]
        public string status { get; set; } = string.Empty;

        [Required]
        public DomainConfig domainConfig { get; set; } = new();

        [Range(1, 3650, ErrorMessage = "Validity must be between 1 and 3650 days.")]
        public int validity { get; set; }

        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }

        [StringLength(500)]
        public string remarks { get; set; } = string.Empty;
    }

    public class DataAttributes
    {
        [Required]
        [StringLength(200)]
        public string displayName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string attribute { get; set; } = string.Empty;

        [Range(1, 10)]
        public int dataType { get; set; }

        public bool mandatory { get; set; }
    }

    public class CredentialConfig
    {
        [Required]
        [StringLength(50)]
        public string format { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string bindingMethod { get; set; } = string.Empty;

        [StringLength(100)]
        public string supportedMethod { get; set; } = string.Empty;
    }

    public class DomainConfig
    {
        [Required]
        public string domain { get; set; } = string.Empty;

        public List<string> purposesList { get; set; } = new();
    }
}
