using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.Core.Constants
{
    // Constants for Cache return codes
    public static class CacheCodes
    {
        public const int KeyExist = 104;
        public const int FailedToDelete = -104;
        public const int CommandException = -105;
        public const int TimeoutException = -106;
        public const int ConnectionException = -107;
        public const int E_FAIL = -1;
    }
}
