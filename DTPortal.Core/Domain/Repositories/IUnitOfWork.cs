using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IRoleRepository Roles { get; }

        IEncDecKeyRepository EncDecKeys { get; }

        ISMTPRepository SMTP { get; }

        IClientRepository Client { get; }

        IUserAuthDatumRepository UserAuthData { get; }

        IUserLoginDetailRepository UserLoginDetail { get; }

        IConfigurationRepository Configuration { get; }

        IRoleActivityRepository RoleActivity { get; }

        ISubscriberRepository Subscriber { get; }

        IUserConsentRepository UserConsent { get; }


        ISubscriberStatusRepository SubscriberStatus { get; }

        IOrgnizationEmailRepository OrgnizationEmail { get; }


        ICertificatesRepository Certificates { get; }


        IThresholdRepository Threshold { get; }


        IScopeRepository Scopes { get; }
        IUserClaimRepository UserClaims { get; }

        IPurposeRepository Purpose { get; }


        ITransactionProfileStatusRepository TransactionProfileStatus { get; }

        ITransactionProfileRequestsRepository TransactionProfileRequests { get; }

        ITransactionProfileConsentRepository TransactionProfileConsent { get; }

        ISubscriberCardDetailRepository SubscriberCardDetail { get; }

        IEConsentClientRepository EConsentClient { get; }

        IUserProfilesConsentRepository UserProfilesConsent { get; }


        IAuthSchemeRepository AuthScheme { get; }
        INorAuthSchemeRepository NorAuthScheme { get; }
        IPrimaryAuthSchemeRepository PrimaryAuthScheme { get; }

        ISubscriberCompleteDetailsRepository SubscriberCompleteDetails { get; }

        Task<int> SaveAsync();

        void DisableDetectChanges();

        void EnableDetectChanges();

        int Save();
    }
}
