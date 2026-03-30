using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class ValidateSignedDataRequestDTO
    {
        public string SignedData { get; set; }
        public string KycMethod { get; set; }
    }
}
