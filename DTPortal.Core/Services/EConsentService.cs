using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{
    public class EConsentService : IEConsentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EConsentService> _logger;
        private readonly IPurposeService _purposeService;
        private readonly IScopeService _scopeService;
        private readonly IClientService _clientService;
        public EConsentService(IUnitOfWork unitOfWork,
            ILogger<EConsentService> logger,
            IScopeService scopeService,
            IPurposeService purposeService,
            IClientService clientService
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _purposeService = purposeService;
            _scopeService = scopeService;
            _clientService = clientService;
        }
        public async Task<ServiceResult> CreateConsentAsync(EConsentDTO consentDTO)
        {
            var consent = new EConsentClient()
            {
                //ClientId = get_unique_string(48),
                //ClientSecret = get_unique_string(64),
                //ApplicationName = consentDTO.ApplicationName,
                Scopes = consentDTO.Scopes,
                //PublicKeyCert = (consentDTO.PublicKeyCert != null ? getCertificate(consentDTO.PublicKeyCert) : ""),
                CreatedDate = DateTime.Now,
                CreatedBy = "system",
                //OrganizationUid = consentDTO.OrganizationUid,
                Purposes = consentDTO.Purposes
            };

            //var isExists = await _unitOfWork.Category.IsCategoryExistsWithNameAsync(
            //    consent.ApplicationName);

            //if (true == isExists)
            //{
            //    _logger.LogError("E-Consent already exists with given name");
            //    return new ServiceResult(false, "E-Consent already exists with given name");
            //}

            consent.Status = StatusConstants.ACTIVE;
            try
            {
                await _unitOfWork.EConsentClient.AddAsync(consent);
                await _unitOfWork.SaveAsync();
                return new ServiceResult(true, "E-Consent created successfully", consent );
            }
            catch (Exception ex)
            {
                _logger.LogError("E-Consent CreateAsync failed " + ex);
                return new ServiceResult(false, "An error occurred while creating E-Consent." +
                    " Please contact the admin.");
            }
        }

        public async Task<ServiceResult> GetConsentListAsync()
        {
            var consentlist = await _unitOfWork.EConsentClient.ListAllConsentServicesAsync();
            if (consentlist == null)
            {
                return new ServiceResult(false, "Failed to get E-Consent List");
            }
            return new ServiceResult(true, "Successfully retrieved E-Consent list", consentlist);
        }

        public async Task<ServiceResult> GetConsentbyIdAsync(int consentId)
        {
            var consent = await _unitOfWork.EConsentClient.GetConsentServiceByIdAsync(consentId);
            if (consent == null)
            {
                return new ServiceResult(false, $"No E-Consent present with {consentId} ID");
            }
            return new ServiceResult(true, "Successfully retrieved E-Consent details", consent);
        }

        public async Task<ServiceResult> GetConsentbyClientIdAsync(int clientId)
        {
            var consent = await _unitOfWork.EConsentClient.GetConsentServiceByClientIdAsync(clientId);
            if (consent == null)
            {
                return new ServiceResult(false, $"No E-Consent present with {clientId} ID");
            }
            return new ServiceResult(true, "Successfully retrieved E-Consent details", consent);
        }

        public async Task<ServiceResult> DeleteConsentbyIdAsync(int consentId)
        {
            try
            {
                var consent = await _unitOfWork.EConsentClient.GetConsentServiceByIdAsync(consentId);
                if (consent == null)
                {
                    return new ServiceResult(false, $"No E-Consent present with {consentId} ID");
                }
                _unitOfWork.EConsentClient.Remove(consent);
                await _unitOfWork.SaveAsync();
                return new ServiceResult(true, "E-Consent Deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete E-Consent : " + ex);
                return new ServiceResult(false, "Failed to delete E-Consent");
            }
        }
        
        //public async Task<ServiceResult> UpdateConsentAsync(EConsentDTO consentDTO)
        //{

        //    var consentinDb = await _unitOfWork.EConsentClient.GetByIdAsync(consentDTO.Id);
        //    //consentinDb.ApplicationName = consentDTO.ApplicationName;
        //    consentinDb.UpdatedBy = consentDTO.UpdatedBy;
        //    consentinDb.ModifiedDate = DateTime.Now;
        //    consentinDb.Scopes = consentDTO.Scopes;
        //    //consentinDb.PublicKeyCert = (consentDTO.PublicKeyCert != null ? getCertificate(consentDTO.PublicKeyCert) : "");
        //    consentinDb.Purposes = consentDTO.Purposes;
        //    //consentinDb.OrganizationUid = consentDTO.OrganizationUid;

        //    if (consentDTO.Status != null) consentinDb.Status = consentDTO.Status;

        //    var allCategories = await _unitOfWork.EConsentClient.GetAllAsync();
        //    foreach (var item in allCategories)
        //    {
        //        if (item.Id != consentDTO.Id)
        //        {
        //            if (item.ApplicationName == consentDTO.ApplicationName)
        //            {
        //                _logger.LogError("E-Consent already exists with given Name");
        //                return new ServiceResult(false, "E-Consent already exists with given Name");
        //            }
        //        }
        //    }
        //    try
        //    {
        //        _unitOfWork.EConsentClient.Update(consentinDb);
        //        await _unitOfWork.SaveAsync();
        //        return new ServiceResult(true, "E-Consent Updated successfully", consentinDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("E-Consent AddAsync failed " + ex);
        //        return new ServiceResult(false, "An error occurred while creating E-Consent." +
        //            " Please contact the admin.");
        //    }
        //}

        public async Task<ServiceResult> GetEConsentClientDetailsAsync(string profiles,string purposes)
        {
            try
            {
                var clientPurposes = (purposes ?? string.Empty).Split(",", StringSplitOptions.RemoveEmptyEntries);
                var purposeList = await _purposeService.GetPurposeListAsync();

                var purposesModel = purposeList
                    .Select(purpose => new PurposesModel
                    {
                        Id = purpose.Id,
                        Name = purpose.Name,
                        DisplayName = purpose.DisplayName,
                        Selected = clientPurposes.Contains(purpose.Id.ToString())
                    })
                    .ToList();

                var clientProfiles = (profiles ?? string.Empty).Split(",", StringSplitOptions.RemoveEmptyEntries);
                var profilesList = await _scopeService.ListScopeAsync();

                var profilesModel = profilesList
                    .Select(profile => new ProfilesModel
                    {
                        Id = profile.Id,
                        Name = profile.Name,
                        DisplayName = profile.DisplayName,
                        Selected = clientProfiles.Contains(profile.Id.ToString())
                    })
                    .ToList();

                var clientConsentRequestList = new ClientConsentRequestModel
                {
                    Purposes = purposesModel,
                    Profiles = profilesModel
                };

                return new ServiceResult(true, "GetClientProfileAndPurposesAsync successfull", clientConsentRequestList);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, "An error occured while getting Profiles and Purposes from ClientID " + ex.Message);
            }
        }

        public async Task<ServiceResult> GetEConsentClientDetailsByIdAsync(int clientId)
        {
            try
            {
                var clientPurposes = await _unitOfWork.EConsentClient.GetPurposesByClientId(clientId);
                string purposesString = clientPurposes != null && clientPurposes.Any() ? string.Join(",", clientPurposes) : string.Empty;

                var clientProfiles = await _unitOfWork.EConsentClient.GetProfilesByClientId(clientId);
                string profilesString = clientProfiles != null && clientProfiles.Any() ? string.Join(",", clientProfiles) : string.Empty;

                List<string> combinedList = new List<string> { purposesString, profilesString };

                return new ServiceResult(true, "GetEConsentClientDetailsByIdAsync successfull", combinedList);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, "An error occured while getting Profiles and Purposes from ClientID " + ex.Message);
            }
        }

        public async Task<ServiceResult> CreateEConsentClientAsync(CreateEConsentClientDetailsDTO eConsentClientDetails)
        {
            var eConsentClient = new EConsentClient()
            {
                ClientId = eConsentClientDetails.ClientId,
                Scopes = eConsentClientDetails.Scopes,
                CreatedDate = DateTime.Now,
                CreatedBy = eConsentClientDetails.CreatedBy,
                Purposes = eConsentClientDetails.Purposes,
            };

            eConsentClient.Status = StatusConstants.ACTIVE;
            try
            {
                await _unitOfWork.EConsentClient.AddAsync(eConsentClient);
                await _unitOfWork.SaveAsync();
                return new ServiceResult(true, $"E-Consent for created successfully", eConsentClient);
            }
            catch (Exception ex)
            {
                _logger.LogError("E-Consent CreateAsync failed " + ex);
                return new ServiceResult(false, "An error occurred while creating E-Consent." +
                    " Please contact the admin.");
            }
        }

        public async Task<ServiceResult> UpdateEConsentClientAsync(UpdateEConsentClientDetailsDTO updateEConsentClientDetails)
        {
            var clientInDb = await _unitOfWork.EConsentClient.GetConsentServiceByClientIdAsync(updateEConsentClientDetails.clientId);

            if(clientInDb == null)
            {
                return new ServiceResult(false, "Client details not found");
            }

            clientInDb.Purposes = updateEConsentClientDetails.purposes;
            clientInDb.Scopes = updateEConsentClientDetails.profiles;

            _unitOfWork.EConsentClient.Update(clientInDb);
            await _unitOfWork.SaveAsync();
            return new ServiceResult(true, "E-Consent Details Updated successfully", clientInDb);
        }
        public async Task<ServiceResult> GetClientProfiles(string clientId)
        {
            try
            {
                var clientInDb = await _clientService.GetClientProfilesAndPurposesAsync(clientId);

                if (null == clientInDb)
                {
                    return new ServiceResult(false, "Client not found");
                }
                var eConsentClient = clientInDb.EConsentClients.FirstOrDefault(s => s.Status == "ACTIVE");

                if (null == eConsentClient)
                {
                    return new ServiceResult(false, "Client not found");
                }

                var clientProfiles = eConsentClient.Scopes.Split(",").ToList();

                var dict = new Dictionary<string, string>();

                var profiles = await _scopeService.ListScopeAsync();

                foreach (var p in profiles)
                {
                    var id = p.Id.ToString();
                    dict[id] = p.Name;
                }

                List<string> profileList = new List<string>();
                foreach (var profile in clientProfiles)
                {
                    profileList.Add(dict[profile]);
                }

                return new ServiceResult(true, "Get Profiles List Successful", profileList);
            }
            catch(Exception)
            {
                _logger.LogError("Failed to get Client Profiles");
                return new ServiceResult(false, "Failed to get Client Profiles");
            }
        }

        public async Task<ServiceResult> GetClientPurposes(string clientId)
        {
            try
            {
                var clientInDb = await _clientService.GetClientProfilesAndPurposesAsync(clientId);

                if (null == clientInDb)
                {
                    return new ServiceResult(false, "Client not found");
                }
                var eConsentClient = clientInDb.EConsentClients.FirstOrDefault(s => s.Status == "ACTIVE");

                if (null == eConsentClient)
                {
                    return new ServiceResult(false, "Client not found");
                }

                var clientPurposes = eConsentClient.Purposes.Split(",").ToList();

                var dict = new Dictionary<string, string>();

                var purposes = await _purposeService.GetPurposeListAsync();

                foreach (var p in purposes)
                {
                    var id = p.Id.ToString();
                    dict[id] = p.Name;
                }

                List<string> purposeList = new List<string>();
                foreach (var purpose in clientPurposes)
                {
                    purposeList.Add(dict[purpose]);
                }

                return new ServiceResult(true, "Get Purposes List Successful", purposeList);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to get Client purpose");
                return new ServiceResult(false, "Failed to get Client purpose");
            }
        }
    }
}
