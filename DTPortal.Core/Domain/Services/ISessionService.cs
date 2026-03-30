using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Domain.Services
{
    public interface ISessionService
    {
        Task<GetAllGlobalSessionsResponse> GetAllGlobalSession(int index, int count);
        Task<GetAllUserSessionsResponse> GetAllIDPUserSessions(string input, int type);
        Task<GetAllUserSessionsResponse> GetAllRAUserSessions(string input, int type);
        Task<GetAllClientSessionsResponse> GetAllClientSessions(string clientId);
        Task<SessionResponse> GetGlobalSession(string sessionId);
        public Task<Response> ValidateAccessToken(string accessToken);
        Task<Response> ValidateAccessTokenSession(string accessToken);
        Task<Response> LogoutUser(LogoutUserRequest request);
    }
}
