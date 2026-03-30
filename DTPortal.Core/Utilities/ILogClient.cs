using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Utilities
{
    public interface ILogClient
    {
        public Task<int> SendCentralLogMessage(string LogMessage);
        public Task<int> SendServiceLogMessage(string LogMessage);
        public Task<int> SendAdminLogMessage(string adminLogMessage);
        Response SendAuthenticationLogMessage(
            TemporarySession tempSession,
            string suid,
            string serviceName,
            string logMessage,
            string logMessageType,
            string transactionType,
            bool centralLog,
            string callStack = null,
            string userActivityType = "IDP_AUTHENTICATION"
            );
        Response SendAuthenticationLogMessage(
           DateTime StartTime,
           string suid,
           string serviceName,
           string logMessage,
           string logMessageType,
           string transactionType,
           bool centralLog
           );
        public Task<Response> SendWalletAuthenticationLog(
            string suid,
            string serviceProviderAppName,
            string serviceName,
            string logMessage,
            string logMessageType,
            string transactionType,
            string callStack,
            bool centralLog,
            string userActivityType);
    }
}
