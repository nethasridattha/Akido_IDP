using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ClientPurposesResponse : BaseResponse<ClientsPurpose>
    {
        public ClientPurposesResponse(ClientsPurpose category) : base(category) { }

        public ClientPurposesResponse(string message) : base(message) { }

        public ClientPurposesResponse(ClientsPurpose category, string message) : base(category, message) { }
    }
}
