using System;


namespace DTPortal.Common
{
    public partial class EncryptionLibrary
    {
        public class ClientKey
        {
            public int Id { get; set; }

            public string ClientId { get; set; }

            public string ClientSecret { get; set; }

            public DateTime CreatedOn { get; set; }
        }
    }
}
