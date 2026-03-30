using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ClientsSaml2Response : BaseResponse<ClientsSaml2>
    {
        public ClientsSaml2Response(ClientsSaml2 category) : base(category) { }

        public ClientsSaml2Response(string message) : base(message) { }
    }
}
