using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class RawCsvDataDTO
    {

        public string UgpassEmail { get; set; }
        public string NIN { get; set; }
        public string MobNo { get; set; }
        public string PassportNumber { get; set; }
       // public string Designation { get; set; }
       // public string SignatureImage { get; set; }
        public string Signature_Permission { get; set; }
        public string Signature_Validity_Required{ get; set; }
        public string Signature_Valid_From { get; set; }
        public string Signature_Valid_Upto { get; set; }
        //public string Eseal_Permission { get; set; }
        //public string Eseal_Validity_Flag { get; set; }
        //public string Eseal_Valid_From { get; set; }
        //public string Eseal_Valid_Upto { get; set; }
        public string User_Annual_Subscription_Permission { get; set; }
        public string User_Annual_Subscription_Validity_Required { get; set; }
        public string User_Annual_Subscription_Valid_From { get; set; }
        public string User_Annual_Subscription_Valid_Upto { get; set; }


    }

    public class AddMultipleBeneficiaries
    {
        public IEnumerable<RawCsvDataDTO> ListA { get; set; } = new List<RawCsvDataDTO>();
    }
}
