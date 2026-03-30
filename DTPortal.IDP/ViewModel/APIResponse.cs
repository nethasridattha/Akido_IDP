using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel
{
    public class APIResponse
    {
        public object result { get; set; }

        public string message { get; set; }

        public bool success { get; set; }
    }
}
