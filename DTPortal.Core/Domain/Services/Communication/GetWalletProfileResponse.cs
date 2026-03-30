using DTPortal.Core.Domain.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class GetWalletProfileResult
    {
        public string provisionUrl { get; set;}
        public string AuthnScheme { get; set;}
        public string ApplicationName { get; set; }
        public List<ScopeInfo> ConsentScopes { get; set; }
        public bool DeselectScopesAndClaims { get; set; }
        public string TransactionId { get; set; }

    }
    public class GetWalletProfileResponse : BaseResponse<GetWalletProfileResult>
    {
        public GetWalletProfileResponse(GetWalletProfileResult category) : base(category) { }

        public GetWalletProfileResponse(string message) : base(message) { }
    }
}
