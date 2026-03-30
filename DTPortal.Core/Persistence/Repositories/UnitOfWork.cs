using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using Google.Apis.Auth.OAuth2;

namespace DTPortal.Core.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly idp_dtplatformContext _idpContext;
        private readonly ra_0_2Context _raContext;
        private IUserRepository _users;
        private IRoleRepository _roles;
        private IEncDecKeyRepository _encDecKeys;
        private ISMTPRepository _smtp;
        private IClientRepository _client;
        private ILogger _logger;
        private IUserAuthDatumRepository _userAuthData;
        private IUserLoginDetailRepository _userLoginDetail;
        private IConfigurationRepository _configuration;
        private IRoleActivityRepository _roleActivity;
        private ISubscriberRepository _subscriber;
        private IOrgnizationEmailRepository _orgnizationEmail;
        private IUserConsentRepository _userConsent;
        private ISubscriberStatusRepository _subscriberStatus;
        private ICertificatesRepository _certificates;
        private IThresholdRepository _threshold;
        private IScopeRepository _scopes;
        private IUserClaimRepository _userClaims;
        private IPurposeRepository _purpose;
        private ITransactionProfileConsentRepository _transactionProfileConsent;
        private ITransactionProfileRequestsRepository _transactionProfileRequests;
        private ITransactionProfileStatusRepository _transactionProfileStatus;
        private ISubscriberCardDetailRepository _subscriberCardDetail;
        private IEConsentClientRepository _eConsentClient;
        private IUserProfilesConsentRepository _userProfilesConsent;
        private IAuthSchemeRepository _authScheme;
        private INorAuthSchemeRepository _norAuthScheme;
        private IPrimaryAuthSchemeRepository _primaryAuthScheme;
        private ISubscriberCompleteDetailsRepository _subscriberCompleteDetails;
        public UnitOfWork(idp_dtplatformContext idpContext,
            ra_0_2Context raContext,
            ILogger Logger)
        {
            _idpContext = idpContext;
            _raContext = raContext;
            _logger = Logger;
        }

        public IUserRepository Users
        {
            get { return _users = _users ?? new UserRepository(_idpContext, _logger); }
        }

        public IRoleRepository Roles
        {
            get { return _roles = _roles ?? new RoleRepository(_idpContext, _logger); }
        }

        public IEncDecKeyRepository EncDecKeys
        {
            get { return _encDecKeys = _encDecKeys ?? new EncDecKeyRepository(_idpContext, _logger); }
        }

        public ISMTPRepository SMTP
        {
            get { return _smtp = _smtp ?? new SMTPRepository(_idpContext, _logger); }
        }
        public IClientRepository Client
        {
            get { return _client = _client ?? new ClientRepository(_idpContext, _logger); }
        }

        public IUserAuthDatumRepository UserAuthData
        {
            get { return _userAuthData = _userAuthData ?? new UserAuthDatumRepository(_idpContext, _logger); }
        }

        public IUserLoginDetailRepository UserLoginDetail
        {
            get { return _userLoginDetail = _userLoginDetail ?? new UserLoginDetailRepository(_idpContext, _logger); }
        }
        public IConfigurationRepository Configuration
        {
            get { return _configuration = _configuration ?? new ConfigurationRepository(_idpContext, _logger); }
        }
        public IRoleActivityRepository RoleActivity
        {
            get { return _roleActivity = _roleActivity ?? new RoleActivityRepository(_idpContext, _logger); }
        }

        public ISubscriberRepository Subscriber
        {
            get { return _subscriber = _subscriber ?? new SubscriberRepository(_raContext, _logger); }
        }
        public IUserConsentRepository UserConsent
        {
            get { return _userConsent = _userConsent ?? new UserConsentRepository(_idpContext, _logger); }
        }
        public ISubscriberStatusRepository SubscriberStatus
        {
            get { return _subscriberStatus = _subscriberStatus ?? new SubscriberStatusRepository(_raContext, _logger); }
        }

        public IOrgnizationEmailRepository OrgnizationEmail
        {
            get { return _orgnizationEmail = _orgnizationEmail ?? new OrgnizationEmailRepository(_raContext, _logger); }
        }


        public ICertificatesRepository Certificates
        {
            get { return _certificates = _certificates ?? new CertificateRepository(_idpContext, _logger); }
        }


        public IThresholdRepository Threshold
        {
            get { return _threshold = _threshold ?? new ThresholdRepository(_raContext, _logger); }
        }

        public IScopeRepository Scopes
        {
            get { return _scopes = _scopes ?? new ScopeRepository(_idpContext, _logger); }
        }

        public IUserClaimRepository UserClaims
        {
            get { return _userClaims = _userClaims ?? new UserClaimRepository(_idpContext, _logger); }
        }
        public IPurposeRepository Purpose
        {
            get { return _purpose = _purpose ?? new PurposeRepository(_idpContext, _logger); }
        }

        public ITransactionProfileStatusRepository TransactionProfileStatus
        {
            get { return _transactionProfileStatus = _transactionProfileStatus ?? new TransactionProfileStatusRepository(_idpContext, _logger); }
        }

        public ITransactionProfileRequestsRepository TransactionProfileRequests
        {
            get { return _transactionProfileRequests = _transactionProfileRequests ?? new TransactionProfileRequestsRepository(_idpContext, _logger); }
        }

        public ITransactionProfileConsentRepository TransactionProfileConsent
        {
            get { return _transactionProfileConsent = _transactionProfileConsent ?? new TransactionProfileConsentRepository(_idpContext, _logger); }
        }

        public ISubscriberCardDetailRepository SubscriberCardDetail
        {
            get { return _subscriberCardDetail = _subscriberCardDetail ?? new SubscriberCardDetailRepository(_raContext, _logger); }
        }

        public IEConsentClientRepository EConsentClient
        {
            get { return _eConsentClient = _eConsentClient ?? new EConsentClientRepository(_idpContext, _logger); }
        }

        public IUserProfilesConsentRepository UserProfilesConsent
        {
            get { return _userProfilesConsent = _userProfilesConsent ?? new UserProfilesConsentRepository(_idpContext, _logger); }
        }


        public IAuthSchemeRepository AuthScheme
        {
            get { return _authScheme = _authScheme ?? new AuthSchemeRepository(_idpContext, _logger); }
        }

        public INorAuthSchemeRepository NorAuthScheme
        {
            get { return _norAuthScheme = _norAuthScheme ?? new NorAuthSchemeRepository(_idpContext, _logger); }
        }

        public IPrimaryAuthSchemeRepository PrimaryAuthScheme
        {
            get { return _primaryAuthScheme = _primaryAuthScheme ?? new PrimaryAuthSchemeRepository(_idpContext, _logger); }
        }

        public ISubscriberCompleteDetailsRepository SubscriberCompleteDetails
        {
            get { return _subscriberCompleteDetails = _subscriberCompleteDetails ?? new SubscriberCompleteDetailsRepository(_raContext, _logger); }
        }
        public async Task<int> SaveAsync()
        {
            return await _idpContext.SaveChangesAsync();
        }

        public void DisableDetectChanges()
        {
            _idpContext.ChangeTracker.AutoDetectChangesEnabled = false;
            return;
        }

        public void EnableDetectChanges()
        {
            _idpContext.ChangeTracker.AutoDetectChangesEnabled = true;
            return;
        }
        public int Save()
        {
            return _idpContext.SaveChanges();
        }

    }
}
