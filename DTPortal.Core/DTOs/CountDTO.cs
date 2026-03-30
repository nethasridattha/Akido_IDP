namespace DTPortal.Core.DTOs
{
    public class CountDTO
    {
        public int CountOfAuthenticationsFailed { get; set; }

        public int CountOfAuthenticationsSuccess { get; set; }

        public int CountOfSignaturesWithXadesSuccess { get; set; }

        public int CountOfSignaturesWithXadesFailed { get; set; }

        public int CountOfSignaturesWithPadesSuccess { get; set; }

        public int CountOfSignaturesWithPadesFailed { get; set; }

        public int CountOfSignaturesWithCadesSuccess { get; set; }

        public int CountOfSignaturesWithCadesFailed { get; set; }

        //public int CountOfSignaturesWithDataSuccess { get; set; }

        //public int CountOfSignaturesWithDataFailed { get; set; }

        public int CountOfSignaturesWithESealSuccess { get; set; }

        public int CountOfSignaturesWithESealFailed { get; set; }
    }
}
