using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface ITokenManagerService
    {
        bool ValidateJWToken(string jwtOken, string issuer, string audience);
        clientDetails GetClientDetailsfromJwt(string jwtOken);

        bool ValidateRequestJWToken(string jwtoken, string issuer, string certificate);
        bool ValidateLogoutJWToken(string jwtoken, string certificate);
        Task<bool> ValidateDeviceRegistrationToken(string token);
    }
}
