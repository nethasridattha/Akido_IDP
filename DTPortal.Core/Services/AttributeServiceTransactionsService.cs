using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Fido2NetLib;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Org.BouncyCastle.Asn1.Ocsp;

namespace DTPortal.Core.Services
{
    public class AttributeServiceTransactionsService : IAttributeServiceTransactionsService
    {
        private readonly ITransactionProfileRequestService _transactionProfileRequestService;
        private readonly ITransactionProfileConsentService _transactionProfileConsentService;
        private readonly ITransactionProfileStatusService _transactionProfileStatusService;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientService _clientService;
        private readonly IScopeService _scopeService;
        private readonly IPurposeService _purposeService;
        public AttributeServiceTransactionsService(ITransactionProfileRequestService transactionProfileRequestService,
            ITransactionProfileConsentService transactionProfileConsentService,
            ITransactionProfileStatusService transactionProfileStatusService,
            IUnitOfWork unitOfWork,
            ILogger logger,
            IClientService clientService,
            IScopeService scopeService,
            IPurposeService purposeService)
        {
            _transactionProfileRequestService = transactionProfileRequestService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _transactionProfileStatusService = transactionProfileStatusService;
            _transactionProfileConsentService = transactionProfileConsentService;
            _clientService = clientService;
            _scopeService = scopeService;
            _purposeService = purposeService;
        }
        public async Task<IEnumerable<AttributeServiceTransactionListDTO>> GetAttributeServiceTransactionsList()
        {
            List<AttributeServiceTransactionListDTO> AttributeServiceTransactionsList = new List<AttributeServiceTransactionListDTO>();
            var transactionRequstProfileList = await _transactionProfileRequestService.TransactionProfileRequestList();
            foreach (var item in transactionRequstProfileList)
            {
                AttributeServiceTransactionListDTO attributeServiceTransactionsDTO = new AttributeServiceTransactionListDTO();

                attributeServiceTransactionsDTO.Id = item.Id;

                attributeServiceTransactionsDTO.TransactionId = item.TransactionId;

                attributeServiceTransactionsDTO.RequestDate = item.CreatedDate;

                GetUserProfileRequest profileRequest = JsonConvert.DeserializeObject<GetUserProfileRequest>(item.RequestDetails);

                attributeServiceTransactionsDTO.UserId = profileRequest.UserId;

                if (item.ClientId != null)
                {
                    item.Client = await _clientService.GetClientAsync((int)item.ClientId);
                }

                if (item.Client != null)
                {
                    attributeServiceTransactionsDTO.ClientName = item.Client.ApplicationName;
                }
                attributeServiceTransactionsDTO.RequestProfile = profileRequest.ProfileType;

                var transactionStatus = await _transactionProfileStatusService.GetTransactionProfileStatusbyTransactionId(item.Id);

                if (transactionStatus != null)
                {
                    attributeServiceTransactionsDTO.Status = transactionStatus.TransactionStatus;
                }

                AttributeServiceTransactionsList.Add(attributeServiceTransactionsDTO);
            }
            
            return AttributeServiceTransactionsList;
        }
        public async Task<AttributeServiceTransactionsDTO> GetDetails(int Id)
        {
            AttributeServiceTransactionsDTO attributeServiceTransactionsDTO = new AttributeServiceTransactionsDTO();

            TransactionProfileRequest transactionProfileRequest = await _transactionProfileRequestService.GetTransactionProfileRequest(Id);

            if (transactionProfileRequest == null)
            {
                return null;
            }

            var profileDict = new Dictionary<string, string>();

            var profiles = await _scopeService.ListScopeAsync();

            foreach (var p in profiles)
            {
                var id = p.Id.ToString();
                profileDict[id] = p.Name;
            }

            var purposeDict = new Dictionary<string, string>();

            var purposes = await _purposeService.GetPurposeListAsync();

            foreach (var p in purposes)
            {
                var id = p.Id.ToString();
                purposeDict[id] = p.Name;
            }

            attributeServiceTransactionsDTO.attributeProfileRequest.TransactionId = transactionProfileRequest.TransactionId;

            attributeServiceTransactionsDTO.attributeProfileRequest.RequestDate = transactionProfileRequest.CreatedDate;

            GetUserProfileRequest profileRequest = JsonConvert.DeserializeObject<GetUserProfileRequest>(transactionProfileRequest.RequestDetails);

            if (transactionProfileRequest.ClientId != null)
            {
                transactionProfileRequest.Client = await _clientService.GetClientAsync((int)transactionProfileRequest.ClientId);

            }
            if (transactionProfileRequest.Client != null)
            {
                attributeServiceTransactionsDTO.attributeProfileRequest.ClientName = transactionProfileRequest.Client.ApplicationName;
            }

            attributeServiceTransactionsDTO.attributeProfileRequest.RequestProfile = profileDict[profileRequest.ProfileType];

            attributeServiceTransactionsDTO.attributeProfileRequest.UserId = profileRequest.UserId;

            if (!string.IsNullOrEmpty(profileRequest.Purpose))
            {
                attributeServiceTransactionsDTO.attributeProfileRequest.RequestPurpose = purposeDict[profileRequest.Purpose];
            }
          

            var transactionStatus = await _transactionProfileStatusService.GetTransactionProfileStatusbyTransactionId(transactionProfileRequest.Id);

            if (transactionStatus != null)
            {
                attributeServiceTransactionsDTO.attributeProfileStatus.Status = transactionStatus.TransactionStatus;

                if (transactionStatus.FailedReason != null)
                {
                    attributeServiceTransactionsDTO.attributeProfileStatus.FailedReason = transactionStatus.FailedReason;
                }
                //if (transactionStatus.DatapivotId != null)
                //{
                //    attributeServiceTransactionsDTO.attributeProfileStatus.DataPivotId = (int)transactionStatus.DatapivotId;
                //}
            }

            var transactionConsent = await _transactionProfileConsentService.GetTransactionProfileConsentbyTransactionId(Id);

            if (transactionConsent != null)
            {
                attributeServiceTransactionsDTO.attributeProfileConsent.ConsentStatus = transactionConsent.ConsentStatus;

                attributeServiceTransactionsDTO.attributeProfileConsent.ApprovedProfileAttributes = transactionConsent.ApprovedProfileAttributes;
                attributeServiceTransactionsDTO.attributeProfileConsent.RequestedProfileAttributes = transactionConsent.RequestedProfileAttributes;
                if (transactionConsent.UpdatedDate != null)
                {
                    attributeServiceTransactionsDTO.attributeProfileConsent.ConsentUpdatedDate = (DateTime)transactionConsent.UpdatedDate;
                } 
            }

            return attributeServiceTransactionsDTO;
        }
        public async Task<IEnumerable<AttributeServiceTransactionListDTO>> GetAttributeServiceTransactionsListByOrgId(string OrgId)
        {
            List<AttributeServiceTransactionListDTO> AttributeServiceTransactionsList = new List<AttributeServiceTransactionListDTO>();
            var transactionRequstProfileList = await _transactionProfileRequestService.TransactionProfileRequestListByOrgId(OrgId);
            foreach (var item in transactionRequstProfileList)
            {
                AttributeServiceTransactionListDTO attributeServiceTransactionsDTO = new AttributeServiceTransactionListDTO();

                attributeServiceTransactionsDTO.Id = item.Id;

                attributeServiceTransactionsDTO.TransactionId = item.TransactionId;

                attributeServiceTransactionsDTO.RequestDate = item.CreatedDate;

                GetUserProfileRequest profileRequest = JsonConvert.DeserializeObject<GetUserProfileRequest>(item.RequestDetails);

                attributeServiceTransactionsDTO.UserId = profileRequest.UserId;

                if (item.ClientId != null)
                {
                    item.Client = await _clientService.GetClientAsync((int)item.ClientId);
                }

                if (item.Client != null)
                {
                    attributeServiceTransactionsDTO.ClientName = item.Client.ApplicationName;
                }
                attributeServiceTransactionsDTO.RequestProfile = profileRequest.ProfileType;

                var transactionStatus = await _transactionProfileStatusService.GetTransactionProfileStatusbyTransactionId(item.Id);

                if (transactionStatus != null)
                {
                    attributeServiceTransactionsDTO.Status = transactionStatus.TransactionStatus;
                }

                AttributeServiceTransactionsList.Add(attributeServiceTransactionsDTO);
            }
            return AttributeServiceTransactionsList;
        }

        public async Task<PaginatedList<AttributeServiceTransactionListDTO>> GetAttributeLogReportAsync(string startDate, string endDate, string userId, string userIdType, string applicationName, string profile, int perPage, int page = 1)
        {
            try
            {
                var clientId = 0;
                var suid = "";

                if (userId != null && userIdType != null)
                {
                    if (userIdType == "Email")
                    {
                        var user = await _unitOfWork.Subscriber.GetSubscriberInfoByEmail(userId);
                        if(user == null) return new PaginatedList<AttributeServiceTransactionListDTO>(Enumerable.Empty<AttributeServiceTransactionListDTO>(), page, perPage, 0, 0);
                        suid = user.SubscriberUid;
                    }
                    else
                    {
                        var user = await _unitOfWork.Subscriber.GetSubscriberInfoByPhone(userId);
                        if (user == null) return new PaginatedList<AttributeServiceTransactionListDTO>(Enumerable.Empty<AttributeServiceTransactionListDTO>(), page, perPage, 0, 0);
                        suid = user.SubscriberUid;
                    }
                }

                if (applicationName != null)
                {
                    var clientInDb = await _unitOfWork.Client.GetClientByAppNameAsync(applicationName);
                    if (null == clientInDb)
                    {
                        return new PaginatedList<AttributeServiceTransactionListDTO>(Enumerable.Empty<AttributeServiceTransactionListDTO>(), page, perPage, 0, 0);
                    }
                    clientId = clientInDb.Id;
                }

                List<AttributeServiceTransactionListDTO> AttributeServiceTransactionsList = new List<AttributeServiceTransactionListDTO>();


                if (!string.IsNullOrEmpty(profile))
                {
                    var profileId = await _scopeService.GetScopeIdByNameAsync(profile);
                    if (profileId == -1)
                    {
                        return new PaginatedList<AttributeServiceTransactionListDTO>(Enumerable.Empty<AttributeServiceTransactionListDTO>(), page, perPage, 0, 0);
                    }
                    profile = profileId.ToString();
                }

                var profileDict = new Dictionary<string, string>();

                var profiles = await _scopeService.ListScopeAsync();

                foreach(var p in profiles)
                {
                    var id = p.Id.ToString();
                    profileDict[id] = p.Name;
                }


                var transactionRequestProfileList = await _unitOfWork.TransactionProfileRequests.GetConditionedList(startDate, endDate, suid, clientId, profile, page, perPage);

                foreach (var item in transactionRequestProfileList)
                {
                    AttributeServiceTransactionListDTO attributeServiceTransactionsDTO = new AttributeServiceTransactionListDTO();

                    attributeServiceTransactionsDTO.Id = item.Id;

                    attributeServiceTransactionsDTO.TransactionId = item.TransactionId;

                    attributeServiceTransactionsDTO.RequestDate = item.CreatedDate;

                    GetUserProfileRequest profileRequest = JsonConvert.DeserializeObject<GetUserProfileRequest>(item.RequestDetails);

                    attributeServiceTransactionsDTO.UserId = profileRequest.UserId;

                    if (item.ClientId != null)
                    {
                        item.Client = await _clientService.GetClientAsync((int)item.ClientId);
                    }

                    if (item.Client != null)
                    {
                        attributeServiceTransactionsDTO.ClientName = item.Client.ApplicationName;
                    }

                    attributeServiceTransactionsDTO.RequestProfile = profileDict[profileRequest.ProfileType];

                    var transactionStatus = await _transactionProfileStatusService.GetTransactionProfileStatusbyTransactionId(item.Id);

                    if (transactionStatus != null)
                    {
                        attributeServiceTransactionsDTO.Status = transactionStatus.TransactionStatus;
                    }

                    AttributeServiceTransactionsList.Add(attributeServiceTransactionsDTO);
                }

                var totalCount = _unitOfWork.TransactionProfileRequests.GetConditionedListCount(startDate, endDate, userId, clientId, profile, page, perPage);
                var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);

                // Return paginated list
                return new PaginatedList<AttributeServiceTransactionListDTO>(AttributeServiceTransactionsList, page, perPage, totalPages, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAttributeLogReportAsync Error: {ex.Message}");
                return new PaginatedList<AttributeServiceTransactionListDTO>(Enumerable.Empty<AttributeServiceTransactionListDTO>(), page, perPage, 0, 0);
            }
        }

    }
}
