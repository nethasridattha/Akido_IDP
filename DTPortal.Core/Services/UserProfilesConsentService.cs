using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DTPortal.Core.Services
{
    public class UserProfilesConsentService : IUserProfilesConsentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserProfilesConsentService> _logger;
        private readonly IClientService _clientService;
        private readonly IUserClaimService _userClaimService;
        private readonly IScopeService _scopeService;

        public UserProfilesConsentService(IUnitOfWork unitOfWork,ILogger<UserProfilesConsentService> logger, IClientService clientService,
            IScopeService scopeService,
            IPurposeService purposeService, IUserClaimService userClaimService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _clientService = clientService;
            _scopeService = scopeService;
            _userClaimService = userClaimService;
        }

        public async Task<ServiceResult> GetUserProfilesConsentbySuidAsync(string suid)
        {
            try
            {
                var consent = await _unitOfWork.UserProfilesConsent.GetUserProfilesConsentBySuidAsync(suid);
                if (consent == null)
                {
                    return new ServiceResult(false, "Internal Error");
                }

                if (consent.Count() == 0)
                {
                    return new ServiceResult(true, $"No Consents Found", consent);
                }

                List<UserProfilesConsentDTO> consentDTOs = new List<UserProfilesConsentDTO>();

                Dictionary<string, string> attributesDictionary = new Dictionary<string, string>();

                var attributesList = await _userClaimService.ListUserClaimAsync();
                foreach (var attributes in attributesList)
                {
                    attributesDictionary[attributes.Name] = attributes.DisplayName;
                }

                var dict = new Dictionary<string, string>();

                var profiles = await _scopeService.ListScopeAsync();

                foreach (var p in profiles)
                {
                    var id = p.Id.ToString();
                    dict[id] = p.Name;
                }

                foreach (var c in consent)
                {
                    var clientName = await GetClientNameByClientID(c.ClientId);

                    var dto = new UserProfilesConsentDTO
                    {
                        Suid = c.Suid,
                        ClientId = c.ClientId,
                        ClientName = clientName,
                        Profile = dict[c.Profile],
                        Attributes = string.IsNullOrEmpty(c.Attributes)
                                    ? new List<string>()
                                    : JsonConvert.DeserializeObject<List<Attributes>>(c.Attributes)
                                        .Select(attribute => attributesDictionary[attribute.name])
                                        .ToList(),
                        CreatedDate = c.CreatedDate,
                        ModifiedDate = c.ModifiedDate,
                        Status = c.Status
                    };

                    consentDTOs.Add(dto);
                }

                return new ServiceResult(true, "Successfully retrieved Consent", consentDTOs);
            }
            catch(Exception ex)
            {
                _logger.LogError("Get User Profiles Consent by SUID failed : {0}", ex.Message);
                return new ServiceResult(false, "An error occurred while retrieving the user profiles consent." +
                    " Please contact the admin.");
            }
        }

        public async Task<ServiceResult> GetUserProfilesConsentByClientNameAsync(string suid, string applicationName)
        {
            try
            {
                var client = await _clientService.GetClientByAppNameAsync(applicationName);

                if (client == null)
                {
                    return new ServiceResult(false, $"Consent Not Found");
                }

                var consent = await _unitOfWork.UserProfilesConsent.GetUserProfilesConsentByIdAsync(suid, client.ClientId);
                if (consent == null || consent.Count() == 0)
                {
                    return new ServiceResult(false, $"Consent Not Found");
                }

                List<UserProfilesConsentDTO> consentDTOs = new List<UserProfilesConsentDTO>();

                Dictionary<string, string> attributesDictionary = new Dictionary<string, string>();

                var attributesList = await _userClaimService.ListUserClaimAsync();
                foreach (var attributes in attributesList)
                {
                    attributesDictionary[attributes.Name] = attributes.DisplayName;
                }

                var dict = new Dictionary<string, string>();

                var profiles = await _scopeService.ListScopeAsync();

                foreach (var p in profiles)
                {
                    var id = p.Id.ToString();
                    dict[id] = p.Name;
                }

                foreach (var c in consent)
                {
                    var clientName = await GetClientNameByClientID(c.ClientId);

                    var dto = new UserProfilesConsentDTO
                    {
                        Suid = c.Suid,
                        ClientId = c.ClientId,
                        ClientName = clientName,
                        Profile = dict[c.Profile],
                        Attributes = string.IsNullOrEmpty(c.Attributes)
                                    ? new List<string>()
                                    : JsonConvert.DeserializeObject<List<Attributes>>(c.Attributes)
                                        .Select(attribute => attributesDictionary[attribute.name])
                                        .ToList(),
                        CreatedDate = c.CreatedDate,
                        ModifiedDate = c.ModifiedDate,
                        Status = c.Status
                    };

                    consentDTOs.Add(dto);
                }

                return new ServiceResult(true, "Successfully retrieved Consent", consentDTOs);
            }
            catch(Exception ex)
            {
                _logger.LogError("Get User Profiles Consent by Client Name failed : {0}", ex.Message);
                return new ServiceResult(false, "An error occurred while retrieving the user profiles consent." +
                    " Please contact the admin.");
            }
        }

        public async Task<ServiceResult> GetUserProfilesConsentByProfileAsync(string suid, string applicationName, string profile)
        {
            try
            {
                var client = await _clientService.GetClientByAppNameAsync(applicationName);
                if (client == null)
                {
                    return new ServiceResult(false, $"Consent Not Found");
                }

                var consent = await _unitOfWork.UserProfilesConsent.GetUserProfilesConsentByProfileAsync(suid, client.ClientId, profile);
                if (consent == null)
                {
                    return new ServiceResult(false, $"Consent Not Found");
                }

                var clientName = client.ApplicationName;

                Dictionary<string, string> attributesDictionary = new Dictionary<string, string>();

                var attributesList = await _userClaimService.ListUserClaimAsync();
                foreach (var attributes in attributesList)
                {
                    attributesDictionary[attributes.Name] = attributes.DisplayName;
                }

                var dict = new Dictionary<string, string>();

                var profiles = await _scopeService.ListScopeAsync();

                foreach (var p in profiles)
                {
                    var id = p.Id.ToString();
                    dict[id] = p.Name;
                }

                var dto = new UserProfilesConsentDTO
                {
                    Suid = consent.Suid,
                    ClientId = consent.ClientId,
                    ClientName = clientName,
                    Profile = dict[consent.Profile],
                    Attributes = string.IsNullOrEmpty(consent.Attributes)
                                    ? new List<string>()
                                    : JsonConvert.DeserializeObject<List<Attributes>>(consent.Attributes)
                                        .Select(attribute => attributesDictionary[attribute.name])
                                        .ToList(),
                    CreatedDate = consent.CreatedDate,
                    ModifiedDate = consent.ModifiedDate,
                    Status = consent.Status
                };

                return new ServiceResult(true, "Successfully retrieved Consent", dto);
            }
            catch(Exception ex)
            {
                _logger.LogError("Get User Profiles Consent by Profile failed : {0}", ex.Message);
                return new ServiceResult(false, "An error occurred while retrieving the user profiles consent." +
                    " Please contact the admin.");
            }
        }

        public async Task<ServiceResult> RevokeUserProfilesConsentByProfileAsync(string suid, string applicationName, string profile)
        {
            try
            {
                var client = await _clientService.GetClientByAppNameAsync(applicationName);
                if (client == null)
                {
                    return new ServiceResult(false, $"Consent Not Found");
                }

                var dict = new Dictionary<string, string>();

                var profiles = await _scopeService.ListScopeAsync();

                foreach (var p in profiles)
                {
                    var id = p.Id.ToString();
                    dict[p.Name] = id;
                }

                var consentProfilesinDb = await _unitOfWork.UserProfilesConsent.GetUserProfilesConsentByProfileAsync(suid, client.ClientId, dict[profile]);
                if (consentProfilesinDb == null)
                {
                    return new ServiceResult(false, $"Consent Not Found");
                }

                consentProfilesinDb.Status = "REVOKED";

                _unitOfWork.UserProfilesConsent.Update(consentProfilesinDb);
                await _unitOfWork.SaveAsync();

                return new ServiceResult(true, "Successfully revoked Consent");
            }
            catch(Exception ex)
            {
                _logger.LogError("User Profiles Consent Update failed : {0}", ex.Message);
                return new ServiceResult(false, "An error occurred while revoking the user profiles consent." +
                    " Please contact the admin.");
            }
        }

        public async Task<string> GetClientNameByClientID(string clientId)
        {
            try
            {
                var client = await _clientService.GetClientByClientIdAsync(clientId);
                if (client == null) return null;
                return client.ApplicationName;
            }
            catch (Exception ex)
            {
                _logger.LogError("Get Client Name by Client ID failed : {0}", ex.Message);
                return "";
            }
        }
    }
}
