using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Utilities
{
    public interface IAssertionValidationClient
    {
        public Task<Response> ValidateAssertion(string address, string requestUri,
        string assertion);

    }
}
