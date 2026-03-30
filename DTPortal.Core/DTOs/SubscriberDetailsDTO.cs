using System;

namespace DTPortal.Core.DTOs
{
    public class SubscriberDetailsDTO
    {
        public string SubscriberUid { get; set; }

        public string SelfieURI { get; set; }

        public string FullName { get; set; }

        public string Photo { get; set; }

        public string SubscriberPhoto
        {
            get
            {
                if (!String.IsNullOrEmpty(Photo))
                {
                    return "data:image/png;base64," + Photo;
                }
                return null;
            }
        }

        public string DateOfBirth { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string GeoLocation { get; set; }

        public string Address { get; set; }

        public string DeviceRegistrationTime { get; set; }

        public string DeviceStatus { get; set; }

        public string SubscriberType { get; set; }

        public string OnBoardingMethod { get; set; }

        public string LevelOfAssurance { get; set; }

        public string OnBoardingTime { get; set; }

        public int IdDocType { get; set; }

        public string DocumentType
        {
            get
            {
                if (IdDocType == 1)
                    return "UID";
                else if (IdDocType == 3)
                    return "PASSPORT";
                else
                    return null;
            }
        }

        public string IdDocNumber { get; set; }

        public string CertificateIssueDate { get; set; }

        public string CertificateExpiryDate { get; set; }

        public string SignPinSetDate { get; set; }

        public string AuthPinSetDate { get; set; }

        public string RevocationDate { get; set; }

        public string RevocationReason { get; set; }

        public string CertificateStatus { get; set; }

        public string SubscriberStatus { get; set; }

        public string LastOnBoardingDate { get; set; }

        public bool IsDetailsAvailable { get; set; } = false;
    }

    public class OrganizaionDetails
    {
        public string OrganizationName { get; set; }

        public string OrganizationUid { get; set; }

        public string SubscriberEmailList { get; set; }

        public string Signatory { get; set; }

        public string eSealSignatory { get; set; }

        public string eSealPrepatory { get; set; }

        public string Template { get; set; }

        public string Bulksign { get; set; }
    }
}
