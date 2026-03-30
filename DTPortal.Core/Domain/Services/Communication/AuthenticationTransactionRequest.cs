using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VaultSharp.Core;

namespace DTPortal.Core.Domain.Services.Communication
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class AuthenticationTransactionRequest
    {
        [Required]
        [StringLength(100,MinimumLength =1)]
        [RegularExpression(@"^[A-Za-z0-9-]+$",
            ErrorMessage = "SUID can contain only letters, numbers, and hyphen.")]
        public string suid { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than 0.")]
        public int pageNumber { get; set; }
    }
    public class AuthenticationTransactionResponse
    {
        public List<AuthenticationTransaction> authenticationTransactions { get; set; }

        public bool hasMoreResults { get; set; }

        public int totalResults { get; set; }
    }
    public class ApplicationInfo
    {
        public string ApplicationName { get; set; }
        public string ApplicationNameAr {  get; set; }
        public string OrganizationUid { get; set; }
    }

    public class AuthenticationTransaction
    {
        public string Id { get; set; }
        public string serviceProviderName { get; set; }
        public string serviceProviderAppName { get; set; }
        public string dateTime { get; set; }
        public string serviceName { get; set; }
        public string authenticationStatus { get; set; }
        public string activityType { get; set; }
    }
}
