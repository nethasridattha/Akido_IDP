using System.Threading.Tasks;
using System.Collections.Generic;

using DTPortal.Core.DTOs;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Domain.Services
{
    public interface ILogReportService
    {
        Task<IEnumerable<AdminActivity>> GetAdminLogReportAsync(int page = 1);

        Task<PaginatedList<LogReportDTO>> GetRegistrationAuthorityLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, string signatureType = null, bool eSealUsed = false, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetOnboardingLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetSigningServiceLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, string signatureType = null, bool eSealUsed = false, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetDigitalAuthenticationLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetCentralLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, string signatureType = null, bool eSealUsed = false, int page = 1, int perPage = 10);

        Task<PaginatedList<AdminLogReportDTO>> GetAdminLogReportAsync(string startDate, string endDate, string userName = null,
            string moduleName = null, int page = 1, int perPage = 10);
        Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificWalletTransactionLogReportAsync(string startDate, string endDate,
            string identifier, string transactionStatus, int page = 1, int perPage = 10);
        Task<PaginatedList<OnboardingFailedLogReportDTO>> GetOnboardingFailedLogReportAsync(string startDate, string endDate, string documentNumber, int page = 1, int perPage = 10);
        Task<PaginatedList<LogReportDTO>> GetSigningFailedLogReportAsync(string startDate, string endDate, int page = 1, int perPage = 10);
        Task<PaginatedList<LogReportDTO>> GetAuthenticationFailedLogReportAsync(string startDate, string endDate, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetRegistrationAuthorityLogReportByCorrelationIDAsync(string correlationID);

        Task<PaginatedList<LogReportDTO>> GetOnboardingLogReportByCorrelationIDAsync(string correlationID);

        Task<PaginatedList<LogReportDTO>> GetSigningServiceLogReportByCorrelationIDAsync(string correlationID, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetDigitalAuthenticationLogReportByCorrelationIDAsync(string correlationID, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificDigitalAuthenticationLogReportAsync(string startDate, string endDate, string identifier, string transactionStatus, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificSigningLogReportAsync(string startDate, string endDate, string identifier, string transactionStatus, int page = 1, int perPage = 10);

        Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificOnboardingLogReportAsync(string email, string identifier);

        Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificCertIssuanceLogReportAsync(string email, string identifier);

        Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificAllLogReportAsync(string startDate, string endDate, string identifier, string transactionStatus, int page = 1, int perPage = 10);

        string AddChecksum(object value);

        ServiceResult VerifyChecksum(object logReport);
    }
}
