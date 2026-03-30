using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public interface ILocalJWTManager
    {
        public string GenerateSecKeyJWT(string userName, string nonce);
        public bool ValidateSecKeyJWT(string token);
        public string GetJSONWebTokenClaims(string token);
    }
}
