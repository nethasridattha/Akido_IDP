using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class SessionResponse : BaseResponse<GlobalSession>
    {
        public SessionResponse(GlobalSession globalSession) : base(globalSession) { }

        public SessionResponse(string message) : base(message) { }
    }
    public class GetAllGlobalSessionsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int NextIndex { get; set; }
        public IList<GlobalSession> GlobalSessions { get; set; }
    }

    public class GlobalSessionsClientIds
    {
        public GlobalSession GlobalSession { get; set; }
        public string ClientId { get; set; }
    }

    public class GetAllUserSessionsResponsew
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<GlobalSession> GlobalSessions { get; set; }
    }

    public class GetAllUserSessionsResponse : BaseResponse<IList<GlobalSession>>
    {
        public GetAllUserSessionsResponse(IList<GlobalSession> globalSessions) : base(globalSessions) { }

        public GetAllUserSessionsResponse(string message) : base(message) { }
    }

    public class GetAllClientSessionsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<GlobalSession> GlobalSessions { get; set; }
    }



}
