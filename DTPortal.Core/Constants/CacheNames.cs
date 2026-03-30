using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.Core.Constants
{
    // Constants for Cache Names
    public static class CacheNames
    {
        public const string TemporarySession = "TemporarySession";
        public const string GlobalSession = "GlobalSession";
        public const string AuthorizationCode = "AuthorizationCode";
        public const string AccessToken = "AccessToken";
        public const string UserSessions = "UserSessions";
        public const string ClientSessions = "ClientSessions";
        public const string MobileAuthTemporarySession = "MobileAuthTemporarySession";
    }
}
