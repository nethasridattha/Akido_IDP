using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class VerifierRequestDTO
    {
        public string Type { get; set; }
        public SelectedClaims SelectedClaims { get; set; }
        public string scope { get; set; }
        public string clientID { get; set; }
    }
    public class SelectedClaims
    {
        public List<string> Document { get; set; }
    }
}
