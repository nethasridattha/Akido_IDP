using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IEConsentService
    {
        public Task<ServiceResult> CreateConsentAsync(EConsentDTO consentDTO);
        public Task<ServiceResult> GetConsentbyClientIdAsync(int clientId);
        public Task<ServiceResult> GetConsentListAsync();
        public Task<ServiceResult> GetConsentbyIdAsync(int consentId);
        public Task<ServiceResult> DeleteConsentbyIdAsync(int consentId);

        //public Task<ServiceResult> UpdateConsentAsync(EConsentDTO consentDTO);
        public Task<ServiceResult> GetEConsentClientDetailsByIdAsync(int clientId);
        public Task<ServiceResult> GetEConsentClientDetailsAsync(string profiles, string purposes);

        public Task<ServiceResult> CreateEConsentClientAsync(CreateEConsentClientDetailsDTO eConsentClientDetails);
        public Task<ServiceResult> UpdateEConsentClientAsync(UpdateEConsentClientDetailsDTO updateEConsentClientDetails);
        public Task<ServiceResult> GetClientProfiles(string clientId);
        public Task<ServiceResult> GetClientPurposes(string clientId);
    }
}
