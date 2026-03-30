using DTPortal.Core.Domain.Services.Communication;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

namespace DTPortal.Core.Utilities

{
    public interface IMessageLocalizer
    {
        string GetMessage(LocalizedMessage message);
        string GetLocalizedDisplayName(LocalizedDisplayName displayName);
        string GetLocalizedDisplayName(string displayNameEn, string displayNameAr);
        Task<string> GetLocalizedDisplayNameFromPreferenceAsync(string displayNameEn, string displayNameAr, string suid);

        Task<string> GetUserPreferredLanguageAsync(string suid);
    }
}

