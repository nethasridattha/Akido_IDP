using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class ClientDTO
    {
        public int Id { get; set; }
        [StringLength(100)]

        [RegularExpression(@"^[0-9a-fA-F-]{36}$", ErrorMessage = "UUID must be a valid GUID.")]
        public string UUID { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z0-9._-]{3,100}$", ErrorMessage = "ClientId contains invalid characters.")]
        public string ClientId { get; set; }

        [Required]
        [StringLength(200)]
        [RegularExpression(@"^[A-Za-z0-9!@#$%^&*()_\-+=]{8,200}$", ErrorMessage = "ClientSecret format is invalid.")]
        public string ClientSecret { get; set; }

        [Required]
        [StringLength(150)]
        [RegularExpression(@"^[A-Za-z0-9\s_.-]{2,150}$", ErrorMessage = "ApplicationName contains invalid characters.")]
        public string ApplicationName { get; set; }

        [StringLength(150)]
        [RegularExpression(@"^[\u0600-\u06FF\sA-Za-z0-9_.-]{0,150}$", ErrorMessage = "ApplicationNameArabic contains invalid characters.")]
        public string ApplicationNameArabic { get; set; }

        [Required]
        [RegularExpression(@"^(Native|Single Page Application|Regular Web Application|Machine to Machine Application)$", ErrorMessage = "Invalid ApplicationType.")]
        public string ApplicationType { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9+\-.]*:\/\/[^\s]+$", ErrorMessage = "Invalid ApplicationUrl.")]
        public string ApplicationUrl { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9+\-.]*:\/\/[^\s]+$", ErrorMessage = "Invalid RedirectUri.")]
        public string RedirectUri { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9_,\-\.\s]{1,200}$", ErrorMessage = "GrantTypes format is invalid.")]
        public string GrantTypes { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9_,\-\.\:\s]{1,500}$", ErrorMessage = "Scopes format is invalid.")]
        public string Scopes { get; set; }

        [RegularExpression(@"^[A-Za-z][A-Za-z0-9+\-.]*:\/\/[^\s]+$", ErrorMessage = "Invalid logout url format.")]
        public string LogoutUri { get; set; }

        [RegularExpression(@"^[0-9a-fA-F-]{36}$", ErrorMessage = "OrganizationUid must be a valid GUID.")]
        public string OrganizationUid { get; set; }

        [Range(0, 1000)]
        public int AuthScheme { get; set; }

        [StringLength(5000)]
        public string PublicKeyCert { get; set; }

        [RegularExpression(@"^[A-Za-z0-9_,\-\.]{0,500}$", ErrorMessage = "Profiles must be comma separated values.")]
        public string Profiles { get; set; }

        [RegularExpression(@"^[A-Za-z0-9_,\-\.]{0,500}$", ErrorMessage = "Purposes must be comma separated values.")]
        public string Purposes { get; set; }
    }

    public class ClientsNewViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Application Type ")]
        public string ApplicationType { get; set; }

        [Required]
        [Display(Name = "Application Name ")]
        [MaxLength(50)]
        public string ApplicationName { get; set; }

        [Required]
        [Display(Name = "Application Name(Arabic) ")]
        [MaxLength(50)]
        public string ApplicationNameArabic { get; set; }

        [Display(Name = "Application Url")]
        //[Url]
        [Required]
        [RegularExpression(@"^(((http(s)?://)((((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))|localhost|((((?![-_0-9])[A-Za-z0-9]+[-_]?[A-Za-z]+)+[.])?((?![-_0-9])[A-Za-z0-9]+[-_]?[A-Za-z0-9]+)+([a-zA-Z]{0,30}[-_]?[a-zA-Z]{0,30})?([.][a-zA-Z]{2,30}[-_]?[a-zA-Z]{0,30}){0,10}?))(:([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5]))?(([/][a-zA-Z0-9._-]+)*[/]?)?)|((?![-_0-9])([a-zA-Z0-9_-]+)(.[a-zA-Z0-9_-]+)+?)(://)(([a-zA-Z0-9-_/=]+)))$", ErrorMessage = "Invalid Url")]
        public string ApplicationUri { get; set; }

        [Required]
        //[Url]
        [RegularExpression(@"^(((http(s)?://)((((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))|localhost|((((?![-_0-9])[A-Za-z0-9]+[-_]?[A-Za-z]+)+[.])?((?![-_0-9])[A-Za-z0-9]+[-_]?[A-Za-z0-9]+)+([a-zA-Z]{0,30}[-_]?[a-zA-Z]{0,30})?([.][a-zA-Z]{2,30}[-_]?[a-zA-Z]{0,30}){0,10}?))(:([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5]))?(([/][a-zA-Z0-9._-]+)*[/]?)?)|((?![-_0-9])([a-zA-Z0-9_-]+)(.[a-zA-Z0-9_-]+)+?)(://)(([a-zA-Z0-9-_/=]+)))$", ErrorMessage = "Invalid Url")]
        [Display(Name = "Redirect Url ")]
        public string RedirectUri { get; set; }

        public string GrantTypes { get; set; }

        public string Scopes { get; set; }

        //[Url]
        [RegularExpression(@"^(((http(s)?://)((((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))|localhost|((((?![-_0-9])[A-Za-z0-9]+[-_]?[A-Za-z]+)+[.])?((?![-_0-9])[A-Za-z0-9]+[-_]?[A-Za-z0-9]+)+([a-zA-Z]{0,30}[-_]?[a-zA-Z]{0,30})?([.][a-zA-Z]{2,30}[-_]?[a-zA-Z]{0,30}){0,10}?))(:([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5]))?(([/][a-zA-Z0-9._-]+)*[/]?)?)|((?![-_0-9])([a-zA-Z0-9_-]+)(.[a-zA-Z0-9_-]+)+?)(://)(([a-zA-Z0-9-_/=]+)))$", ErrorMessage = "Invalid Url")]
        [Display(Name = "Logout Url")]
        public string LogoutUri { get; set; }

        //[Required]
        [Display(Name = "Organisation Name")]
        public string OrganizationId { get; set; }


        [Display(Name = "Signing Certificate (.crt,.cer only)")]
        public IFormFile Cert { get; set; }
    }
}
