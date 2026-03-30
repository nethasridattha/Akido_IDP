namespace DTPortal.Core.DTOs
{
    public class CumulativeCountDTO
    {
        public int CountOfActiveServiceProviders { get; set; }

        public int CountOfInactiveServiceProviders { get; set; }
        public int inactiveOrganizations { get; set; }
        public int activeOrganizations { get; set; }
        public int registeredOrganizations { get; set; }
        public int totalOrganizations { get; set; }
        public int CountOfCertificates { get; set; }

        public int CountOfSubscribers { get; set; }

        public int CountOfActiveSubscribers { get; set; }

        public int CountOfInactiveSubscribers { get; set; }

        public int CountOfSignaturesWithXadesSuccess { get; set; }

        public int CountOfSignaturesWithPadesSuccess { get; set; }

        public int CountOfSignaturesWithCadesSuccess { get; set; }

        //public int CountOfSignaturesWithDataSuccess { get; set; }

        public int CountOfSignaturesWithESealSuccess { get; set; }

        public int CountOfSignaturesWithXadesFailed { get; set; }

        public int CountOfSignaturesWithPadesFailed { get; set; }

        public int CountOfSignaturesWithCadesFailed { get; set; }

        //public int CountOfSignaturesWithDataFailed { get; set; }

        public int CountOfSignaturesWithESealFailed { get; set; }

        public int CountOfAuthenticationsSuccess { get; set; }

        public int CountOfAuthenticationsFailed { get; set; }
    }
}
