using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ClientResponse : BaseResponse<Client>
    {
        public ClientResponse (Client category) : base(category) { }

        public ClientResponse(string message) : base(message) { }

        public ClientResponse(Client category, string message) :
            base(category, message) { }
    }

    public class ClientRequest
    {
        public Client client { get; set; }
        public ClientsSaml2 ClientSaml2 { get; set; }
    }

}
