using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using DTPortal.Core.Utilities;

namespace DTPortal.Core.Services
{
    public class UserConsentService : IUserConsentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserConsentService> _logger;
        private readonly ICacheClient _cacheClient;

        public UserConsentService(ILogger<UserConsentService> logger,
            IUnitOfWork unitOfWork, ICacheClient cacheClient)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _cacheClient = cacheClient;
        }

        public async Task<UserConsentResponse> ModifyUserConsent(UserConsent consent)
        {
            try
            {
                var userConsent = await _unitOfWork.UserConsent.GetUserConsent
                    (consent.Suid, consent.ClientId);
                if (null == userConsent)
                {
                    await _unitOfWork.UserConsent.AddAsync(consent);
                }
                else
                {
                    var userScope = JsonConvert.DeserializeObject<Scopes>(userConsent.Scopes);
                    if (null == userScope)
                    {
                        return new UserConsentResponse("Internal server error");
                    }

                    var jsonObject = JsonConvert.DeserializeObject<Scopes>(consent.Scopes);
                    if (null == jsonObject)
                    {
                        return new UserConsentResponse("Internal server error");
                    }

                    foreach (var item in jsonObject.approved_scopes)
                    {
                        userScope.approved_scopes.Add(item);
                    }

                    userConsent.Scopes = JsonConvert.SerializeObject(userScope);
                    userConsent.ModifiedDate = DateTime.Now;

                    _unitOfWork.UserConsent.Update(userConsent);
                }

                await _unitOfWork.SaveAsync();
                return new UserConsentResponse(consent);
            }
            catch (Exception err)
            {
                _logger.LogError("ModifyUserConsent Failed: {0}",
                    err.Message);
                return new UserConsentResponse("Internal server error");
            }

        }

        public async Task<CheckConsentResponse> CheckUserConsent(string clientId,
            string globalSession,
            IList<string> scopes)
        {
            CheckConsentResponse response = new CheckConsentResponse();

            var globalSessionDetails = await _cacheClient.Get<GlobalSession>
                ("GlobalSession", globalSession);
            if (null == globalSession)
            {
                response.Success = false;
                response.Message = "Global session not found";
                return response;
            }

            var clientDetails = await _unitOfWork.Client.GetClientByClientIdAsync
                (clientId);
            if (null == clientDetails)
            {
                response.Success = false;
                response.Message = "Client details not found";
                return response;
            }

            var userConsent = await _unitOfWork.UserConsent.GetUserConsent
                (globalSessionDetails.UserId, clientId);
            if (null == userConsent)
            {
                var scopeList = String.Join(" ", scopes);
                ConsentDetails consentDetails = new ConsentDetails();

                response.Success = false;
                response.Message = "User Consent Required";
                consentDetails.Scopes = scopes;
                consentDetails.Suid = globalSessionDetails.UserId;
                consentDetails.ApplicationName = clientDetails.ApplicationName;
                response.Result = consentDetails;
                return response;
            }

            var approvedCount = 0;
            var deniedCount = 0;

            var userScopes = JsonConvert.DeserializeObject<Scopes>(userConsent.Scopes);
            if (null == userScopes)
            {
                _logger.LogError("DeserializeObject failed");
                response.Success = false;
                response.Message = "userScopes does not exists";
                return response;
            }

            var unApprovedScopes = new List<string>();
            var deniedScopes = new List<string>();

            if (scopes.Count == 1)
            {
                var istrue = false;
                for (int i = 0; i < userScopes.approved_scopes.Count; i++)
                {
                    if (userScopes.approved_scopes[i].scope == scopes.First())
                    {
                        approvedCount++;
                        istrue = true;
                    }
                }
                if (istrue == false)
                {
                    unApprovedScopes.Add(scopes.First());
                }
            }
            else
            {
                foreach (var item in userScopes.approved_scopes)
                {
                    if (scopes.Contains(item.scope) && item.permission == true)
                    {
                        approvedCount++;
                    }
                    else if (scopes.Contains(item.scope) && item.permission == false)
                    {
                        deniedCount++;
                        deniedScopes.Add(item.scope);
                        //return response;
                    }
                    else
                    {
                        var userScopeList = new List<string>();

                        foreach (var item3 in userScopes.approved_scopes)
                        {
                            userScopeList.Add(item3.scope);
                        }
                        var firstNotSecond = scopes.Except(userScopeList).ToList();
                        if (firstNotSecond.Count > 0)
                        {
                            unApprovedScopes = firstNotSecond;
                        }
                    }
                }
            }
            if (approvedCount != scopes.Count)
            {
                if (deniedCount > 0)
                {
                    _logger.LogError("User denied consent for ({0})",
                        deniedScopes.ToArray());
                    ConsentDetails consentDetails = new ConsentDetails();

                    response.Success = false;
                    response.Message = "User denied consent";
                    consentDetails.Scopes = deniedScopes;
                    consentDetails.Suid = globalSessionDetails.UserId;
                    consentDetails.ApplicationName = clientDetails.ApplicationName;
                    response.Result = consentDetails;
                    return response;
                }
                if (unApprovedScopes.Any())
                {
                    var scopeList = String.Join(" ", unApprovedScopes);
                    _logger.LogError("User Consent Required for ({0})",
                        unApprovedScopes.ToArray());

                    ConsentDetails consentDetails = new ConsentDetails();

                    response.Success = false;
                    response.Message = "User Consent Required";
                    consentDetails.Scopes = unApprovedScopes;
                    consentDetails.Suid = globalSessionDetails.UserId;
                   consentDetails.ApplicationName = clientDetails.ApplicationName;
                    response.Result = consentDetails;
                    return response;
                }
            }

            response.Success = true;
            response.Message = string.Empty;
            return response;
        }

        public async Task<UserConsentResponse> AddUserConsentAsync(UserConsent consent)
        {
            try
            {
                await _unitOfWork.UserConsent.AddAsync(consent);
                await _unitOfWork.SaveAsync();
                return new UserConsentResponse(consent);
            }
            catch (Exception err)
            {
                _logger.LogError("RevokeUserConsent Failed: {0}",
                    err.Message);
                return new UserConsentResponse("Internal server error");
            }
        }

        public async Task<UserConsentResponse> UpdateUserConsentAsync(UserConsent consent)
        {
            try
            {
                var userConsent = await _unitOfWork.UserConsent.GetUserConsent
                    (consent.Suid, consent.ClientId);
                if (null == userConsent)
                {
                    return new UserConsentResponse("User consent not found");
                }
                userConsent.Scopes = consent.Scopes;
                userConsent.ModifiedDate = DateTime.Now;
                _unitOfWork.UserConsent.Update(userConsent);
                await _unitOfWork.SaveAsync();
                return new UserConsentResponse(consent);
            }
            catch (Exception err)
            {
                _logger.LogError("UpdateUserConsent Failed: {0}",
                    err.Message);
                return new UserConsentResponse("Internal server error");
            }
        }
    }
}
