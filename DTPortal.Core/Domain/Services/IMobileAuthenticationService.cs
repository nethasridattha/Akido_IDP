using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Domain.Services
{
    public interface IMobileAuthenticationService
    {
        public Task<ServiceResult> InitiateMobileAuthenticationAsync
            (MobileAuthRequest request);

        public Task<ServiceResult> GetConsentDetailsAsync
            (string sessionId);

        public Task<ServiceResult> AuthenticateUserAsync
            (AuthenticateUserRequest request);

        public Task<GetAuthZCodeResponse> GetAuthorizationCode
            (string sessionId);

        public Task<ServiceResult> VerifyClientDetails
            (AuthorizationRequest request);

        public Task<ServiceResult> AddTransactionLog
            (WalletTransactionRequestDTO request);

        public Task<PaginatedList<LogReportDTO>> GetAuthenticationTransactionLog
            (string suid, int pageNumber, int perPage = 10);

        public Task<ServiceResult> GetServiceProviderAppDetails
            (string token);
        public Task<ServiceResult> SaveConsentAsync
            (ConsentApprovalRequest request);

        public Task<ServiceResult> GetLogDetailsAsync(string identifier);

        public Task<ServiceResult> GetScopeDetailsAsync(string callStack, string ServiceName,string ActivityType);

        ServiceResult AddAuthenticationTransactionLog
            (Utilities.LogMessage request);

        public Task<ServiceResult> GetTransactionLogCount(string Id);
        public Task<ServiceResult> SaveUserScopesAsync
            (SaveConsentRequest request, string SessionId);

        public Task<ServiceResult> GetAuthenticationDetailsAsync
            (string CorrelationId, string ClientId);

    }
}
