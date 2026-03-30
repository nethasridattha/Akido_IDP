using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class NiraResponseDTO
    {
        public ReturnDetails Return { get; set; }
    }

    public class ReturnDetails
    {
        public string nationalId { get; set; }

        public string surname { get; set; }

        public string givenNames { get; set; }

        public string dateOfBirth { get; set; }

        public string gender { get; set; }

        public string nationality { get; set; }

        public string cardNumber { get; set; }

        public string cardExpiryDate { get; set; }

        public string cardStatus { get; set; }

        public string photo { get; set; }
    }
}
