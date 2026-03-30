using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ClientConsentRequestModel
    {
        public List<ProfilesModel> Profiles { get; set; } = new List<ProfilesModel>();
        public List<PurposesModel> Purposes { get; set; } = new List<PurposesModel>();
    }

    public class ProfilesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Selected { get; set; }
    }
    public class PurposesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Selected { get; set; }
    }
}
