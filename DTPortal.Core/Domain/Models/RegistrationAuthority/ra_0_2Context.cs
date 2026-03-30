using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ra_0_2Context : DbContext
{
    public ra_0_2Context()
    {
    }

    public ra_0_2Context(DbContextOptions<ra_0_2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AdmissionInSchool> AdmissionInSchools { get; set; }

    public virtual DbSet<AgentsActivity> AgentsActivities { get; set; }

    public virtual DbSet<AppConfig> AppConfigs { get; set; }

    public virtual DbSet<AssuranceLevel> AssuranceLevels { get; set; }

    public virtual DbSet<BalanceSheetOrganization> BalanceSheetOrganizations { get; set; }

    public virtual DbSet<BalanceSheetSubscriber> BalanceSheetSubscribers { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BannersDetail> BannersDetails { get; set; }

    public virtual DbSet<Beneficiary> Beneficiaries { get; set; }

    public virtual DbSet<BeneficiaryInfoView> BeneficiaryInfoViews { get; set; }

    public virtual DbSet<BeneficiaryValidity> BeneficiaryValidities { get; set; }

    public virtual DbSet<BirthDetail> BirthDetails { get; set; }

    public virtual DbSet<BusinessAccount> BusinessAccounts { get; set; }

    public virtual DbSet<CarRentalSimulation> CarRentalSimulations { get; set; }

    public virtual DbSet<CertificateCountView> CertificateCountViews { get; set; }

    public virtual DbSet<ClientRegistrationDetail> ClientRegistrationDetails { get; set; }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<Consent> Consents { get; set; }

    public virtual DbSet<ConsentDataCategory> ConsentDataCategories { get; set; }

    public virtual DbSet<ConsentHistory> ConsentHistories { get; set; }

    public virtual DbSet<ConsentRecord> ConsentRecords { get; set; }

    public virtual DbSet<CountryDetail> CountryDetails { get; set; }

    public virtual DbSet<CreditsUsageHistory> CreditsUsageHistories { get; set; }

    public virtual DbSet<CustomerWallet> CustomerWallets { get; set; }

    public virtual DbSet<Cycle> Cycles { get; set; }

    public virtual DbSet<CycleMembership> CycleMemberships { get; set; }

    public virtual DbSet<DaesCertificate> DaesCertificates { get; set; }

    public virtual DbSet<DeathDetail> DeathDetails { get; set; }

    public virtual DbSet<DevicePolicy> DevicePolicies { get; set; }

    public virtual DbSet<DownloadSoftware> DownloadSoftwares { get; set; }

    public virtual DbSet<DrivingDetail> DrivingDetails { get; set; }

    public virtual DbSet<Entitlement> Entitlements { get; set; }

    public virtual DbSet<FaceThreshold> FaceThresholds { get; set; }

    public virtual DbSet<FreeCredit> FreeCredits { get; set; }

    public virtual DbSet<Fund> Funds { get; set; }

    public virtual DbSet<GenericRateCardAdditionalDefinition> GenericRateCardAdditionalDefinitions { get; set; }

    public virtual DbSet<GenericRateCardDefinition> GenericRateCardDefinitions { get; set; }

    public virtual DbSet<GetAllTemplate> GetAllTemplates { get; set; }

    public virtual DbSet<HealthCareRegistration> HealthCareRegistrations { get; set; }

    public virtual DbSet<HibernateSequence> HibernateSequences { get; set; }

    public virtual DbSet<HospitalInsurancePolicy> HospitalInsurancePolicies { get; set; }

    public virtual DbSet<Hospitality> Hospitalities { get; set; }

    public virtual DbSet<HotelOfflineLog> HotelOfflineLogs { get; set; }

    public virtual DbSet<HotelSimulator> HotelSimulators { get; set; }

    public virtual DbSet<IssuerKey> IssuerKeys { get; set; }

    public virtual DbSet<LoginDetail> LoginDetails { get; set; }

    public virtual DbSet<ManualCreditsAllocation> ManualCreditsAllocations { get; set; }

    public virtual DbSet<MapMethodOnboardingStep> MapMethodOnboardingSteps { get; set; }

    public virtual DbSet<MoneyExchangeSimulation> MoneyExchangeSimulations { get; set; }

    public virtual DbSet<MoneyTransfer> MoneyTransfers { get; set; }

    public virtual DbSet<OnboardingAgent> OnboardingAgents { get; set; }

    public virtual DbSet<OnboardingDocument> OnboardingDocuments { get; set; }

    public virtual DbSet<OnboardingLiveliness> OnboardingLivelinesses { get; set; }

    public virtual DbSet<OnboardingMethod> OnboardingMethods { get; set; }

    public virtual DbSet<OnboardingStep> OnboardingSteps { get; set; }

    public virtual DbSet<OnboardingStepDetail> OnboardingStepDetails { get; set; }

    public virtual DbSet<OrgApprovalSignedDatum> OrgApprovalSignedData { get; set; }

    public virtual DbSet<OrgBucket> OrgBuckets { get; set; }

    public virtual DbSet<OrgBucketsConfig> OrgBucketsConfigs { get; set; }

    public virtual DbSet<OrgCeoDetail> OrgCeoDetails { get; set; }

    public virtual DbSet<OrgCertificateEmailCounter> OrgCertificateEmailCounters { get; set; }

    public virtual DbSet<OrgClientAppConfig> OrgClientAppConfigs { get; set; }

    public virtual DbSet<OrgESeal> OrgESeals { get; set; }

    public virtual DbSet<OrgEmailDomain> OrgEmailDomains { get; set; }

    public virtual DbSet<OrgFinancialAuditorDetail> OrgFinancialAuditorDetails { get; set; }

    public virtual DbSet<OrgInfo> OrgInfos { get; set; }

    public virtual DbSet<OrgSignatureTemplate> OrgSignatureTemplates { get; set; }

    public virtual DbSet<OrgSubscriberEmail> OrgSubscriberEmails { get; set; }

    public virtual DbSet<OrgSubscriberEmailOld> OrgSubscriberEmailOlds { get; set; }

    public virtual DbSet<OrgWallet> OrgWallets { get; set; }

    public virtual DbSet<OrganisationCategory> OrganisationCategories { get; set; }

    public virtual DbSet<OrganisationFieldMapping> OrganisationFieldMappings { get; set; }

    public virtual DbSet<OrganisationFormField> OrganisationFormFields { get; set; }

    public virtual DbSet<OrganisationOnboardingForm> OrganisationOnboardingForms { get; set; }

    public virtual DbSet<OrganisationSpocDetail> OrganisationSpocDetails { get; set; }

    public virtual DbSet<OrganizationAdditionalDetail> OrganizationAdditionalDetails { get; set; }

    public virtual DbSet<OrganizationCertificate> OrganizationCertificates { get; set; }

    public virtual DbSet<OrganizationCertificateLifeCycle> OrganizationCertificateLifeCycles { get; set; }

    public virtual DbSet<OrganizationDetail> OrganizationDetails { get; set; }

    public virtual DbSet<OrganizationDirector> OrganizationDirectors { get; set; }

    public virtual DbSet<OrganizationDocument> OrganizationDocuments { get; set; }

    public virtual DbSet<OrganizationDocumentCheck> OrganizationDocumentChecks { get; set; }

    public virtual DbSet<OrganizationPaymentsHistory> OrganizationPaymentsHistories { get; set; }

    public virtual DbSet<OrganizationPricingSlabDefinition> OrganizationPricingSlabDefinitions { get; set; }

    public virtual DbSet<OrganizationPrivilege> OrganizationPrivileges { get; set; }

    public virtual DbSet<OrganizationStatus> OrganizationStatuses { get; set; }

    public virtual DbSet<OrganizationUsageReport> OrganizationUsageReports { get; set; }

    public virtual DbSet<OrganizationWrappedKey> OrganizationWrappedKeys { get; set; }

    public virtual DbSet<Organizationsview> Organizationsviews { get; set; }

    public virtual DbSet<PaymentHistory> PaymentHistories { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<PaynovaReturnTransaction> PaynovaReturnTransactions { get; set; }

    public virtual DbSet<PhotoFeature> PhotoFeatures { get; set; }

    public virtual DbSet<Pid> Pids { get; set; }

    public virtual DbSet<PoaCredential> PoaCredentials { get; set; }

    public virtual DbSet<PoaCredentialRequest> PoaCredentialRequests { get; set; }

    public virtual DbSet<PoaCredentialRequestsView> PoaCredentialRequestsViews { get; set; }

    public virtual DbSet<PoaCredentialView> PoaCredentialViews { get; set; }

    public virtual DbSet<PreferedTitle> PreferedTitles { get; set; }

    public virtual DbSet<PricingSlabDefinition> PricingSlabDefinitions { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramMembership> ProgramMemberships { get; set; }

    public virtual DbSet<QrIssuer> QrIssuers { get; set; }

    public virtual DbSet<Redeem> Redeems { get; set; }

    public virtual DbSet<RedemptionProof> RedemptionProofs { get; set; }

    public virtual DbSet<RegisteredSpoc> RegisteredSpocs { get; set; }

    public virtual DbSet<RegistrantDetail> RegistrantDetails { get; set; }

    public virtual DbSet<SchemaDatum> SchemaData { get; set; }

    public virtual DbSet<ServiceLevelManagement> ServiceLevelManagements { get; set; }

    public virtual DbSet<ServicesDefinition> ServicesDefinitions { get; set; }

    public virtual DbSet<SignatureTemplate> SignatureTemplates { get; set; }

    public virtual DbSet<SignatureTemplatesDetail> SignatureTemplatesDetails { get; set; }

    public virtual DbSet<SignedDocument> SignedDocuments { get; set; }

    public virtual DbSet<SigningDocument> SigningDocuments { get; set; }

    public virtual DbSet<SimulatedBoarderControl> SimulatedBoarderControls { get; set; }

    public virtual DbSet<SoftwareLicense> SoftwareLicenses { get; set; }

    public virtual DbSet<SoftwareLicenseApprovalRequest> SoftwareLicenseApprovalRequests { get; set; }

    public virtual DbSet<SoftwareLicensesHistory> SoftwareLicensesHistories { get; set; }

    public virtual DbSet<SoftwareSuggestionAndOrganisationReadiness> SoftwareSuggestionAndOrganisationReadinesses { get; set; }

    public virtual DbSet<SppMetaSoftwareName> SppMetaSoftwareNames { get; set; }

    public virtual DbSet<SppVgAuditorDetail> SppVgAuditorDetails { get; set; }

    public virtual DbSet<SppVgOrganisationCategory> SppVgOrganisationCategories { get; set; }

    public virtual DbSet<SppVgOrganizationDetail> SppVgOrganizationDetails { get; set; }

    public virtual DbSet<SppVgPasswordResetToken> SppVgPasswordResetTokens { get; set; }

    public virtual DbSet<SppVgSoftware> SppVgSoftwares { get; set; }

    public virtual DbSet<SppVgSpocDetail> SppVgSpocDetails { get; set; }

    public virtual DbSet<SppVgTrustedUser> SppVgTrustedUsers { get; set; }

    public virtual DbSet<SubCertStatus> SubCertStatuses { get; set; }

    public virtual DbSet<SubmitCheckboxConfig> SubmitCheckboxConfigs { get; set; }

    public virtual DbSet<Subscriber> Subscribers { get; set; }

    public virtual DbSet<SubscriberCardDetail> SubscriberCardDetails { get; set; }

    public virtual DbSet<SubscriberCertificate> SubscriberCertificates { get; set; }

    public virtual DbSet<SubscriberCertificateLifeCycle> SubscriberCertificateLifeCycles { get; set; }

    public virtual DbSet<SubscriberCertificatePinHistory> SubscriberCertificatePinHistories { get; set; }

    public virtual DbSet<SubscriberCertificateView> SubscriberCertificateViews { get; set; }

    public virtual DbSet<SubscriberCertificatesDetail> SubscriberCertificatesDetails { get; set; }

    public virtual DbSet<SubscriberCompleteDetail> SubscriberCompleteDetails { get; set; }

    public virtual DbSet<SubscriberConsent> SubscriberConsents { get; set; }

    public virtual DbSet<SubscriberContactHistory> SubscriberContactHistories { get; set; }

    public virtual DbSet<SubscriberCountView> SubscriberCountViews { get; set; }

    public virtual DbSet<SubscriberDevice> SubscriberDevices { get; set; }

    public virtual DbSet<SubscriberDevicesHistory> SubscriberDevicesHistories { get; set; }

    public virtual DbSet<SubscriberFcmToken> SubscriberFcmTokens { get; set; }

    public virtual DbSet<SubscriberIdDocCount> SubscriberIdDocCounts { get; set; }

    public virtual DbSet<SubscriberInfo> SubscriberInfos { get; set; }

    public virtual DbSet<SubscriberOnboardingDatum> SubscriberOnboardingData { get; set; }

    public virtual DbSet<SubscriberOnboardingTemplate> SubscriberOnboardingTemplates { get; set; }

    public virtual DbSet<SubscriberPaymentHistory> SubscriberPaymentHistories { get; set; }

    public virtual DbSet<SubscriberPaymentsHistory> SubscriberPaymentsHistories { get; set; }

    public virtual DbSet<SubscriberPersonalDocument> SubscriberPersonalDocuments { get; set; }

    public virtual DbSet<SubscriberRaDatum> SubscriberRaData { get; set; }

    public virtual DbSet<SubscriberSimDatum> SubscriberSimData { get; set; }

    public virtual DbSet<SubscriberStatus> SubscriberStatuses { get; set; }

    public virtual DbSet<SubscriberUgpassIdCard> SubscriberUgpassIdCards { get; set; }

    public virtual DbSet<SubscriberView> SubscriberViews { get; set; }

    public virtual DbSet<SubscriberWrappedKey> SubscriberWrappedKeys { get; set; }

    public virtual DbSet<SystemRateCard> SystemRateCards { get; set; }

    public virtual DbSet<SystemRateCardCustomPricing> SystemRateCardCustomPricings { get; set; }

    public virtual DbSet<SystemRateCardVolume> SystemRateCardVolumes { get; set; }

    public virtual DbSet<TaxPayerRegistrationDetail> TaxPayerRegistrationDetails { get; set; }

    public virtual DbSet<TelecomSimIssuance> TelecomSimIssuances { get; set; }

    public virtual DbSet<TempDocStatusDatum> TempDocStatusData { get; set; }

    public virtual DbSet<TemporaryPoa> TemporaryPoas { get; set; }

    public virtual DbSet<TemporaryTable> TemporaryTables { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TrustedSpoc> TrustedSpocs { get; set; }

    public virtual DbSet<TrustedStakeholder> TrustedStakeholders { get; set; }

    public virtual DbSet<TrustedUser> TrustedUsers { get; set; }

    public virtual DbSet<TsaDatum> TsaData { get; set; }

    public virtual DbSet<UrabDetail> UrabDetails { get; set; }

    public virtual DbSet<UserKey> UserKeys { get; set; }

    public virtual DbSet<UserVcCredential> UserVcCredentials { get; set; }

    public virtual DbSet<ValidationRule> ValidationRules { get; set; }

    public virtual DbSet<VehcileRental> VehcileRentals { get; set; }

    public virtual DbSet<ViewOrgUserEmailList> ViewOrgUserEmailLists { get; set; }

    public virtual DbSet<VisitorCompleteDetailsNodoc> VisitorCompleteDetailsNodocs { get; set; }

    public virtual DbSet<VoiceEnrollment> VoiceEnrollments { get; set; }

    public virtual DbSet<WalletCertInfo> WalletCertInfos { get; set; }

    public virtual DbSet<WalletCertificate> WalletCertificates { get; set; }

    public virtual DbSet<WalletCertificateNative> WalletCertificateNatives { get; set; }

    public virtual DbSet<WalletIssuer> WalletIssuers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdmissionInSchool>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("admission_in_school_pkey");

            entity.ToTable("admission_in_school");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicantName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("applicant_name");
            entity.Property(e => e.ChildrenData).HasColumnName("children_data");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("document_number");
            entity.Property(e => e.JsonData)
                .IsRequired()
                .HasColumnName("json_data");
            entity.Property(e => e.NumberOfChildren)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("number_of_children");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<AgentsActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("agents_activity_pkey");

            entity.ToTable("agents_activity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgentUgpassSuid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_ugpass_suid");
            entity.Property(e => e.AgentsName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("agents_name");
            entity.Property(e => e.AssistedOnboardedName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("assisted_onboarded_name");
            entity.Property(e => e.AssistedOnboardedSuid)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("assisted_onboarded_suid");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<AppConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("app_config_pkey");

            entity.ToTable("app_config");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LatestVersion)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("latest_version");
            entity.Property(e => e.MinimumVersion)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("minimum_version");
            entity.Property(e => e.OsVersion)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("os_version");
            entity.Property(e => e.Updatelink)
                .HasMaxLength(250)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("updatelink");
        });

        modelBuilder.Entity<AssuranceLevel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("assurance_levels");

            entity.Property(e => e.AssuranceLevel1)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("assurance_level");
            entity.Property(e => e.AssuranceLevelValue).HasColumnName("assurance_level_value");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
        });

        modelBuilder.Entity<BalanceSheetOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("balance_sheet_organization_pkey");

            entity.ToTable("balance_sheet_organization");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.OrganizationId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("organization_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.TotalCredits).HasColumnName("total_credits");
            entity.Property(e => e.TotalDebits).HasColumnName("total_debits");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.Service).WithMany(p => p.BalanceSheetOrganizations)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("balance_sheet_organization_fk");
        });

        modelBuilder.Entity<BalanceSheetSubscriber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("balance_sheet_subscriber_pkey");

            entity.ToTable("balance_sheet_subscriber");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.SubscriberSuid)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("subscriber_suid");
            entity.Property(e => e.TotalCredits).HasColumnName("total_credits");
            entity.Property(e => e.TotalDebits).HasColumnName("total_debits");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.Service).WithMany(p => p.BalanceSheetSubscribers)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("balance_sheet_subscriber_fk");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bank_pkey");

            entity.ToTable("bank");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ApplicantName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("applicant_name");
            entity.Property(e => e.ApplicantPhoto).HasColumnName("applicant_photo");
            entity.Property(e => e.ApplicationStatus)
                .HasMaxLength(100)
                .HasColumnName("application_status");
            entity.Property(e => e.BankAccountHolder)
                .HasMaxLength(100)
                .HasColumnName("bank_account_holder");
            entity.Property(e => e.BankAccountNumber)
                .HasMaxLength(100)
                .HasColumnName("bank_account_number");
            entity.Property(e => e.BankAccountOpeningForm).HasColumnName("bank_account_opening_form");
            entity.Property(e => e.BankAccountOpeningJsonData).HasColumnName("bank_account_opening_json_data");
            entity.Property(e => e.BankAccountStatus)
                .HasMaxLength(100)
                .HasColumnName("bank_account_status");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.EkycPdfBase64).HasColumnName("ekyc_pdf_base64");
            entity.Property(e => e.EstablishmentCard).HasColumnName("establishment_card");
            entity.Property(e => e.ExtendedEkyc).HasColumnName("extended_ekyc");
            entity.Property(e => e.KycJson).HasColumnName("kyc_json");
            entity.Property(e => e.KycPdfBase64).HasColumnName("kyc_pdf_base64");
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .HasColumnName("nationality");
            entity.Property(e => e.PassportDocument).HasColumnName("passport_document");
            entity.Property(e => e.PassportNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("passport_number");
            entity.Property(e => e.ResidenceIdDocument).HasColumnName("residence_id_document");
            entity.Property(e => e.ResidentIdNumber)
                .HasMaxLength(100)
                .HasColumnName("resident_id_number");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.TradeLicenseDocument).HasColumnName("trade_license_document");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
            entity.Property(e => e.VisitorDocument).HasColumnName("visitor_document");
        });

        modelBuilder.Entity<BannersDetail>(entity =>
        {
            entity.HasKey(e => e.BannerId).HasName("banners_details_pkey");

            entity.ToTable("banners_details");

            entity.Property(e => e.BannerId).HasColumnName("banner_id");
            entity.Property(e => e.BannerSmallImage).HasColumnName("banner_small_image");
            entity.Property(e => e.BannerText)
                .HasMaxLength(500)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("banner_text");
            entity.Property(e => e.BannnerBackgroundImage).HasColumnName("bannner_background_image");
            entity.Property(e => e.BannnerFullImage).HasColumnName("bannner_full_image");
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("beneficiaries_pkey");

            entity.ToTable("beneficiaries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BeneficiaryConsentAcquired).HasColumnName("beneficiary_consent_acquired");
            entity.Property(e => e.BeneficiaryDigitalId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("beneficiary_digital_id");
            entity.Property(e => e.BeneficiaryMobileNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("beneficiary_mobile_number");
            entity.Property(e => e.BeneficiaryName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("beneficiary_name");
            entity.Property(e => e.BeneficiaryNin)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("beneficiary_nin");
            entity.Property(e => e.BeneficiaryOfficeEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("beneficiary_office_email");
            entity.Property(e => e.BeneficiaryPassport)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("beneficiary_passport");
            entity.Property(e => e.BeneficiaryType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("beneficiary_type");
            entity.Property(e => e.BeneficiaryUgpassEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("beneficiary_ugpass_email");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("designation");
            entity.Property(e => e.SignaturePhoto).HasColumnName("signature_photo");
            entity.Property(e => e.SponsorDigitalId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("sponsor_digital_id");
            entity.Property(e => e.SponsorExternalId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("sponsor_external_id");
            entity.Property(e => e.SponsorName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("sponsor_name");
            entity.Property(e => e.SponsorPaymentPriorityLevel).HasColumnName("sponsor_payment_priority_level");
            entity.Property(e => e.SponsorType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("sponsor_type");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<BeneficiaryInfoView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("beneficiary_info_view");

            entity.Property(e => e.BeneficiaryConsentAcquired).HasColumnName("beneficiary_consent_acquired");
            entity.Property(e => e.BeneficiaryCreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("beneficiary_created_on");
            entity.Property(e => e.BeneficiaryDigitalId)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_digital_id");
            entity.Property(e => e.BeneficiaryId).HasColumnName("beneficiary_id");
            entity.Property(e => e.BeneficiaryMobileNumber)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_mobile_number");
            entity.Property(e => e.BeneficiaryName)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_name");
            entity.Property(e => e.BeneficiaryNin)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_nin");
            entity.Property(e => e.BeneficiaryOfficeEmail)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_office_email");
            entity.Property(e => e.BeneficiaryPassport)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_passport");
            entity.Property(e => e.BeneficiaryStatus)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_status");
            entity.Property(e => e.BeneficiaryType)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_type");
            entity.Property(e => e.BeneficiaryUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_ugpass_email");
            entity.Property(e => e.BeneficiaryUpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("beneficiary_updated_on");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.IsChargeable).HasColumnName("is_chargeable");
            entity.Property(e => e.PrivilegeId).HasColumnName("privilege_id");
            entity.Property(e => e.PrivilegeServiceDisplayName)
                .HasMaxLength(100)
                .HasColumnName("privilege_service_display_name");
            entity.Property(e => e.PrivilegeServiceId).HasColumnName("privilege_service_id");
            entity.Property(e => e.PrivilegeServiceName)
                .HasMaxLength(100)
                .HasColumnName("privilege_service_name");
            entity.Property(e => e.PrivilegeStatus)
                .HasMaxLength(20)
                .HasColumnName("privilege_status");
            entity.Property(e => e.SignaturePhoto).HasColumnName("signature_photo");
            entity.Property(e => e.SponsorDigitalId)
                .HasMaxLength(100)
                .HasColumnName("sponsor_digital_id");
            entity.Property(e => e.SponsorExternalId)
                .HasMaxLength(100)
                .HasColumnName("sponsor_external_id");
            entity.Property(e => e.SponsorName)
                .HasMaxLength(100)
                .HasColumnName("sponsor_name");
            entity.Property(e => e.SponsorPaymentPriorityLevel).HasColumnName("sponsor_payment_priority_level");
            entity.Property(e => e.SponsorType)
                .HasMaxLength(100)
                .HasColumnName("sponsor_type");
            entity.Property(e => e.ValidFrom)
                .HasColumnType("character varying")
                .HasColumnName("valid_from");
            entity.Property(e => e.ValidUpto)
                .HasColumnType("character varying")
                .HasColumnName("valid_upto");
            entity.Property(e => e.ValidityApplicable).HasColumnName("validity_applicable");
            entity.Property(e => e.ValidityCreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("validity_created_on");
            entity.Property(e => e.ValidityId).HasColumnName("validity_id");
            entity.Property(e => e.ValidityStatus)
                .HasMaxLength(100)
                .HasColumnName("validity_status");
            entity.Property(e => e.ValidityUpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("validity_updated_on");
        });

        modelBuilder.Entity<BeneficiaryValidity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("beneficiary_validity_pkey");

            entity.ToTable("beneficiary_validity");

            entity.HasIndex(e => e.PrivilegeServiceId, "beneficiary_validity_fk_1_idx");

            entity.HasIndex(e => e.BeneficiaryId, "beneficiary_validity_fk_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BeneficiaryId).HasColumnName("beneficiary_id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.PrivilegeServiceId).HasColumnName("privilege_service_id");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
            entity.Property(e => e.ValidFrom)
                .HasColumnType("character varying")
                .HasColumnName("valid_from");
            entity.Property(e => e.ValidUpto)
                .HasColumnType("character varying")
                .HasColumnName("valid_upto");
            entity.Property(e => e.ValidityApplicable).HasColumnName("validity_applicable");

            entity.HasOne(d => d.Beneficiary).WithMany(p => p.BeneficiaryValidities)
                .HasForeignKey(d => d.BeneficiaryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("beneficiary_validity_fk");

            entity.HasOne(d => d.PrivilegeService).WithMany(p => p.BeneficiaryValidities)
                .HasForeignKey(d => d.PrivilegeServiceId)
                .HasConstraintName("beneficiary_validity_fk_1");
        });

        modelBuilder.Entity<BirthDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("birth_details_pkey");

            entity.ToTable("birth_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendantOfBirth)
                .HasMaxLength(255)
                .HasColumnName("attendant_of_birth");
            entity.Property(e => e.BirthProofCertificate).HasColumnName("birth_proof_certificate");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.FamilyDetails)
                .HasColumnType("json")
                .HasColumnName("family_details");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.InformantDetails).HasColumnName("informant_details");
            entity.Property(e => e.InformantIddocument).HasColumnName("informant_iddocument");
            entity.Property(e => e.PlaceOfDelivery).HasColumnName("place_of_delivery");
            entity.Property(e => e.TypeOfBirth)
                .HasMaxLength(100)
                .HasColumnName("type_of_birth");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WeightInKgs).HasColumnName("weight_in_kgs");
        });

        modelBuilder.Entity<BusinessAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("business_account_pkey");

            entity.ToTable("business_account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicantName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("applicant_name");
            entity.Property(e => e.ApplicantPhoto).HasColumnName("applicant_photo");
            entity.Property(e => e.ApplicationStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("application_status");
            entity.Property(e => e.BankAccountHolder)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("bank_account_holder");
            entity.Property(e => e.BankAccountNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("bank_account_number");
            entity.Property(e => e.BankAccountOpeningForm).HasColumnName("bank_account_opening_form");
            entity.Property(e => e.BankAccountOpeningJsonData).HasColumnName("bank_account_opening_json_data");
            entity.Property(e => e.BankAccountStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("bank_account_status");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.Ekycpdfbase64).HasColumnName("ekycpdfbase64");
            entity.Property(e => e.EstablishmentCard).HasColumnName("establishment_card");
            entity.Property(e => e.ExtendedEkyc).HasColumnName("extended_ekyc");
            entity.Property(e => e.KycJson).HasColumnName("kyc_json");
            entity.Property(e => e.Kycpdfbase64).HasColumnName("kycpdfbase64");
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("nationality");
            entity.Property(e => e.PassportDocument).HasColumnName("passport_document");
            entity.Property(e => e.PassportNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("passport_number");
            entity.Property(e => e.ResidenceIdDocument).HasColumnName("residence_id_document");
            entity.Property(e => e.ResidentIdNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("resident_id_number");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.TradeLicenseDocument).HasColumnName("trade_license_document");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
            entity.Property(e => e.VisitorDocument).HasColumnName("visitor_document");
        });

        modelBuilder.Entity<CarRentalSimulation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("car_rental_simulation_pkey");

            entity.ToTable("car_rental_simulation");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('car_rental_simulation_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.ApplicantName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("applicant_name");
            entity.Property(e => e.CarNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("car_number");
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DrivingLicenseDocument)
                .IsRequired()
                .HasColumnName("driving_license_document");
            entity.Property(e => e.DrivingLicenseNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("driving_license_number");
            entity.Property(e => e.InternationalPermit)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("international_permit");
            entity.Property(e => e.JsonData)
                .IsRequired()
                .HasColumnName("json_data");
            entity.Property(e => e.NoOfDays)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("no_of_days");
            entity.Property(e => e.PassportDocument).HasColumnName("passport_document");
            entity.Property(e => e.PassportNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("passport_number");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PickUpDate)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("pick_up_date");
            entity.Property(e => e.PidDocument).HasColumnName("pid_document");
            entity.Property(e => e.RentalAgreementFile).HasColumnName("rental_agreement_file");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<CertificateCountView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("certificate_count_view");

            entity.Property(e => e.ActiveCertCount).HasColumnName("active_cert_count");
            entity.Property(e => e.CertCount).HasColumnName("cert_count");
            entity.Property(e => e.ExpiredCertCount).HasColumnName("expired_cert_count");
            entity.Property(e => e.RevokeCertCount).HasColumnName("revoke_cert_count");
        });

        modelBuilder.Entity<ClientRegistrationDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_registration_details_pkey");

            entity.ToTable("client_registration_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.CustomsAgent)
                .HasMaxLength(100)
                .HasColumnName("customs_agent");
            entity.Property(e => e.LicenceNumber)
                .HasMaxLength(100)
                .HasColumnName("licence_number");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.PostalAddress)
                .HasMaxLength(100)
                .HasColumnName("postal_address");
            entity.Property(e => e.RegistrationStatus)
                .HasMaxLength(100)
                .HasColumnName("registration_status");
            entity.Property(e => e.TaxPayerEmail)
                .HasMaxLength(100)
                .HasColumnName("tax_payer_email");
            entity.Property(e => e.TaxPayerName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("tax_payer_name");
            entity.Property(e => e.Tin)
                .HasMaxLength(100)
                .HasColumnName("tin");
            entity.Property(e => e.TypeOfUser)
                .HasMaxLength(100)
                .HasColumnName("type_of_user");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("config_pkey");

            entity.ToTable("config");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Config1).HasColumnName("config");
            entity.Property(e => e.Connector)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("connector");
            entity.Property(e => e.VerificationId).HasColumnName("verification_id");
            entity.Property(e => e.VerificationIdName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("verification_id_name");
        });

        modelBuilder.Entity<Consent>(entity =>
        {
            entity.HasKey(e => e.ConsentId).HasName("consent_pkey");

            entity.ToTable("consent");

            entity.Property(e => e.ConsentId).HasColumnName("consent_id");
            entity.Property(e => e.Consent1).HasColumnName("consent");
            entity.Property(e => e.ConsentType)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("consent_type");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.PrivacyConsent).HasColumnName("privacy_consent");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<ConsentDataCategory>(entity =>
        {
            entity.HasKey(e => new { e.ConsentId, e.Category }).HasName("consent_data_categories_pkey");

            entity.ToTable("consent_data_categories");

            entity.Property(e => e.ConsentId).HasColumnName("consent_id");
            entity.Property(e => e.Category)
                .HasMaxLength(150)
                .HasColumnName("category");

            entity.HasOne(d => d.Consent).WithMany(p => p.ConsentDataCategories)
                .HasForeignKey(d => d.ConsentId)
                .HasConstraintName("fk_consent_data_categories_consent");
        });

        modelBuilder.Entity<ConsentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("consent_history_pkey");

            entity.ToTable("consent_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Consent)
                .IsRequired()
                .HasColumnName("consent");
            entity.Property(e => e.ConsentId).HasColumnName("consent_id");
            entity.Property(e => e.ConsentRequired).HasColumnName("consent_required");
            entity.Property(e => e.ConsentType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("consent_type");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.OptionalDataAndPrivacy).HasColumnName("optional_data_and_privacy");
            entity.Property(e => e.OptionalTermsAndConditions).HasColumnName("optional_terms_and_conditions");
            entity.Property(e => e.PrivacyConsent)
                .IsRequired()
                .HasColumnName("privacy_consent");
        });

        modelBuilder.Entity<ConsentRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("consent_records_pkey");

            entity.ToTable("consent_records");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CitizenName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("citizen_name");
            entity.Property(e => e.ConsentMethod)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("consent_method");
            entity.Property(e => e.ConsentType)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("consent_type");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.DigitalSignature).HasColumnName("digital_signature");
            entity.Property(e => e.EmiratesId)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnName("emirates_id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(64)
                .HasColumnName("ip_address");
            entity.Property(e => e.Purpose)
                .IsRequired()
                .HasColumnName("purpose");
            entity.Property(e => e.RevocationReason)
                .HasMaxLength(1000)
                .HasColumnName("revocation_reason");
            entity.Property(e => e.RevokedDate).HasColumnName("revoked_date");
            entity.Property(e => e.ServiceProvider)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("service_provider");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDING'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(512)
                .HasColumnName("user_agent");
            entity.Property(e => e.ValidityEnd).HasColumnName("validity_end");
            entity.Property(e => e.ValidityStart).HasColumnName("validity_start");
        });

        modelBuilder.Entity<CountryDetail>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_details_pkey");

            entity.ToTable("country_details");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("country_code");
            entity.Property(e => e.CountryFlag).HasColumnName("country_flag");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("country_name");
            entity.Property(e => e.MaxMobileDigits)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("max_mobile_digits");
            entity.Property(e => e.SupportNationalityId)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("support_nationality_id");
        });

        modelBuilder.Entity<CreditsUsageHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("credits_usage_history_pkey");

            entity.ToTable("credits_usage_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.OrganizationId)
                .HasColumnType("character varying")
                .HasColumnName("organization_id");
            entity.Property(e => e.ServiceName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("service_name");
            entity.Property(e => e.SubscriberSuid)
                .HasColumnType("character varying")
                .HasColumnName("subscriber_suid");
            entity.Property(e => e.TransactionForOrganization)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("transaction_for_organization");
        });

        modelBuilder.Entity<CustomerWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_wallet_pkey");

            entity.ToTable("customer_wallet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.ArabicName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("arabic_name");
            entity.Property(e => e.ClientIdentifier)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("client_identifier");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on");
            entity.Property(e => e.CustomerCode)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("customer_code");
            entity.Property(e => e.CustomerType)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("customer_type");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EnglishName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("english_name");
            entity.Property(e => e.IdentificationNumber)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("identification_number");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .HasColumnName("mobile");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PaynovaIdentifier)
                .HasMaxLength(100)
                .HasColumnName("paynova_identifier");
            entity.Property(e => e.StakeholderType)
                .HasMaxLength(100)
                .HasColumnName("stakeholder_type");
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            entity.Property(e => e.WalletNumber).HasColumnName("wallet_number");
        });

        modelBuilder.Entity<Cycle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cycle_pkey");

            entity.ToTable("cycle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CycleEndDate).HasColumnName("cycle_end_date");
            entity.Property(e => e.CycleName)
                .HasMaxLength(255)
                .HasColumnName("cycle_name");
            entity.Property(e => e.CycleStartDate).HasColumnName("cycle_start_date");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.HasOne(d => d.Program).WithMany(p => p.Cycles)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cycle_program_fk");
        });

        modelBuilder.Entity<CycleMembership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cycle_membership_pkey");

            entity.ToTable("cycle_membership");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CycleId).HasColumnName("cycle_id");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.RegistrantId).HasColumnName("registrant_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.HasOne(d => d.Cycle).WithMany(p => p.CycleMemberships)
                .HasForeignKey(d => d.CycleId)
                .HasConstraintName("cycle_membership_cycle_fk");

            entity.HasOne(d => d.Program).WithMany(p => p.CycleMemberships)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("cycle_membership_program_fk");

            entity.HasOne(d => d.Registrant).WithMany(p => p.CycleMemberships)
                .HasForeignKey(d => d.RegistrantId)
                .HasConstraintName("cycle_membership_registrant_details_fk");
        });

        modelBuilder.Entity<DaesCertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateSerialNumber).HasName("daes_certificate_pkey");

            entity.ToTable("daes_certificate");

            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(32)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CerificateExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateData)
                .IsRequired()
                .HasMaxLength(4096)
                .HasColumnName("certificate_data");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateStatus)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CertificateType)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("certificate_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<DeathDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("death_details_pkey");

            entity.ToTable("death_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfDeath).HasColumnName("date_of_death");
            entity.Property(e => e.FamilyDetails).HasColumnName("family_details");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(100)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.InformantDetails).HasColumnName("informant_details");
            entity.Property(e => e.InformantIdDocument).HasColumnName("informant_id_document");
            entity.Property(e => e.MannerOfDeath)
                .HasMaxLength(255)
                .HasColumnName("manner_of_death");
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .HasColumnName("nationality");
            entity.Property(e => e.PlaceOfDeath)
                .HasMaxLength(255)
                .HasColumnName("place_of_death");
            entity.Property(e => e.ProofOfDeath).HasColumnName("proof_of_death");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<DevicePolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("device_policy_pkey");

            entity.ToTable("device_policy");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeviceChangePolicyHour).HasColumnName("device_change_policy_hour");
        });

        modelBuilder.Entity<DownloadSoftware>(entity =>
        {
            entity.HasKey(e => e.SoftwareId).HasName("download_softwares_pkey");

            entity.ToTable("download_softwares");

            entity.Property(e => e.SoftwareId).HasColumnName("software_id");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.DownloadLink)
                .HasMaxLength(100)
                .HasColumnName("download_link");
            entity.Property(e => e.FileName)
                .HasMaxLength(100)
                .HasColumnName("file_name");
            entity.Property(e => e.InstallManual)
                .HasMaxLength(100)
                .HasColumnName("install_manual");
            entity.Property(e => e.PublishedOn)
                .HasMaxLength(100)
                .HasColumnName("published_on");
            entity.Property(e => e.SizeOfManual)
                .HasMaxLength(100)
                .HasColumnName("size_of_manual");
            entity.Property(e => e.SizeOfSoftware)
                .HasMaxLength(100)
                .HasColumnName("size_of_software");
            entity.Property(e => e.SoftwareName)
                .HasMaxLength(100)
                .HasColumnName("software_name");
            entity.Property(e => e.SoftwareVersion)
                .HasMaxLength(100)
                .HasColumnName("software_version");
            entity.Property(e => e.SpocSuid)
                .HasMaxLength(100)
                .HasColumnName("spoc_suid");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<DrivingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("driving_details_pkey");

            entity.ToTable("driving_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasMaxLength(100)
                .HasColumnName("birthdate");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.DrivingLicenseNo)
                .HasMaxLength(100)
                .HasColumnName("driving_license_no");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.ExpiryDate)
                .HasMaxLength(100)
                .HasColumnName("expiry_date");
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .HasColumnName("gender");
            entity.Property(e => e.IdDocumentNumber)
                .HasMaxLength(100)
                .HasColumnName("id_document_number");
            entity.Property(e => e.InternationalPermit)
                .HasMaxLength(100)
                .HasColumnName("international_permit");
            entity.Property(e => e.IssueDate)
                .HasMaxLength(100)
                .HasColumnName("issue_date");
            entity.Property(e => e.IssuingAuthority)
                .HasMaxLength(100)
                .HasColumnName("issuing_authority");
            entity.Property(e => e.IssuingCountry)
                .HasMaxLength(100)
                .HasColumnName("issuing_country");
            entity.Property(e => e.MdlDocument).HasColumnName("mdl_document");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .HasColumnName("phone_number");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<Entitlement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entitlements_pkey");

            entity.ToTable("entitlements");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(38)
                .HasColumnName("amount");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.Currency)
                .HasMaxLength(255)
                .HasColumnName("currency");
            entity.Property(e => e.CycleId).HasColumnName("cycle_id");
            entity.Property(e => e.Ern)
                .HasMaxLength(255)
                .HasColumnName("ern");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.RegistrantId).HasColumnName("registrant_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.ValidFrom).HasColumnName("valid_from");
            entity.Property(e => e.ValidUntil).HasColumnName("valid_until");
            entity.Property(e => e.VideoFile)
                .HasColumnType("character varying")
                .HasColumnName("video_file");
            entity.Property(e => e.VideoUploadedOn)
                .HasMaxLength(255)
                .HasColumnName("video_uploaded_on");

            entity.HasOne(d => d.Cycle).WithMany(p => p.Entitlements)
                .HasForeignKey(d => d.CycleId)
                .HasConstraintName("entitlements_cycle_fk");

            entity.HasOne(d => d.Program).WithMany(p => p.Entitlements)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("entitlements_program_fk");

            entity.HasOne(d => d.Registrant).WithMany(p => p.Entitlements)
                .HasForeignKey(d => d.RegistrantId)
                .HasConstraintName("entitlements_registrant_details_fk");
        });

        modelBuilder.Entity<FaceThreshold>(entity =>
        {
            entity.HasKey(e => e.MapMethodOnboardingStepId).HasName("face_threshold_pkey");

            entity.ToTable("face_threshold");

            entity.Property(e => e.MapMethodOnboardingStepId).HasColumnName("map_method_onboarding_step_id");
            entity.Property(e => e.AndriodDttThreshold).HasColumnName("andriod_dtt_threshold");
            entity.Property(e => e.AndroidDlibThreshold).HasColumnName("android_dlib_threshold");
            entity.Property(e => e.AndroidTfliteThreshold).HasColumnName("android_tflite_threshold");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.IntegrationUrl)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("integration_url");
            entity.Property(e => e.IosDlibThreshold).HasColumnName("ios_dlib_threshold");
            entity.Property(e => e.IosDttThreshold).HasColumnName("ios_dtt_threshold");
            entity.Property(e => e.IosTfliteThreshold).HasColumnName("ios_tflite_threshold");
            entity.Property(e => e.MethodName)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("method_name");
            entity.Property(e => e.OnboardingStep)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("onboarding_step");
            entity.Property(e => e.OnboardingStepThreshold).HasColumnName("onboarding_step_threshold");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.WebDttThreshold).HasColumnName("web_dtt_threshold");
        });

        modelBuilder.Entity<FreeCredit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("free_credits_pkey");

            entity.ToTable("free_credits");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreditsType)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("credits_type");
            entity.Property(e => e.FreeCredits).HasColumnName("free_credits");
            entity.Property(e => e.MaximumLimitMessage)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("maximum_limit_message");
            entity.Property(e => e.NotificationMessage)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("notification_message");
            entity.Property(e => e.Stakeholder)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("stakeholder");
        });

        modelBuilder.Entity<Fund>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fund_pkey");

            entity.ToTable("fund");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvailableFund)
                .HasPrecision(38)
                .HasColumnName("available_fund");
            entity.Property(e => e.Currency)
                .HasMaxLength(255)
                .HasColumnName("currency");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.ProgramName)
                .HasMaxLength(255)
                .HasColumnName("program_name");
            entity.Property(e => e.ReservedFund)
                .HasPrecision(38)
                .HasColumnName("reserved_fund");
            entity.Property(e => e.TotalDebitFund)
                .HasPrecision(38)
                .HasColumnName("total_debit_fund");
            entity.Property(e => e.TotalFund)
                .HasPrecision(38)
                .HasColumnName("total_fund");

            entity.HasOne(d => d.Program).WithMany(p => p.Funds)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("fund_program_fk");
        });

        modelBuilder.Entity<GenericRateCardAdditionalDefinition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("generic_rate_card_additional_definitions_pkey");

            entity.ToTable("generic_rate_card_additional_definitions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GenericRateCardId).HasColumnName("generic_rate_card_id");
            entity.Property(e => e.Tax).HasColumnName("tax");

            entity.HasOne(d => d.GenericRateCard).WithMany(p => p.GenericRateCardAdditionalDefinitions)
                .HasForeignKey(d => d.GenericRateCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("generic_rate_card_additional_definitions_fk");
        });

        modelBuilder.Entity<GenericRateCardDefinition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("generic_rate_card_definitions_pkey");

            entity.ToTable("generic_rate_card_definitions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovedBy)
                .HasColumnType("character varying")
                .HasColumnName("approved_by");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.RateEffectiveFrom)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("rate_effective_from");
            entity.Property(e => e.RateEffectiveUpto)
                .HasColumnType("character varying")
                .HasColumnName("rate_effective_upto");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Stakeholder)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("stakeholder");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Service).WithMany(p => p.GenericRateCardDefinitions)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("generic_rate_card_definitions_fk");
        });

        modelBuilder.Entity<GetAllTemplate>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("get_all_template");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(255)
                .HasColumnName("approved_by");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.PublishedStatus)
                .HasMaxLength(16)
                .HasColumnName("published_status");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks");
            entity.Property(e => e.State)
                .HasMaxLength(16)
                .HasColumnName("state");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.TemplateMethod)
                .HasMaxLength(64)
                .HasColumnName("template_method");
            entity.Property(e => e.TemplateName)
                .HasMaxLength(32)
                .HasColumnName("template_name");
            entity.Property(e => e.UpatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("upated_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<HealthCareRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("health_care_registration_pkey");

            entity.ToTable("health_care_registration");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .HasColumnName("gender");
            entity.Property(e => e.InsuredName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("insured_name");
            entity.Property(e => e.JsonData)
                .IsRequired()
                .HasColumnName("json_data");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .HasColumnName("phone_number");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PolicyDocument).HasColumnName("policy_document");
            entity.Property(e => e.PolicyEndDate)
                .HasMaxLength(100)
                .HasColumnName("policy_end_date");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(100)
                .HasColumnName("policy_name");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(100)
                .HasColumnName("policy_number");
            entity.Property(e => e.PolicyStartDate)
                .HasMaxLength(100)
                .HasColumnName("policy_start_date");
            entity.Property(e => e.PolicyStatus)
                .HasMaxLength(100)
                .HasColumnName("policy_status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<HibernateSequence>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("hibernate_sequence");

            entity.Property(e => e.NextVal).HasColumnName("next_val");
        });

        modelBuilder.Entity<HospitalInsurancePolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hospital_insurance_policy_pkey");

            entity.ToTable("hospital_insurance_policy");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("gender");
            entity.Property(e => e.InsuredName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("insured_name");
            entity.Property(e => e.JsonData)
                .IsRequired()
                .HasColumnName("json_data");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PolicyDocument).HasColumnName("policy_document");
            entity.Property(e => e.PolicyEndDate)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("policy_end_date");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("policy_name");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("policy_number");
            entity.Property(e => e.PolicyStartDate)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("policy_start_date");
            entity.Property(e => e.PolicyStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("policy_status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<Hospitality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hospitality_pkey");

            entity.ToTable("hospitality");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgeInYears)
                .HasColumnType("character varying")
                .HasColumnName("age_in_years");
            entity.Property(e => e.AgeOver18)
                .HasColumnType("character varying")
                .HasColumnName("age_over_18");
            entity.Property(e => e.CreationDate)
                .HasColumnType("character varying")
                .HasColumnName("creation_date");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(100)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(100)
                .HasColumnName("document_number");
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .HasColumnName("gender");
            entity.Property(e => e.JsonData).HasColumnName("json_data");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.RoomAllocated)
                .HasMaxLength(100)
                .HasColumnName("room_allocated");
        });

        modelBuilder.Entity<HotelOfflineLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hotel_offline_logs_pkey");

            entity.ToTable("hotel_offline_logs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DocumentNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("document_number");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("type");
        });

        modelBuilder.Entity<HotelSimulator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hotel_simulator_pkey");

            entity.ToTable("hotel_simulator");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgeInYears)
                .HasColumnType("character varying")
                .HasColumnName("age_in_years");
            entity.Property(e => e.AgeOver18).HasColumnName("age_over_18");
            entity.Property(e => e.CreationDate)
                .HasColumnType("character varying")
                .HasColumnName("creation_date");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("document_number");
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("name");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.RoomAllocated)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("room_allocated");
        });

        modelBuilder.Entity<IssuerKey>(entity =>
        {
            entity.HasKey(e => e.IssuerId).HasName("issuer_key_pkey");

            entity.ToTable("issuer_key");

            entity.Property(e => e.IssuerId)
                .HasMaxLength(100)
                .HasColumnName("issuer_id");
            entity.Property(e => e.CertificateChain)
                .IsRequired()
                .HasColumnName("certificate_chain");
            entity.Property(e => e.IssuerCertificate)
                .IsRequired()
                .HasColumnName("issuer_certificate");
            entity.Property(e => e.IssuerKey1)
                .IsRequired()
                .HasColumnName("issuer_key");
        });

        modelBuilder.Entity<LoginDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("login_details_pkey");

            entity.ToTable("login_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConsentStatus)
                .HasMaxLength(255)
                .HasColumnName("consent_status");
            entity.Property(e => e.ConsentToken)
                .HasMaxLength(255)
                .HasColumnName("consent_token");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(255)
                .HasColumnName("created_on");
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email_id");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(255)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<ManualCreditsAllocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manual_credits_allocation_pkey");

            entity.ToTable("manual_credits_allocation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AllocationStatus)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("allocation_status");
            entity.Property(e => e.AmountReceived).HasColumnName("amount_received");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.InvoiceNumber)
                .HasColumnType("character varying")
                .HasColumnName("invoice_number");
            entity.Property(e => e.NoOfOnboardingCredits).HasColumnName("no_of_onboarding_credits");
            entity.Property(e => e.OnlinePaymentGateway)
                .HasColumnType("character varying")
                .HasColumnName("online_payment_gateway");
            entity.Property(e => e.OnlinePaymentGatewayReferenceNum)
                .HasColumnType("character varying")
                .HasColumnName("online_payment_gateway_reference_num");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrganisationId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("organisation_id");
            entity.Property(e => e.PaymentChannel)
                .HasColumnType("character varying")
                .HasColumnName("payment_channel");
            entity.Property(e => e.TotalEsealCredits).HasColumnName("total_eseal_credits");
            entity.Property(e => e.TotalSigningCredits).HasColumnName("total_signing_credits");
            entity.Property(e => e.TransactionReferenceId)
                .HasColumnType("character varying")
                .HasColumnName("transaction_reference_id");
        });

        modelBuilder.Entity<MapMethodOnboardingStep>(entity =>
        {
            entity.HasKey(e => e.MapMethodOnboardingStepId).HasName("map_method_onboarding_steps_pkey");

            entity.ToTable("map_method_onboarding_steps");

            entity.Property(e => e.MapMethodOnboardingStepId).HasColumnName("map_method_onboarding_step_id");
            entity.Property(e => e.AndriodDttThreshold).HasColumnName("andriod_dtt_threshold");
            entity.Property(e => e.AndroidTfliteThreshold).HasColumnName("android_tflite_threshold");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.IntegrationUrl)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("integration_url");
            entity.Property(e => e.IosDttThreshold).HasColumnName("ios_dtt_threshold");
            entity.Property(e => e.IosTfliteThreshold).HasColumnName("ios_tflite_threshold");
            entity.Property(e => e.MethodName)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("method_name");
            entity.Property(e => e.OnboardingStep)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("onboarding_step");
            entity.Property(e => e.OnboardingStepThreshold).HasColumnName("onboarding_step_threshold");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");

            entity.HasOne(d => d.OnboardingStepNavigation).WithMany(p => p.MapMethodOnboardingSteps)
                .HasPrincipalKey(p => p.OnboardingStep1)
                .HasForeignKey(d => d.OnboardingStep)
                .HasConstraintName("map_method_onboarding_steps_fk_1");

            entity.HasOne(d => d.Template).WithMany(p => p.MapMethodOnboardingSteps)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("map_method_onboarding_steps_fk");
        });

        modelBuilder.Entity<MoneyExchangeSimulation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("money_exchange_simulation_pkey");

            entity.ToTable("money_exchange_simulation");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('money_exchange_simulation_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.AmountToBeExchanged)
                .IsRequired()
                .HasColumnName("amount_to_be_exchanged");
            entity.Property(e => e.ApplicantName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("applicant_name");
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ExchangedAmount)
                .IsRequired()
                .HasColumnName("exchanged_amount");
            entity.Property(e => e.JsonData)
                .IsRequired()
                .HasColumnName("json_data");
            entity.Property(e => e.PassportDocument).HasColumnName("passport_document");
            entity.Property(e => e.PassportNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("passport_number");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PidDocument).HasColumnName("pid_document");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<MoneyTransfer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("money_transfer_pkey");

            entity.ToTable("money_transfer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgentEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("agent_email");
            entity.Property(e => e.AgentIdDocNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("agent_id_doc_number");
            entity.Property(e => e.AgentName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("agent_name");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.NotaryEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("notary_email");
            entity.Property(e => e.NotaryIdDocNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("notary_id_doc_number");
            entity.Property(e => e.NotaryName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("notary_name");
            entity.Property(e => e.PoaSignedDoc).HasColumnName("poa_signed_doc");
            entity.Property(e => e.PrincipalEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("principal_email");
            entity.Property(e => e.PrincipalIdDocNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("principal_id_doc_number");
            entity.Property(e => e.PrincipalName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("principal_name");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.ValidUpto)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("valid_upto");
        });

        modelBuilder.Entity<OnboardingAgent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("onboarding_agents_pkey");

            entity.ToTable("onboarding_agents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgentName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("agent_name");
            entity.Property(e => e.AgentUgpassEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("agent_ugpass_email");
            entity.Property(e => e.AgentUgpassMobileNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("agent_ugpass_mobile_number");
            entity.Property(e => e.AgentUgpassSuid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_ugpass_suid");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("device_id");
            entity.Property(e => e.FcmToken)
                .HasMaxLength(256)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("fcm_token");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OnboardingDocument>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("onboarding_documents_pkey");

            entity.ToTable("onboarding_documents");

            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("description");
            entity.Property(e => e.ExpireDate)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("expire_date");
            entity.Property(e => e.Label)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("label");
            entity.Property(e => e.MimeType)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("mime_type");
            entity.Property(e => e.ModifiedDate)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("modified_date");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Uuid)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("uuid");
            entity.Property(e => e.Version)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("version");
        });

        modelBuilder.Entity<OnboardingLiveliness>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SubscriberUid }).HasName("onboarding_liveliness_pkey");

            entity.ToTable("onboarding_liveliness");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(50)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.File)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("file");
            entity.Property(e => e.RecordedGeoLocation)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("recorded_geo_location");
            entity.Property(e => e.RecordedTime)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("recorded_time");
            entity.Property(e => e.TypeOfService)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("type_of_service");
            entity.Property(e => e.Url)
                .HasMaxLength(600)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("url");
            entity.Property(e => e.VerificationFirst)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("verification_first");
            entity.Property(e => e.VerificationSecond)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("verification_second");
            entity.Property(e => e.VerificationThird)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("verification_third");
        });

        modelBuilder.Entity<OnboardingMethod>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("onboarding_methods");

            entity.HasIndex(e => e.OnboardingMethod1, "onboarding_methods_onboarding_method_key").IsUnique();

            entity.Property(e => e.IdDocumentType)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("id_document_type");
            entity.Property(e => e.LevelOfAssurance)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("level_of_assurance");
            entity.Property(e => e.OnboardingMethod1)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("onboarding_method");
            entity.Property(e => e.OnboardingMethodId).HasColumnName("onboarding_method_id");
            entity.Property(e => e.SubscriberType)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("subscriber_type");
        });

        modelBuilder.Entity<OnboardingStep>(entity =>
        {
            entity.HasKey(e => e.OnboardingStepId).HasName("onboarding_steps_pkey");

            entity.ToTable("onboarding_steps");

            entity.HasIndex(e => e.OnboardingStep1, "onboarding_steps_onboarding_step_key").IsUnique();

            entity.Property(e => e.OnboardingStepId)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("onboarding_step_id");
            entity.Property(e => e.AndriodDttThreshold).HasColumnName("andriod_dtt_threshold");
            entity.Property(e => e.AndroidTfliteThreshold).HasColumnName("android_tflite_threshold");
            entity.Property(e => e.IntegrationUrl)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("integration_url");
            entity.Property(e => e.IosDttThreshold).HasColumnName("ios_dtt_threshold");
            entity.Property(e => e.IosTfliteThreshold).HasColumnName("ios_tflite_threshold");
            entity.Property(e => e.OnboardingStep1)
                .IsRequired()
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("onboarding_step");
            entity.Property(e => e.OnboardingStepDisplayName)
                .HasMaxLength(32)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("onboarding_step_display_name");
            entity.Property(e => e.OnboardingStepThreshold).HasColumnName("onboarding_step_threshold");
        });

        modelBuilder.Entity<OnboardingStepDetail>(entity =>
        {
            entity.HasKey(e => e.StepId).HasName("onboarding_step_details_pkey");

            entity.ToTable("onboarding_step_details");

            entity.Property(e => e.StepId)
                .ValueGeneratedNever()
                .HasColumnName("step_id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.StepDescription).HasColumnName("step_description");
            entity.Property(e => e.StepName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("step_name");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OrgApprovalSignedDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_approval_signed_data_pkey");

            entity.ToTable("org_approval_signed_data");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovalStatus)
                .HasMaxLength(50)
                .HasColumnName("approval_status");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.Formid).HasColumnName("formid");
            entity.Property(e => e.OrginalData).HasColumnName("orginal_data");
            entity.Property(e => e.SignedData).HasColumnName("signed_data");
        });

        modelBuilder.Entity<OrgBucket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_buckets_pkey");

            entity.ToTable("org_buckets");

            entity.HasIndex(e => e.BucketId, "org_buckets_bucket_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Additionaldsremaining).HasColumnName("additionaldsremaining");
            entity.Property(e => e.Additionaledsremaining).HasColumnName("additionaledsremaining");
            entity.Property(e => e.BucketConfigurationId).HasColumnName("bucket_configuration_id");
            entity.Property(e => e.BucketId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("bucket_id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.LastSignatoryId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("last_signatory_id");
            entity.Property(e => e.PaymentRecieved).HasColumnName("payment_recieved");
            entity.Property(e => e.PaymentRecievedOn)
                .HasColumnType("character varying")
                .HasColumnName("payment_recieved_on");
            entity.Property(e => e.SponsorId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("sponsor_id");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TotalDigitalSignatures).HasColumnName("total_digital_signatures");
            entity.Property(e => e.TotalEseal).HasColumnName("total_eseal");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.BucketConfiguration).WithMany(p => p.OrgBuckets)
                .HasForeignKey(d => d.BucketConfigurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("org_buckets_fk");
        });

        modelBuilder.Entity<OrgBucketsConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_buckets_config_pkey");

            entity.ToTable("org_buckets_config");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalDs).HasColumnName("additional_ds");
            entity.Property(e => e.AdditionalEds).HasColumnName("additional_eds");
            entity.Property(e => e.AppId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("app_id");
            entity.Property(e => e.BucketClosingMessage)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("bucket_closing_message");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("label");
            entity.Property(e => e.OrgId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("org_id");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("org_name");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OrgCeoDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_ceo_details_pkey");

            entity.ToTable("org_ceo_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CeoEmail)
                .HasMaxLength(100)
                .HasColumnName("ceo_email");
            entity.Property(e => e.CeoIdDocumentNumber)
                .HasMaxLength(100)
                .HasColumnName("ceo_id_document_number");
            entity.Property(e => e.CeoName)
                .HasMaxLength(100)
                .HasColumnName("ceo_name");
            entity.Property(e => e.CeoPanTaxNum)
                .HasMaxLength(100)
                .HasColumnName("ceo_pan_tax_num");
            entity.Property(e => e.OrgOnboardingFormId).HasColumnName("org_onboarding_form_id");
            entity.Property(e => e.OrgUid)
                .HasMaxLength(100)
                .HasColumnName("org_uid");

            entity.HasOne(d => d.OrgOnboardingForm).WithMany(p => p.OrgCeoDetails)
                .HasForeignKey(d => d.OrgOnboardingFormId)
                .HasConstraintName("org_ceo_details_fk");
        });

        modelBuilder.Entity<OrgCertificateEmailCounter>(entity =>
        {
            entity.HasKey(e => e.OrganizationUid).HasName("org_certificate_email_counter_pkey");

            entity.ToTable("org_certificate_email_counter");

            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.Counter0Day)
                .HasMaxLength(45)
                .HasColumnName("counter_0_day");
            entity.Property(e => e.Counter10Day)
                .HasMaxLength(45)
                .HasColumnName("counter_10_day");
            entity.Property(e => e.Counter15Day)
                .HasMaxLength(45)
                .HasColumnName("counter_15_day");
            entity.Property(e => e.Counter5Day)
                .HasMaxLength(45)
                .HasColumnName("counter_5_day");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(100)
                .HasColumnName("organization_name");
            entity.Property(e => e.SpocUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
        });

        modelBuilder.Entity<OrgClientAppConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_client_app_config_pkey");

            entity.ToTable("org_client_app_config");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId)
                .HasMaxLength(100)
                .HasColumnName("app_id");
            entity.Property(e => e.ConfigValue)
                .HasMaxLength(100)
                .HasColumnName("config_value");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.OrgId)
                .HasMaxLength(100)
                .HasColumnName("org_id");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OrgESeal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_e_seal_pkey");

            entity.ToTable("org_e_seal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.ESealApplyOrRenewConsent).HasColumnName("e_seal_apply_or_renew_consent");
            entity.Property(e => e.ESealAuthorisationLetter).HasColumnName("e_seal_authorisation_letter");
            entity.Property(e => e.ESealLastIssuedOn)
                .HasColumnType("character varying")
                .HasColumnName("e_seal_last_issued_on");
            entity.Property(e => e.ESealLogo).HasColumnName("e_seal_logo");
            entity.Property(e => e.ESealPaymentReferenceId)
                .HasMaxLength(100)
                .HasColumnName("e_seal_payment_reference_id");
            entity.Property(e => e.ESealValidUpTo)
                .HasColumnType("character varying")
                .HasColumnName("e_seal_valid_up_to");
            entity.Property(e => e.OrgOnboardingFormId).HasColumnName("org_onboarding_form_id");
            entity.Property(e => e.UpdtaedOn)
                .HasColumnType("character varying")
                .HasColumnName("updtaed_on");

            entity.HasOne(d => d.OrgOnboardingForm).WithMany(p => p.OrgESeals)
                .HasForeignKey(d => d.OrgOnboardingFormId)
                .HasConstraintName("org_e_seal_fk");
        });

        modelBuilder.Entity<OrgEmailDomain>(entity =>
        {
            entity.HasKey(e => e.OrgDomainId).HasName("org_email_domains_pkey");

            entity.ToTable("org_email_domains");

            entity.Property(e => e.OrgDomainId).HasColumnName("org_domain_id");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.EmailDomain)
                .HasMaxLength(100)
                .HasColumnName("email_domain");
            entity.Property(e => e.OrganizationUid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrgEmailDomains)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("org_email_domains_fk");
        });

        modelBuilder.Entity<OrgFinancialAuditorDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_financial_auditor_details_pkey");

            entity.ToTable("org_financial_auditor_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.FinancialAuditorIdDocumentNumber)
                .HasMaxLength(100)
                .HasColumnName("financial_auditor_id_document_number");
            entity.Property(e => e.FinancialAuditorLicenseNum)
                .HasMaxLength(100)
                .HasColumnName("financial_auditor_license_num");
            entity.Property(e => e.FinancialAuditorName)
                .HasMaxLength(100)
                .HasColumnName("financial_auditor_name");
            entity.Property(e => e.FinancialAuditorTinNumber)
                .HasMaxLength(100)
                .HasColumnName("financial_auditor_tin_number");
            entity.Property(e => e.FinancialAuditorUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("financial_auditor_ugpass_email");
            entity.Property(e => e.OrgOnboardingFormId).HasColumnName("org_onboarding_form_id");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");

            entity.HasOne(d => d.OrgOnboardingForm).WithMany(p => p.OrgFinancialAuditorDetails)
                .HasForeignKey(d => d.OrgOnboardingFormId)
                .HasConstraintName("org_financial_auditor_details_fk");
        });

        modelBuilder.Entity<OrgInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("org_info");

            entity.Property(e => e.AuthorizedLetterForSignatories).HasColumnName("authorized_letter_for_signatories");
            entity.Property(e => e.CerificateExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(45)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CorporateOfficeAddress)
                .HasMaxLength(500)
                .HasColumnName("corporate_office_address");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.ESealImage).HasColumnName("e_seal_image");
            entity.Property(e => e.EnablePostPaidOption).HasColumnName("enable_post_paid_option");
            entity.Property(e => e.IncorporationFile).HasColumnName("incorporation_file");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrganizationDetailsId).HasColumnName("organization_details_id");
            entity.Property(e => e.OrganizationEmail)
                .HasMaxLength(100)
                .HasColumnName("organization_email");
            entity.Property(e => e.OrganizationSegments)
                .HasMaxLength(100)
                .HasColumnName("organization_segments");
            entity.Property(e => e.OrganizationStatus)
                .HasMaxLength(100)
                .HasColumnName("organization_status");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.OtherEsealDocument).HasColumnName("other_eseal_document");
            entity.Property(e => e.OtherLegalDocument).HasColumnName("other_legal_document");
            entity.Property(e => e.SignedPdf).HasColumnName("signed_pdf");
            entity.Property(e => e.SpocUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.TaxFile).HasColumnName("tax_file");
            entity.Property(e => e.TaxNo)
                .HasMaxLength(100)
                .HasColumnName("tax_no");
            entity.Property(e => e.UniqueRegdNo)
                .HasMaxLength(100)
                .HasColumnName("unique_regd_no");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OrgSignatureTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_signature_templates_pkey");

            entity.ToTable("org_signature_templates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrgSignatureTemplates)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("org_signature_templates_fk");
        });

        modelBuilder.Entity<OrgSubscriberEmail>(entity =>
        {
            entity.HasKey(e => e.OrgContactsId).HasName("org_subscriber_email_pkey");

            entity.ToTable("org_subscriber_email");

            entity.Property(e => e.OrgContactsId).HasColumnName("org_contacts_id");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.EmployeeEmail)
                .HasMaxLength(255)
                .HasColumnName("employee_email");
            entity.Property(e => e.IsBulkSign).HasColumnName("is_bulk_sign");
            entity.Property(e => e.IsDelegate).HasColumnName("is_delegate");
            entity.Property(e => e.IsDigitalForm).HasColumnName("is_digital_form");
            entity.Property(e => e.IsEsealPreparatory).HasColumnName("is_eseal_preparatory");
            entity.Property(e => e.IsEsealSignatory).HasColumnName("is_eseal_signatory");
            entity.Property(e => e.IsOrgSignatory).HasColumnName("is_org_signatory");
            entity.Property(e => e.IsTemplate).HasColumnName("is_template");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.NationalIdNumber)
                .HasMaxLength(100)
                .HasColumnName("national_id_number");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(100)
                .HasColumnName("passport_number");
            entity.Property(e => e.ShortSignature).HasColumnName("short_signature");
            entity.Property(e => e.SignaturePhoto).HasColumnName("signature_photo");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasColumnName("status");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(100)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.UgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("ugpass_email");
            entity.Property(e => e.UgpassUserLinkApproved).HasColumnName("ugpass_user_link_approved");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrgSubscriberEmails)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("org_subscriber_email_new_fk");
        });

        modelBuilder.Entity<OrgSubscriberEmailOld>(entity =>
        {
            entity.HasKey(e => e.OrgContactsId).HasName("org_subscriber_email_old_pkey");

            entity.ToTable("org_subscriber_email_old");

            entity.Property(e => e.OrgContactsId).HasColumnName("org_contacts_id");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.IsEsealPreparatory).HasColumnName("is_eseal_preparatory");
            entity.Property(e => e.IsEsealSignatory).HasColumnName("is_eseal_signatory");
            entity.Property(e => e.IsOrgSignatory).HasColumnName("is_org_signatory");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.SignaturePhoto).HasColumnName("signature_photo");
            entity.Property(e => e.SubEmailList).HasColumnName("sub_email_list");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrgSubscriberEmailOlds)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("org_subscriber_email_fk");
        });

        modelBuilder.Entity<OrgWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_wallet_pkey");

            entity.ToTable("org_wallet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.OrgOnboardingFormId).HasColumnName("org_onboarding_form_id");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.WalletConsent)
                .IsRequired()
                .HasColumnName("wallet_consent");
            entity.Property(e => e.WalletIssuesOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("wallet_issues_on");
            entity.Property(e => e.WalletTransactionReferenceId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("wallet_transaction_reference_id");
            entity.Property(e => e.WalletValidUpto)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("wallet_valid_upto");
        });

        modelBuilder.Entity<OrganisationCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organisation_categories_pkey");

            entity.ToTable("organisation_categories");

            entity.HasIndex(e => e.CategoryName, "organisation_categories_category_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.LabelName)
                .HasMaxLength(100)
                .HasColumnName("label_name");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OrganisationFieldMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organisation_field_mappings_pkey");

            entity.ToTable("organisation_field_mappings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .HasColumnName("field_name");
            entity.Property(e => e.LabelName)
                .HasMaxLength(50)
                .HasColumnName("label_name");
            entity.Property(e => e.Mandatory).HasColumnName("mandatory");
            entity.Property(e => e.Modifiable).HasColumnName("modifiable");
            entity.Property(e => e.OrgCategoryId).HasColumnName("org_category_id");
            entity.Property(e => e.OrgFormFieldId).HasColumnName("org_form_field_id");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
            entity.Property(e => e.Visibility).HasColumnName("visibility");

            entity.HasOne(d => d.OrgCategory).WithMany(p => p.OrganisationFieldMappings)
                .HasForeignKey(d => d.OrgCategoryId)
                .HasConstraintName("organisation_field_mappings_org_category_fk");

            entity.HasOne(d => d.OrgFormField).WithMany(p => p.OrganisationFieldMappings)
                .HasForeignKey(d => d.OrgFormFieldId)
                .HasConstraintName("organisation_field_mappings_org_form_field_fk");
        });

        modelBuilder.Entity<OrganisationFormField>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organisation_form_fields_pkey");

            entity.ToTable("organisation_form_fields");

            entity.HasIndex(e => e.FieldName, "organisation_form_fields_field_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.FieldName)
                .HasMaxLength(100)
                .HasColumnName("field_name");
            entity.Property(e => e.LabelName)
                .HasMaxLength(100)
                .HasColumnName("label_name");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OrganisationOnboardingForm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organisation_onboarding_forms_pkey");

            entity.ToTable("organisation_onboarding_forms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovalLetter).HasColumnName("approval_letter");
            entity.Property(e => e.Checksum).HasColumnName("checksum");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.Flag).HasColumnName("flag");
            entity.Property(e => e.ObFormStatus)
                .HasMaxLength(100)
                .HasColumnName("ob_form_status");
            entity.Property(e => e.OrgAddedByAdmin).HasColumnName("org_added_by_admin");
            entity.Property(e => e.OrgApprovalStatus)
                .HasMaxLength(100)
                .HasColumnName("org_approval_status");
            entity.Property(e => e.OrgCategory)
                .HasMaxLength(100)
                .HasColumnName("org_category");
            entity.Property(e => e.OrgCategoryId)
                .HasMaxLength(10)
                .HasColumnName("org_category_id");
            entity.Property(e => e.OrgCorporateAddress)
                .HasMaxLength(256)
                .HasColumnName("org_corporate_address");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrgObRejectedReason)
                .HasMaxLength(500)
                .HasColumnName("org_ob_rejected_reason");
            entity.Property(e => e.OrgOfficialContactNum)
                .HasMaxLength(100)
                .HasColumnName("org_official_contact_num");
            entity.Property(e => e.OrgRegIdNum)
                .HasMaxLength(100)
                .HasColumnName("org_reg_id_num");
            entity.Property(e => e.OrgTanTaxNum)
                .HasMaxLength(100)
                .HasColumnName("org_tan_tax_num");
            entity.Property(e => e.OrgUid)
                .HasMaxLength(100)
                .HasColumnName("org_uid");
            entity.Property(e => e.OrgWebUrl)
                .HasMaxLength(100)
                .HasColumnName("org_web_url");
            entity.Property(e => e.OtpVerification)
                .HasMaxLength(100)
                .HasColumnName("otp_verification");
            entity.Property(e => e.SignApprByBrmStaff)
                .HasMaxLength(100)
                .HasColumnName("sign_appr_by_brm_staff");
            entity.Property(e => e.SignedChecksum).HasColumnName("signed_checksum");
            entity.Property(e => e.SpocEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_email");
            entity.Property(e => e.SpocSuid)
                .HasMaxLength(100)
                .HasColumnName("spoc_suid");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
            entity.Property(e => e.UrsbCert).HasColumnName("ursb_cert");
        });

        modelBuilder.Entity<OrganisationSpocDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organisation_spoc_details_pkey");

            entity.ToTable("organisation_spoc_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.OrgOnboardingFormId).HasColumnName("org_onboarding_form_id");
            entity.Property(e => e.OrgUid)
                .HasMaxLength(100)
                .HasColumnName("org_uid");
            entity.Property(e => e.SpocFaceCaptured).HasColumnName("spoc_face_captured");
            entity.Property(e => e.SpocFaceFromUgpass).HasColumnName("spoc_face_from_ugpass");
            entity.Property(e => e.SpocFaceMatchStatus).HasColumnName("spoc_face_match_status");
            entity.Property(e => e.SpocIdDocumentNumber)
                .HasMaxLength(100)
                .HasColumnName("spoc_id_document_number");
            entity.Property(e => e.SpocName)
                .HasMaxLength(100)
                .HasColumnName("spoc_name");
            entity.Property(e => e.SpocOfficeEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_office_email");
            entity.Property(e => e.SpocOtpVerfyStatus).HasColumnName("spoc_otp_verfy_status");
            entity.Property(e => e.SpocSuid)
                .HasMaxLength(100)
                .HasColumnName("spoc_suid");
            entity.Property(e => e.SpocTaxNum)
                .HasMaxLength(100)
                .HasColumnName("spoc_tax_num");
            entity.Property(e => e.SpocUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.SpocUgpassMobNum)
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_mob_num");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");

            entity.HasOne(d => d.OrgOnboardingForm).WithMany(p => p.OrganisationSpocDetails)
                .HasForeignKey(d => d.OrgOnboardingFormId)
                .HasConstraintName("organisation_spoc_details_fk");
        });

        modelBuilder.Entity<OrganizationAdditionalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_additional_details_pkey");

            entity.ToTable("organization_additional_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.EnablePostPaidOption).HasColumnName("enable_post_paid_option");
            entity.Property(e => e.OrganizationId)
                .IsRequired()
                .HasColumnName("organization_id");
            entity.Property(e => e.SpocUgpassEmail)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.UpdatedOn)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OrganizationCertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateSerialNumber).HasName("organization_certificates_pkey");

            entity.ToTable("organization_certificates");

            entity.HasIndex(e => e.PkiKeyId, "organization_certificates_pki_key_id_key").IsUnique();

            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(200)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CerificateExpiryDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateData)
                .IsRequired()
                .HasMaxLength(4096)
                .HasColumnName("certificate_data");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateStatus)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CertificateType)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("certificate_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.OrganizationUid)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("organization_uid");
            entity.Property(e => e.PkiKeyId)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("pki_key_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("remarks");
            entity.Property(e => e.TransactionReferenceId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("transaction_reference_id");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("NULL::timestamp without time zone")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.WrappedKey)
                .IsRequired()
                .HasMaxLength(5000)
                .HasColumnName("wrapped_key");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrganizationCertificates)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("organization_certificates_fk");
        });

        modelBuilder.Entity<OrganizationCertificateLifeCycle>(entity =>
        {
            entity.HasKey(e => e.OrganizationCertificateLifeCycleId).HasName("organization_certificate_life_cycle_pkey");

            entity.ToTable("organization_certificate_life_cycle");

            entity.Property(e => e.OrganizationCertificateLifeCycleId)
                .HasDefaultValueSql("nextval('organization_certificate_life_organization_certificate_life_seq'::regclass)")
                .HasColumnName("organization_certificate_life_cycle_id");
            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(32)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("certificate_status");
            entity.Property(e => e.CertificateType)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("certificate_type");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("NULL::timestamp without time zone")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.OrganizationUid)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("organization_uid");
            entity.Property(e => e.RevocationReason)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("revocation_reason");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrganizationCertificateLifeCycles)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("organization_certificate_life_cycle_fk");
        });

        modelBuilder.Entity<OrganizationDetail>(entity =>
        {
            entity.HasKey(e => e.OrganizationDetailsId).HasName("organization_details_pkey");

            entity.ToTable("organization_details");

            entity.HasIndex(e => e.OrganizationUid, "organization_details_organization_uid_key").IsUnique();

            entity.Property(e => e.OrganizationDetailsId).HasColumnName("organization_details_id");
            entity.Property(e => e.AgentUrl)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("agent_url");
            entity.Property(e => e.AuthorizedLetterForSignatories).HasColumnName("authorized_letter_for_signatories");
            entity.Property(e => e.CorporateOfficeAddress)
                .HasMaxLength(500)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("corporate_office_address");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.ESealImage).HasColumnName("e_seal_image");
            entity.Property(e => e.EnablePostPaidOption).HasColumnName("enable_post_paid_option");
            entity.Property(e => e.IncorporationFile).HasColumnName("incorporation_file");
            entity.Property(e => e.ManageByAdmin)
                .HasDefaultValue(true)
                .HasColumnName("manage_by_admin");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("org_name");
            entity.Property(e => e.OrganizationEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_email");
            entity.Property(e => e.OrganizationSegments)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_segments");
            entity.Property(e => e.OrganizationStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_status");
            entity.Property(e => e.OrganizationUid)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_uid");
            entity.Property(e => e.OtherEsealDocument).HasColumnName("other_eseal_document");
            entity.Property(e => e.OtherLegalDocument).HasColumnName("other_legal_document");
            entity.Property(e => e.SignedPdf).HasColumnName("signed_pdf");
            entity.Property(e => e.SpocUgpassEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.TaxFile).HasColumnName("tax_file");
            entity.Property(e => e.TaxNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("tax_no");
            entity.Property(e => e.UniqueRegdNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("unique_regd_no");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<OrganizationDirector>(entity =>
        {
            entity.HasKey(e => e.OrganizationDirectorsId).HasName("organization_directors_pkey");

            entity.ToTable("organization_directors");

            entity.Property(e => e.OrganizationDirectorsId).HasColumnName("organization_directors_id");
            entity.Property(e => e.DirectorsEmails).HasColumnName("directors_emails");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_uid");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrganizationDirectors)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("organization_directors_fk");
        });

        modelBuilder.Entity<OrganizationDocument>(entity =>
        {
            entity.HasKey(e => e.OrganizationDocumentsId).HasName("organization_documents_pkey");

            entity.ToTable("organization_documents");

            entity.Property(e => e.OrganizationDocumentsId).HasColumnName("organization_documents_id");
            entity.Property(e => e.AnyOtherDoc)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("any_other_doc");
            entity.Property(e => e.ESealImage)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("e_seal_image");
            entity.Property(e => e.IncorporationFile)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("incorporation_file");
            entity.Property(e => e.OrgDetailsPdf)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("org_details_pdf");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_uid");
            entity.Property(e => e.TaxFile)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("tax_file");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrganizationDocuments)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("organization_documents_fk");
        });

        modelBuilder.Entity<OrganizationDocumentCheck>(entity =>
        {
            entity.HasKey(e => e.OrganizationdocCheckId).HasName("organization_document_check_pkey");

            entity.ToTable("organization_document_check");

            entity.Property(e => e.OrganizationdocCheckId).HasColumnName("organizationdoc_check_id");
            entity.Property(e => e.DocumentCheckBox)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("document_check_box");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_uid");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrganizationDocumentChecks)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("organization_document_check_fk");
        });

        modelBuilder.Entity<OrganizationPaymentsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_payments_history_pkey");

            entity.ToTable("organization_payments_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.InvoiceNumber)
                .HasColumnType("character varying")
                .HasColumnName("invoice_number");
            entity.Property(e => e.OrganizationId)
                .HasColumnType("character varying")
                .HasColumnName("organization_id");
            entity.Property(e => e.PaymentChannel)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("payment_channel");
            entity.Property(e => e.PaymentInfo)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("payment_info");
            entity.Property(e => e.TotalAmountPaid).HasColumnName("total_amount_paid");
            entity.Property(e => e.TransactionReferenceId)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("transaction_reference_id");
        });

        modelBuilder.Entity<OrganizationPricingSlabDefinition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_pricing_slab_definitions_pkey");

            entity.ToTable("organization_pricing_slab_definitions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.OrganizationId)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("organization_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.VolumeRangeFrom).HasColumnName("volume_range_from");
            entity.Property(e => e.VolumeRangeTo).HasColumnName("volume_range_to");

            entity.HasOne(d => d.Service).WithMany(p => p.OrganizationPricingSlabDefinitions)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("pricing_slab_definitions_fk");
        });

        modelBuilder.Entity<OrganizationPrivilege>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_privileges_pkey");

            entity.ToTable("organization_privileges");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("character varying")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrganizationId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("organization_id");
            entity.Property(e => e.OrganizationName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("organization_name");
            entity.Property(e => e.Privilege)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("privilege");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("status");
        });

        modelBuilder.Entity<OrganizationStatus>(entity =>
        {
            entity.HasKey(e => e.OrganizationStatusId).HasName("organization_status_pkey");

            entity.ToTable("organization_status");

            entity.Property(e => e.OrganizationStatusId).HasColumnName("organization_status_id");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.OrganizationStatus1)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_status");
            entity.Property(e => e.OrganizationStatusDescription)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_status_description");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("organization_uid");
            entity.Property(e => e.OtpVerifiedStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("otp_verified_status");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrganizationStatuses)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("organization_status_fk");
        });

        modelBuilder.Entity<OrganizationUsageReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_usage_reports_pkey");

            entity.ToTable("organization_usage_reports");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.OrganizationId)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("organization_id");
            entity.Property(e => e.PdfReport)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("pdf_report");
            entity.Property(e => e.ReportMonth)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("report_month");
            entity.Property(e => e.ReportYear)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("report_year");
            entity.Property(e => e.TotalInvoiceAmount).HasColumnName("total_invoice_amount");
        });

        modelBuilder.Entity<OrganizationWrappedKey>(entity =>
        {
            entity.HasKey(e => e.CertificateSerialNumber).HasName("organization_wrapped_key_pkey");

            entity.ToTable("organization_wrapped_key");

            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(100)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.WrappedKey)
                .HasMaxLength(5000)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("wrapped_key");
        });

        modelBuilder.Entity<Organizationsview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("organizationsview");

            entity.Property(e => e.AuthorizedLetterForSignatories).HasColumnName("authorized_letter_for_signatories");
            entity.Property(e => e.CertificateExpiryDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("certificate_expiry_date");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(45)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CorporateOfficeAddress)
                .HasMaxLength(500)
                .HasColumnName("corporate_office_address");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.ESealCreatedDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("e_seal_created_date");
            entity.Property(e => e.ESealImage).HasColumnName("e_seal_image");
            entity.Property(e => e.EnablePostPaidOption).HasColumnName("enable_post_paid_option");
            entity.Property(e => e.IncorporationFile).HasColumnName("incorporation_file");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrganizationDetailsId).HasColumnName("organization_details_id");
            entity.Property(e => e.OrganizationEmail)
                .HasMaxLength(100)
                .HasColumnName("organization_email");
            entity.Property(e => e.OrganizationStatus)
                .HasMaxLength(100)
                .HasColumnName("organization_status");
            entity.Property(e => e.OtherEsealDocument).HasColumnName("other_eseal_document");
            entity.Property(e => e.OtherLegalDocument).HasColumnName("other_legal_document");
            entity.Property(e => e.Ouid)
                .HasMaxLength(100)
                .HasColumnName("ouid");
            entity.Property(e => e.SpocUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.TaxFile).HasColumnName("tax_file");
            entity.Property(e => e.TaxNo)
                .HasMaxLength(100)
                .HasColumnName("tax_no");
            entity.Property(e => e.UniqueRegdNo)
                .HasMaxLength(100)
                .HasColumnName("unique_regd_no");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
            entity.Property(e => e.WalletCertificateExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("wallet_certificate_expiry_date");
            entity.Property(e => e.WalletCertificateIssueDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("wallet_certificate_issue_date");
        });

        modelBuilder.Entity<PaymentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_history_pkey");

            entity.ToTable("payment_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.CustomerType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("customer_type");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Mobile)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("mobile");
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(100)
                .HasColumnName("organization_id");
            entity.Property(e => e.PaymentCategory)
                .HasMaxLength(100)
                .HasColumnName("payment_category");
            entity.Property(e => e.PaymentInfo).HasColumnName("payment_info");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentReferenceTransactionId)
                .HasMaxLength(100)
                .HasColumnName("payment_reference_transaction_id");
            entity.Property(e => e.PaymentResponse).HasColumnName("payment_response");
            entity.Property(e => e.PlatformFee)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("platform_fee");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.TransactionFee)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("transaction_fee");
            entity.Property(e => e.TransactionId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("transaction_id");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(10)
                .HasColumnName("transaction_type");
            entity.Property(e => e.Vat)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("vat");
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_transaction_pkey");

            entity.ToTable("payment_transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(38)
                .HasColumnName("amount");
            entity.Property(e => e.Currency)
                .HasMaxLength(255)
                .HasColumnName("currency");
            entity.Property(e => e.EntitlementId).HasColumnName("entitlement_id");
            entity.Property(e => e.PaymentReference)
                .HasMaxLength(255)
                .HasColumnName("payment_reference");
            entity.Property(e => e.RegistrantId).HasColumnName("registrant_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("transaction_date");

            entity.HasOne(d => d.Entitlement).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.EntitlementId)
                .HasConstraintName("payment_transaction_entitlements_fk");

            entity.HasOne(d => d.Registrant).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.RegistrantId)
                .HasConstraintName("payment_transaction_registrant_details_fk");
        });

        modelBuilder.Entity<PaynovaReturnTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("paynova_return_transaction_pkey");

            entity.ToTable("paynova_return_transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(50)
                .HasColumnName("card_number");
            entity.Property(e => e.ClientExtra)
                .HasMaxLength(100)
                .HasColumnName("client_extra");
            entity.Property(e => e.ClientReference)
                .HasMaxLength(100)
                .HasColumnName("client_reference");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.PayerCustomerCode)
                .HasMaxLength(50)
                .HasColumnName("payer_customer_code");
            entity.Property(e => e.PaymentReference)
                .HasMaxLength(100)
                .HasColumnName("payment_reference");
            entity.Property(e => e.PaymentTransactionInstrument).HasColumnName("payment_transaction_instrument");
            entity.Property(e => e.PaymentTransactionInstrumentName)
                .HasMaxLength(100)
                .HasColumnName("payment_transaction_instrument_name");
            entity.Property(e => e.PaymentTransactionReference)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("payment_transaction_reference");
            entity.Property(e => e.RawPayload).HasColumnName("raw_payload");
            entity.Property(e => e.ReconciliationDate).HasColumnName("reconciliation_date");
            entity.Property(e => e.ResponseCode).HasColumnName("response_code");
            entity.Property(e => e.ResponseDateTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("response_date_time");
            entity.Property(e => e.ResponseDescription)
                .HasMaxLength(255)
                .HasColumnName("response_description");
            entity.Property(e => e.ServiceAmount)
                .HasPrecision(10)
                .HasColumnName("service_amount");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .HasColumnName("source");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StatusName)
                .HasMaxLength(100)
                .HasColumnName("status_name");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10)
                .HasColumnName("total_amount");
            entity.Property(e => e.TotalCharges)
                .HasPrecision(10)
                .HasColumnName("total_charges");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("transaction_date");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<PhotoFeature>(entity =>
        {
            entity.HasKey(e => e.Suid).HasName("photo_features_pkey");

            entity.ToTable("photo_features");

            entity.Property(e => e.Suid)
                .HasMaxLength(100)
                .HasColumnName("suid");
            entity.Property(e => e.PhotoFeatures).HasColumnName("photo_features");
            entity.Property(e => e.SubscriberDataJson).HasColumnName("subscriber_data_json");
            entity.Property(e => e.SubscriberName)
                .HasMaxLength(100)
                .HasColumnName("subscriber_name");
            entity.Property(e => e.SubscriberPhoto)
                .IsRequired()
                .HasColumnName("subscriber_photo");
        });

        modelBuilder.Entity<Pid>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pid_pkey");

            entity.ToTable("pid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(100)
                .HasColumnName("card_number");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .HasColumnName("country_code");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(50)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(100)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.MobNo)
                .HasMaxLength(20)
                .HasColumnName("mob_no");
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .HasColumnName("nationality");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PidDocument).HasColumnName("pid_document");
            entity.Property(e => e.PidExpiryDate)
                .HasMaxLength(50)
                .HasColumnName("pid_expiry_date");
            entity.Property(e => e.PidIssueDate)
                .HasMaxLength(50)
                .HasColumnName("pid_issue_date");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<PoaCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("poa_credentials_pkey");

            entity.ToTable("poa_credentials");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgentEmail)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_email");
            entity.Property(e => e.AgentIdDocNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_id_doc_number");
            entity.Property(e => e.AgentName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_name");
            entity.Property(e => e.AgentPhoto).HasColumnName("agent_photo");
            entity.Property(e => e.AgentSuid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_suid");
            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.PoaCredential1)
                .IsRequired()
                .HasColumnName("poa_credential");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<PoaCredentialRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("poa_credential_requests_pkey");

            entity.ToTable("poa_credential_requests");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalFields).HasColumnName("additional_fields");
            entity.Property(e => e.AgentEmail)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_email");
            entity.Property(e => e.AgentIdDocNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_id_doc_number");
            entity.Property(e => e.AgentName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_name");
            entity.Property(e => e.AgentSuid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("agent_suid");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.NotaryEmail)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("notary_email");
            entity.Property(e => e.NotaryIdDocNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("notary_id_doc_number");
            entity.Property(e => e.NotaryName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("notary_name");
            entity.Property(e => e.NotarySuid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("notary_suid");
            entity.Property(e => e.PoaDocSigned).HasColumnName("poa_doc_signed");
            entity.Property(e => e.PoaRequestForm).HasColumnName("poa_request_form");
            entity.Property(e => e.PrincipleEmail)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("principle_email");
            entity.Property(e => e.PrincipleIdDocNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("principle_id_doc_number");
            entity.Property(e => e.PrincipleName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("principle_name");
            entity.Property(e => e.PrinciplePhoto).HasColumnName("principle_photo");
            entity.Property(e => e.PrincipleSuid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("principle_suid");
            entity.Property(e => e.Scope)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("scope");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
            entity.Property(e => e.ValidUpto).HasColumnName("valid_upto");
        });

        modelBuilder.Entity<PoaCredentialRequestsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("poa_credential_requests_view");

            entity.Property(e => e.AgentEmail)
                .HasMaxLength(100)
                .HasColumnName("agent_email");
            entity.Property(e => e.AgentIdDocNumber)
                .HasMaxLength(100)
                .HasColumnName("agent_id_doc_number");
            entity.Property(e => e.AgentName)
                .HasMaxLength(100)
                .HasColumnName("agent_name");
            entity.Property(e => e.AgentSuid)
                .HasMaxLength(100)
                .HasColumnName("agent_suid");
            entity.Property(e => e.NotaryEmail)
                .HasMaxLength(100)
                .HasColumnName("notary_email");
            entity.Property(e => e.NotaryIdDocNumber)
                .HasMaxLength(100)
                .HasColumnName("notary_id_doc_number");
            entity.Property(e => e.NotaryName)
                .HasMaxLength(100)
                .HasColumnName("notary_name");
            entity.Property(e => e.NotarySuid)
                .HasMaxLength(100)
                .HasColumnName("notary_suid");
            entity.Property(e => e.PoaDocSigned).HasColumnName("poa_doc_signed");
            entity.Property(e => e.PrincipleEmail)
                .HasMaxLength(100)
                .HasColumnName("principle_email");
            entity.Property(e => e.PrincipleIdDocNumber)
                .HasMaxLength(100)
                .HasColumnName("principle_id_doc_number");
            entity.Property(e => e.PrincipleName)
                .HasMaxLength(100)
                .HasColumnName("principle_name");
            entity.Property(e => e.PrincipleSuid)
                .HasMaxLength(100)
                .HasColumnName("principle_suid");
            entity.Property(e => e.Scope)
                .HasMaxLength(250)
                .HasColumnName("scope");
            entity.Property(e => e.ValidUpto).HasColumnName("valid_upto");
        });

        modelBuilder.Entity<PoaCredentialView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("poa_credential_view");

            entity.Property(e => e.AgentEmail)
                .HasMaxLength(100)
                .HasColumnName("agent_email");
            entity.Property(e => e.AgentIdDocNumber)
                .HasMaxLength(100)
                .HasColumnName("agent_id_doc_number");
            entity.Property(e => e.AgentName)
                .HasMaxLength(100)
                .HasColumnName("agent_name");
            entity.Property(e => e.AgentSuid)
                .HasMaxLength(100)
                .HasColumnName("agent_suid");
            entity.Property(e => e.PoaCredential).HasColumnName("poa_credential");
        });

        modelBuilder.Entity<PreferedTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prefered_titles_pkey");

            entity.ToTable("prefered_titles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PreferedTitles)
                .HasMaxLength(100)
                .HasColumnName("prefered_titles");
        });

        modelBuilder.Entity<PricingSlabDefinition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pricing_slab_definitions_pkey");

            entity.ToTable("pricing_slab_definitions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Stakeholder)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("stakeholder");
            entity.Property(e => e.VolumeRangeFrom).HasColumnName("volume_range_from");
            entity.Property(e => e.VolumeRangeTo).HasColumnName("volume_range_to");

            entity.HasOne(d => d.Service).WithMany(p => p.PricingSlabDefinitions)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pricing_slab_definitions_fk");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId).HasName("privilege_pkey");

            entity.ToTable("privilege");

            entity.Property(e => e.PrivilegeId).HasColumnName("privilege_id");
            entity.Property(e => e.IsChargeable).HasColumnName("is_chargeable");
            entity.Property(e => e.PrivilegeServiceDisplayName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("privilege_service_display_name");
            entity.Property(e => e.PrivilegeServiceName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("privilege_service_name");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("program_pkey");

            entity.ToTable("program");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmountPerCycle)
                .HasPrecision(38)
                .HasColumnName("amount_per_cycle");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.Currency)
                .HasMaxLength(255)
                .HasColumnName("currency");
            entity.Property(e => e.MatchingRegistrants).HasColumnName("matching_registrants");
            entity.Property(e => e.OneTime).HasColumnName("one_time");
            entity.Property(e => e.ProgramName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("program_name");
            entity.Property(e => e.Recurrence)
                .HasMaxLength(255)
                .HasColumnName("recurrence");
            entity.Property(e => e.StartDate)
                .HasMaxLength(100)
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
        });

        modelBuilder.Entity<ProgramMembership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("program_membership_pkey");

            entity.ToTable("program_membership");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.RegistrantId).HasColumnName("registrant_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.HasOne(d => d.Program).WithMany(p => p.ProgramMemberships)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("program_membership_program_fk");

            entity.HasOne(d => d.Registrant).WithMany(p => p.ProgramMemberships)
                .HasForeignKey(d => d.RegistrantId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("program_membership_registrant_details_fk");
        });

        modelBuilder.Entity<QrIssuer>(entity =>
        {
            entity.HasKey(e => e.IssuerId).HasName("qr_issuer_pkey");

            entity.ToTable("qr_issuer");

            entity.Property(e => e.IssuerId)
                .HasMaxLength(100)
                .HasColumnName("issuer_id");
            entity.Property(e => e.IssuerKey)
                .IsRequired()
                .HasColumnName("issuer_key");
        });

        modelBuilder.Entity<Redeem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("redeem_pkey");

            entity.ToTable("redeem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.UploadedOn)
                .HasMaxLength(255)
                .HasColumnName("uploaded_on");
            entity.Property(e => e.VideoFile).HasColumnName("video_file");
        });

        modelBuilder.Entity<RedemptionProof>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("redemption_proof_pkey");

            entity.ToTable("redemption_proof");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RegistrantId).HasColumnName("registrant_id");
            entity.Property(e => e.UploadedAt)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("uploaded_at");
            entity.Property(e => e.VideoData)
                .HasColumnType("oid")
                .HasColumnName("video_data");

            entity.HasOne(d => d.Registrant).WithMany(p => p.RedemptionProofs)
                .HasForeignKey(d => d.RegistrantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkday9an6qprsc5b2hdci6usvvv");
        });

        modelBuilder.Entity<RegisteredSpoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("registered_spocs_pkey");

            entity.ToTable("registered_spocs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CeoName)
                .HasMaxLength(100)
                .HasColumnName("ceo_name");
            entity.Property(e => e.CeoTin)
                .HasMaxLength(100)
                .HasColumnName("ceo_tin");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.OrgName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrgTin)
                .HasMaxLength(100)
                .HasColumnName("org_tin");
            entity.Property(e => e.SpocUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<RegistrantDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("registrant_details_pkey");

            entity.ToTable("registrant_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountHolderName)
                .HasColumnType("character varying")
                .HasColumnName("account_holder_name");
            entity.Property(e => e.AccountType)
                .HasColumnType("character varying")
                .HasColumnName("account_type");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Age)
                .HasColumnType("character varying")
                .HasColumnName("age");
            entity.Property(e => e.BankAccountNumber)
                .HasColumnType("character varying")
                .HasColumnName("bank_account_number");
            entity.Property(e => e.BankAccountStatus)
                .HasColumnType("character varying")
                .HasColumnName("bank_account_status");
            entity.Property(e => e.BirthPlace)
                .HasColumnType("character varying")
                .HasColumnName("birth_place");
            entity.Property(e => e.City)
                .HasColumnType("character varying")
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasColumnType("character varying")
                .HasColumnName("country");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("character varying")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.IdDocNumber)
                .HasColumnType("character varying")
                .HasColumnName("id_doc_number");
            entity.Property(e => e.Income).HasColumnName("income");
            entity.Property(e => e.MaritalStatus)
                .HasColumnType("character varying")
                .HasColumnName("marital_status");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Occupation)
                .HasColumnType("character varying")
                .HasColumnName("occupation");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Photo)
                .HasColumnType("character varying")
                .HasColumnName("photo");
            entity.Property(e => e.Pincode)
                .HasColumnType("character varying")
                .HasColumnName("pincode");
            entity.Property(e => e.State)
                .HasColumnType("character varying")
                .HasColumnName("state");
            entity.Property(e => e.Suid)
                .HasColumnType("character varying")
                .HasColumnName("suid");
            entity.Property(e => e.SwiftCode)
                .HasColumnType("character varying")
                .HasColumnName("swift_code");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<SchemaDatum>(entity =>
        {
            entity.HasKey(e => new { e.IssuerId, e.ProfileType }).HasName("schema_data_pkey");

            entity.ToTable("schema_data");

            entity.Property(e => e.IssuerId)
                .HasMaxLength(100)
                .HasColumnName("issuer_id");
            entity.Property(e => e.ProfileType)
                .HasMaxLength(100)
                .HasColumnName("profile_type");
            entity.Property(e => e.Schema)
                .IsRequired()
                .HasColumnName("schema");
        });

        modelBuilder.Entity<ServiceLevelManagement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_level_management_pkey");

            entity.ToTable("service_level_management");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Loa).HasColumnName("loa");
            entity.Property(e => e.ServiceName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("service_name");
        });

        modelBuilder.Entity<ServicesDefinition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("services_definitions_pkey");

            entity.ToTable("services_definitions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PricingSlabApplicable).HasColumnName("pricing_slab_applicable");
            entity.Property(e => e.ServiceDisplayName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("service_display_name");
            entity.Property(e => e.ServiceName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("service_name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("status");
        });

        modelBuilder.Entity<SignatureTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("signature_templates_pkey");

            entity.ToTable("signature_templates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("display_name");
            entity.Property(e => e.SamplePreview).HasColumnName("sample_preview");
            entity.Property(e => e.TemplateId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("template_id");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("type");
        });

        modelBuilder.Entity<SignatureTemplatesDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("signature_templates_details");

            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.SamplePreview).HasColumnName("sample_preview");
            entity.Property(e => e.SignaturePhoto).HasColumnName("signature_photo");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");
        });

        modelBuilder.Entity<SignedDocument>(entity =>
        {
            entity.HasKey(e => e.CorrelationId).HasName("signed_documents_pkey");

            entity.ToTable("signed_documents");

            entity.Property(e => e.CorrelationId)
                .HasMaxLength(40)
                .HasColumnName("correlation_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.DocType)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("doc_type");
            entity.Property(e => e.SignedData)
                .IsRequired()
                .HasColumnName("signed_data");
        });

        modelBuilder.Entity<SigningDocument>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("signing_documents_pkey");

            entity.ToTable("signing_documents");

            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("description");
            entity.Property(e => e.ExpireDate)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("expire_date");
            entity.Property(e => e.Label)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("label");
            entity.Property(e => e.MimeType)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("mime_type");
            entity.Property(e => e.ModifiedDate)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("modified_date");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Uuid)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("uuid");
            entity.Property(e => e.Version)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("version");
        });

        modelBuilder.Entity<SimulatedBoarderControl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("simulated_boarder_control_pkey");

            entity.ToTable("simulated_boarder_control");

            entity.HasIndex(e => e.IdDocNumber, "simulated_boarder_control_id_doc_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(256)
                .HasColumnName("address");
            entity.Property(e => e.BloodGroup)
                .HasMaxLength(256)
                .HasColumnName("blood_group");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(256)
                .HasColumnName("country_code");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(256)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.EmiratesIdNumber)
                .HasMaxLength(256)
                .HasColumnName("emirates_id_number");
            entity.Property(e => e.FamilyBook).HasColumnName("family_book");
            entity.Property(e => e.FatherName)
                .HasMaxLength(256)
                .HasColumnName("father_name");
            entity.Property(e => e.FingerData).HasColumnName("finger_data");
            entity.Property(e => e.FirstName)
                .HasMaxLength(256)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(256)
                .HasColumnName("gender");
            entity.Property(e => e.IdDocExpiryDate)
                .HasMaxLength(256)
                .HasColumnName("id_doc_expiry_date");
            entity.Property(e => e.IdDocIssueDate)
                .HasMaxLength(256)
                .HasColumnName("id_doc_issue_date");
            entity.Property(e => e.IdDocNumber)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.IdDocType).HasColumnName("id_doc_type");
            entity.Property(e => e.LastName)
                .HasMaxLength(256)
                .HasColumnName("last_name");
            entity.Property(e => e.MobNo)
                .HasMaxLength(256)
                .HasColumnName("mob_no");
            entity.Property(e => e.MotherName)
                .HasMaxLength(256)
                .HasColumnName("mother_name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(256)
                .HasColumnName("nationality");
            entity.Property(e => e.Occupation)
                .HasMaxLength(256)
                .HasColumnName("occupation");
            entity.Property(e => e.PersonNo)
                .HasMaxLength(100)
                .HasColumnName("person_no");
            entity.Property(e => e.PlaceOfBirth)
                .HasMaxLength(256)
                .HasColumnName("place_of_birth");
            entity.Property(e => e.SelfieImage).HasColumnName("selfie_image");
            entity.Property(e => e.SpouseName)
                .HasMaxLength(256)
                .HasColumnName("spouse_name");
            entity.Property(e => e.Uin).HasColumnName("uin");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<SoftwareLicense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("software_licenses_pkey");

            entity.ToTable("software_licenses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasColumnName("application_name");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("character varying")
                .HasColumnName("created_date_time");
            entity.Property(e => e.FirstActivated)
                .HasMaxLength(100)
                .HasColumnName("first_activated");
            entity.Property(e => e.IssuedOn)
                .HasColumnType("character varying")
                .HasColumnName("issued_on");
            entity.Property(e => e.LastActivated)
                .HasMaxLength(100)
                .HasColumnName("last_activated");
            entity.Property(e => e.LicenceStatus)
                .HasMaxLength(100)
                .HasColumnName("licence_status");
            entity.Property(e => e.LicenseInfo).HasColumnName("license_info");
            entity.Property(e => e.LicenseType)
                .HasMaxLength(100)
                .HasColumnName("license_type");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.Ouid)
                .HasMaxLength(100)
                .HasColumnName("ouid");
            entity.Property(e => e.SoftwareName)
                .HasMaxLength(100)
                .HasColumnName("software_name");
            entity.Property(e => e.UpdatedDateTime)
                .HasColumnType("character varying")
                .HasColumnName("updated_date_time");
            entity.Property(e => e.ValidUpto)
                .HasColumnType("character varying")
                .HasColumnName("valid_upto");
        });

        modelBuilder.Entity<SoftwareLicenseApprovalRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("software_license_approval_requests_pkey");

            entity.ToTable("software_license_approval_requests");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovalStatus)
                .HasMaxLength(50)
                .HasColumnName("approval_status");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("character varying")
                .HasColumnName("created_date_time");
            entity.Property(e => e.LicenseType)
                .HasMaxLength(50)
                .HasColumnName("license_type");
            entity.Property(e => e.Ouid)
                .HasMaxLength(100)
                .HasColumnName("ouid");
            entity.Property(e => e.SoftwareName)
                .HasMaxLength(100)
                .HasColumnName("software_name");
            entity.Property(e => e.UpdatedDateTime)
                .HasColumnType("character varying")
                .HasColumnName("updated_date_time");
        });

        modelBuilder.Entity<SoftwareLicensesHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("software_licenses_history_pkey");

            entity.ToTable("software_licenses_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("character varying")
                .HasColumnName("created_date_time");
            entity.Property(e => e.IssuedOn)
                .HasColumnType("character varying")
                .HasColumnName("issued_on");
            entity.Property(e => e.LicenseInfo).HasColumnName("license_info");
            entity.Property(e => e.LicenseType)
                .HasMaxLength(100)
                .HasColumnName("license_type");
            entity.Property(e => e.Ouid)
                .HasMaxLength(100)
                .HasColumnName("ouid");
            entity.Property(e => e.SoftwareName)
                .HasMaxLength(100)
                .HasColumnName("software_name");
            entity.Property(e => e.UpdatedDateTime)
                .HasColumnType("character varying")
                .HasColumnName("updated_date_time");
            entity.Property(e => e.ValidUpto)
                .HasColumnType("character varying")
                .HasColumnName("valid_upto");
        });

        modelBuilder.Entity<SoftwareSuggestionAndOrganisationReadiness>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("software_suggestion_and_organisation_readiness_pkey");

            entity.ToTable("software_suggestion_and_organisation_readiness");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrmSuggestionToOrganisation)
                .HasMaxLength(100)
                .HasColumnName("brm_suggestion_to_organisation");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.DigitalCertificateForEnterpriseGateway).HasColumnName("digital_certificate_for_enterprise_gateway");
            entity.Property(e => e.InfraReadinessChecklistSubmittedBySpoc)
                .HasColumnType("character varying")
                .HasColumnName("infra_readiness_checklist_submitted_by_spoc");
            entity.Property(e => e.OrgOnboardingFormId).HasColumnName("org_onboarding_form_id");
            entity.Property(e => e.SpocOptedFor)
                .HasMaxLength(100)
                .HasColumnName("spoc_opted_for");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.OrgOnboardingForm).WithMany(p => p.SoftwareSuggestionAndOrganisationReadinesses)
                .HasForeignKey(d => d.OrgOnboardingFormId)
                .HasConstraintName("software_suggestion_and_organisation_readiness_fk");
        });

        modelBuilder.Entity<SppMetaSoftwareName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spp_meta_software_names_pkey");

            entity.ToTable("spp_meta_software_names");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("display_name");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<SppVgAuditorDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spp_vg_auditor_details_pkey");

            entity.ToTable("spp_vg_auditor_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuditorDocumentNumber)
                .HasMaxLength(255)
                .HasColumnName("auditor_document_number");
            entity.Property(e => e.AuditorName)
                .HasMaxLength(255)
                .HasColumnName("auditor_name");
            entity.Property(e => e.AuditorOfficialEmail)
                .HasMaxLength(255)
                .HasColumnName("auditor_official_email");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(255)
                .HasColumnName("created_on");
            entity.Property(e => e.OrgDetailsId).HasColumnName("org_details_id");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(255)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<SppVgOrganisationCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spp_vg_organisation_categories_pkey");

            entity.ToTable("spp_vg_organisation_categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.LabelName)
                .HasMaxLength(255)
                .HasColumnName("label_name");
        });

        modelBuilder.Entity<SppVgOrganizationDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spp_vg_organization_details_pkey");

            entity.ToTable("spp_vg_organization_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(255)
                .HasColumnName("created_on");
            entity.Property(e => e.OrgEmail)
                .HasMaxLength(100)
                .HasColumnName("org_email");
            entity.Property(e => e.OrgName)
                .HasMaxLength(255)
                .HasColumnName("org_name");
            entity.Property(e => e.OrgType)
                .HasMaxLength(255)
                .HasColumnName("org_type");
            entity.Property(e => e.OrganizationLetter).HasColumnName("organization_letter");
            entity.Property(e => e.Ouid)
                .HasMaxLength(255)
                .HasColumnName("ouid");
            entity.Property(e => e.RegNo)
                .HasMaxLength(255)
                .HasColumnName("reg_no");
            entity.Property(e => e.SpocAuthorizationLetter).HasColumnName("spoc_authorization_letter");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.TaxNumber)
                .HasMaxLength(255)
                .HasColumnName("tax_number");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(255)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<SppVgPasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spp_vg_password_reset_token_pkey");

            entity.ToTable("spp_vg_password_reset_token");

            entity.HasIndex(e => e.Token, "uk_password_reset_token_token").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.ExpiryTime)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("expiry_time");
            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("token");
            entity.Property(e => e.Used)
                .HasDefaultValue(false)
                .HasColumnName("used");
        });

        modelBuilder.Entity<SppVgSoftware>(entity =>
        {
            entity.HasKey(e => e.SoftwareId).HasName("spp_vg_softwares_pkey");

            entity.ToTable("spp_vg_softwares");

            entity.Property(e => e.SoftwareId).HasColumnName("software_id");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.DownloadLink)
                .HasMaxLength(100)
                .HasColumnName("download_link");
            entity.Property(e => e.FileName)
                .HasMaxLength(100)
                .HasColumnName("file_name");
            entity.Property(e => e.InstallManual)
                .HasMaxLength(100)
                .HasColumnName("install_manual");
            entity.Property(e => e.PublishedOn)
                .HasMaxLength(100)
                .HasColumnName("published_on");
            entity.Property(e => e.SizeOfSoftware)
                .HasMaxLength(100)
                .HasColumnName("size_of_software");
            entity.Property(e => e.SoftwareName)
                .HasMaxLength(100)
                .HasColumnName("software_name");
            entity.Property(e => e.SoftwareVersion)
                .HasMaxLength(100)
                .HasColumnName("software_version");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<SppVgSpocDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spp_vg_spoc_details_pkey");

            entity.ToTable("spp_vg_spoc_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(255)
                .HasColumnName("created_on");
            entity.Property(e => e.OrgDetailsId).HasColumnName("org_details_id");
            entity.Property(e => e.SpocDocumentNumber)
                .HasMaxLength(255)
                .HasColumnName("spoc_document_number");
            entity.Property(e => e.SpocName)
                .HasMaxLength(255)
                .HasColumnName("spoc_name");
            entity.Property(e => e.SpocOfficialEmail)
                .HasMaxLength(255)
                .HasColumnName("spoc_official_email");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(255)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<SppVgTrustedUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spp_vg_trusted_users_pkey");

            entity.ToTable("spp_vg_trusted_users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(255)
                .HasColumnName("created_on");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(255)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<SubCertStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("sub_cert_status");

            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(16)
                .HasColumnName("certificate_status");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(128)
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.FcmToken)
                .HasMaxLength(255)
                .HasColumnName("fcm_token");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(16)
                .HasColumnName("mobile_number");
            entity.Property(e => e.SubscriberStatus)
                .HasMaxLength(16)
                .HasColumnName("subscriber_status");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubmitCheckboxConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("submit_checkbox_config_pkey");

            entity.ToTable("submit_checkbox_config");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FeildDescription)
                .HasMaxLength(100)
                .HasColumnName("feild_description");
            entity.Property(e => e.Isenable).HasColumnName("isenable");
            entity.Property(e => e.OrgCategory)
                .HasMaxLength(100)
                .HasColumnName("org_category");
            entity.Property(e => e.OrgCategoryId)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("org_category_id");
        });

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.SubscriberId).HasName("subscribers_pkey");

            entity.ToTable("subscribers");

            entity.HasIndex(e => e.EmailId, "subscribers_email_id_key").IsUnique();

            entity.HasIndex(e => e.MobileNumber, "subscribers_mobile_number_key").IsUnique();

            entity.HasIndex(e => e.SubscriberUid, "subscribers_subscriber_uid_key").IsUnique();

            entity.Property(e => e.SubscriberId).HasColumnName("subscriber_id");
            entity.Property(e => e.AppVersion)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("app_version");
            entity.Property(e => e.CreatedDate)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(50)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DeviceInfo)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("device_info");
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("email_id");
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("full_name");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(16)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.IdDocType)
                .HasMaxLength(16)
                .HasColumnName("id_doc_type");
            entity.Property(e => e.IsSmartphoneUser)
                .HasDefaultValue((short)1)
                .HasColumnName("is_smartphone_user");
            entity.Property(e => e.MobileNumber)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("mobile_number");
            entity.Property(e => e.NationalId)
                .HasMaxLength(100)
                .HasColumnName("national_id");
            entity.Property(e => e.OsName)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("os_name");
            entity.Property(e => e.OsVersion)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("os_version");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedDate)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<SubscriberCardDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriber_card_details_pkey");

            entity.ToTable("subscriber_card_details");

            entity.HasIndex(e => e.SubscriberUid, "subscriber_card_details_subscriber_uid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardDocumnet)
                .IsRequired()
                .HasColumnName("card_documnet");
            entity.Property(e => e.IdDocNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubscriberCertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateSerialNumber).HasName("subscriber_certificates_pkey");

            entity.ToTable("subscriber_certificates");

            entity.HasIndex(e => e.PkiKeyId, "subscriber_certificates_pki_key_id_key").IsUnique();

            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(1000)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CerificateExpiryDate)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateData)
                .IsRequired()
                .HasMaxLength(4096)
                .HasColumnName("certificate_data");
            entity.Property(e => e.CertificateIssueDate)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateStatus)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CertificateType)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("certificate_type");
            entity.Property(e => e.CreatedDate)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.PkiKeyId)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("pki_key_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("remarks");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("character varying")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<SubscriberCertificateLifeCycle>(entity =>
        {
            entity.HasKey(e => e.SubscriberCertificateLifeCycleId).HasName("subscriber_certificate_life_cycle_pkey");

            entity.ToTable("subscriber_certificate_life_cycle");

            entity.Property(e => e.SubscriberCertificateLifeCycleId)
                .HasDefaultValueSql("nextval('subscriber_certificate_life_c_subscriber_certificate_life_c_seq'::regclass)")
                .HasColumnName("subscriber_certificate_life_cycle_id");
            entity.Property(e => e.CertificateSerialNumber)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CertificateStatus)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CertificateType)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("certificate_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.RevocationReason)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("revocation_reason");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubscriberCertificatePinHistory>(entity =>
        {
            entity.HasKey(e => e.SubscriberCertificatePinHistoryId).HasName("subscriber_certificate_pin_history_pkey");

            entity.ToTable("subscriber_certificate_pin_history");

            entity.HasIndex(e => e.SubscriberUid, "subscriber_certificate_pin_history_subscriber_uid_key").IsUnique();

            entity.Property(e => e.SubscriberCertificatePinHistoryId)
                .HasDefaultValueSql("nextval('subscriber_certificate_pin_hi_subscriber_certificate_pin_hi_seq'::regclass)")
                .HasColumnName("subscriber_certificate_pin_history_id");
            entity.Property(e => e.AuthPinList)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("auth_pin_list");
            entity.Property(e => e.AuthPinSetDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auth_pin_set_date");
            entity.Property(e => e.SignPinList)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("sign_pin_list");
            entity.Property(e => e.SignPinSetDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sign_pin_set_date");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubscriberCertificateView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("subscriber_certificate_view");

            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(1000)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubscriberCertificatesDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("subscriber_certificates_details");

            entity.Property(e => e.CerificateExpiryDate)
                .HasColumnType("character varying")
                .HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("character varying")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(1000)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(16)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CertificateType)
                .HasMaxLength(16)
                .HasColumnName("certificate_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.FullName)
                .HasMaxLength(128)
                .HasColumnName("full_name");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(16)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.IdDocType)
                .HasMaxLength(16)
                .HasColumnName("id_doc_type");
            entity.Property(e => e.OnBoardingMethod)
                .HasMaxLength(16)
                .HasColumnName("on_boarding_method");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubscriberCompleteDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("subscriber_complete_details");

            entity.Property(e => e.AuthPinSetDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auth_pin_set_date");
            entity.Property(e => e.CerificateExpiryDate)
                .HasColumnType("character varying")
                .HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("character varying")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(1000)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CertificateStatus)
                .HasColumnType("character varying")
                .HasColumnName("certificate_status");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(50)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DeviceRegistrationTime)
                .HasColumnType("character varying")
                .HasColumnName("device_registration_time");
            entity.Property(e => e.DeviceStatus)
                .HasColumnType("character varying")
                .HasColumnName("device_status");
            entity.Property(e => e.EmailId)
                .HasMaxLength(64)
                .HasColumnName("email_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(128)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(16)
                .HasColumnName("gender");
            entity.Property(e => e.GeoLocation)
                .HasMaxLength(255)
                .HasColumnName("geo_location");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(16)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.IdDocType)
                .HasMaxLength(16)
                .HasColumnName("id_doc_type");
            entity.Property(e => e.LevelOfAssurance)
                .HasMaxLength(16)
                .HasColumnName("level_of_assurance");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(16)
                .HasColumnName("mobile_number");
            entity.Property(e => e.OnBoardingMethod)
                .HasMaxLength(16)
                .HasColumnName("on_boarding_method");
            entity.Property(e => e.OnBoardingTime)
                .HasColumnType("character varying")
                .HasColumnName("on_boarding_time");
            entity.Property(e => e.RevocationDate)
                .HasColumnType("character varying")
                .HasColumnName("revocation_date");
            entity.Property(e => e.RevocationReason)
                .HasMaxLength(128)
                .HasColumnName("revocation_reason");
            entity.Property(e => e.SelfieThumbnailUri).HasColumnName("selfie_thumbnail_uri");
            entity.Property(e => e.SelfieUri).HasColumnName("selfie_uri");
            entity.Property(e => e.SignPinSetDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sign_pin_set_date");
            entity.Property(e => e.SubscriberStatus)
                .HasColumnType("character varying")
                .HasColumnName("subscriber_status");
            entity.Property(e => e.SubscriberType)
                .HasMaxLength(16)
                .HasColumnName("subscriber_type");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.VideoType)
                .HasMaxLength(50)
                .HasColumnName("video_type");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(600)
                .HasColumnName("video_url");
        });

        modelBuilder.Entity<SubscriberConsent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriber_consents_pkey");

            entity.ToTable("subscriber_consents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConsentData).HasColumnName("consent_data");
            entity.Property(e => e.ConsentId).HasColumnName("consent_id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.SignedConsentData).HasColumnName("signed_consent_data");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubscriberContactHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("subscriber_contact_history");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.EmailId)
                .HasMaxLength(64)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("email_id");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("mobile_number");
            entity.Property(e => e.SubscriberContactHistoryId).HasColumnName("subscriber_contact_history_id");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubscriberCountView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("subscriber_count_view");

            entity.Property(e => e.ActiveSubscriberCount).HasColumnName("active_subscriber_count");
            entity.Property(e => e.CertExpiredSubscriberCount).HasColumnName("cert_expired_subscriber_count");
            entity.Property(e => e.CertRevokeSubscriberCount).HasColumnName("cert_revoke_subscriber_count");
            entity.Property(e => e.DisableSubscriberCount).HasColumnName("disable_subscriber_count");
            entity.Property(e => e.InactiveSubscriberCount).HasColumnName("inactive_subscriber_count");
            entity.Property(e => e.OnboardedSubscriberCount).HasColumnName("onboarded_subscriber_count");
            entity.Property(e => e.RegisteredSubscriberCount).HasColumnName("registered_subscriber_count");
            entity.Property(e => e.SubscriberCount).HasColumnName("subscriber_count");
        });

        modelBuilder.Entity<SubscriberDevice>(entity =>
        {
            entity.HasKey(e => e.SubscriberDeviceId).HasName("subscriber_devices_pkey");

            entity.ToTable("subscriber_devices");

            entity.HasIndex(e => e.SubscriberUid, "subscriber_devices_un").IsUnique();

            entity.Property(e => e.SubscriberDeviceId).HasColumnName("subscriber_device_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.DeviceStatus)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("device_status");
            entity.Property(e => e.DeviceUid)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("device_uid");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("character varying")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.SubscriberU).WithOne(p => p.SubscriberDevice)
                .HasPrincipalKey<Subscriber>(p => p.SubscriberUid)
                .HasForeignKey<SubscriberDevice>(d => d.SubscriberUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("subscriber_devices_fk");
        });

        modelBuilder.Entity<SubscriberDevicesHistory>(entity =>
        {
            entity.HasKey(e => e.SubscriberDeviceHistoryId).HasName("subscriber_devices_history_pkey");

            entity.ToTable("subscriber_devices_history");

            entity.Property(e => e.SubscriberDeviceHistoryId).HasColumnName("subscriber_device_history_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.DeviceStatus)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("device_status");
            entity.Property(e => e.DeviceUid)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("device_uid");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("character varying")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.SubscriberU).WithMany(p => p.SubscriberDevicesHistories)
                .HasPrincipalKey(p => p.SubscriberUid)
                .HasForeignKey(d => d.SubscriberUid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("subscriber_devices_fk_copy");
        });

        modelBuilder.Entity<SubscriberFcmToken>(entity =>
        {
            entity.HasKey(e => e.SubscriberFcmTokenId).HasName("subscriber_fcm_token_pkey");

            entity.ToTable("subscriber_fcm_token");

            entity.HasIndex(e => e.SubscriberUid, "subscriber_fcm_token_un").IsUnique();

            entity.Property(e => e.SubscriberFcmTokenId).HasColumnName("subscriber_fcm_token_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.FcmToken)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("fcm_token");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("subscriber_uid");

            entity.HasOne(d => d.SubscriberU).WithOne(p => p.SubscriberFcmToken)
                .HasPrincipalKey<Subscriber>(p => p.SubscriberUid)
                .HasForeignKey<SubscriberFcmToken>(d => d.SubscriberUid)
                .HasConstraintName("subscriber_fcm_token_fk");
        });

        modelBuilder.Entity<SubscriberIdDocCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("subscriber_id_doc_count");

            entity.Property(e => e.CountIdDocNumber).HasColumnName("count_id_doc_number");
        });

        modelBuilder.Entity<SubscriberInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("subscriber_info");

            entity.Property(e => e.DisplayName)
                .HasMaxLength(128)
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.FcmToken)
                .HasMaxLength(255)
                .HasColumnName("fcm_token");
            entity.Property(e => e.IdDocumentExpiryDate).HasColumnName("id_document_expiry_date");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(16)
                .HasColumnName("mobile_number");
            entity.Property(e => e.SubscriberStatus)
                .HasMaxLength(16)
                .HasColumnName("subscriber_status");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<SubscriberOnboardingDatum>(entity =>
        {
            entity.HasKey(e => e.SubscriberOnboardingDataId).HasName("subscriber_onboarding_data_pkey");

            entity.ToTable("subscriber_onboarding_data");

            entity.Property(e => e.SubscriberOnboardingDataId).HasColumnName("subscriber_onboarding_data_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.DateOfExpiry)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("date_of_expiry");
            entity.Property(e => e.DocumentsLocation)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("documents_location");
            entity.Property(e => e.Gender)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("gender");
            entity.Property(e => e.Geolocation)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("geolocation");
            entity.Property(e => e.IdDocCode)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("id_doc_code");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("id_doc_number");
            entity.Property(e => e.IdDocType)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("id_doc_type");
            entity.Property(e => e.IdDocUri).HasColumnName("id_doc_uri");
            entity.Property(e => e.LevelOfAssurance)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("level_of_assurance");
            entity.Property(e => e.NiraResponse).HasColumnName("nira_response");
            entity.Property(e => e.OnboardingDataFieldsJson).HasColumnName("onboarding_data_fields_json");
            entity.Property(e => e.OnboardingMethod)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("onboarding_method");
            entity.Property(e => e.OptionalData1)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("optional_data1");
            entity.Property(e => e.PrevIdDocNumber)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("prev_id_doc_number");
            entity.Property(e => e.Remarks).HasColumnName("remarks");
            entity.Property(e => e.SelfieThumbnailUri).HasColumnName("selfie_thumbnail_uri");
            entity.Property(e => e.SelfieUri).HasColumnName("selfie_uri");
            entity.Property(e => e.SubscriberType)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("subscriber_type");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(56)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.VerifierProvidedPhoto).HasColumnName("verifier_provided_photo");
            entity.Property(e => e.VoiceUrl).HasColumnName("voice_url");

            entity.HasOne(d => d.SubscriberU).WithMany(p => p.SubscriberOnboardingData)
                .HasPrincipalKey(p => p.SubscriberUid)
                .HasForeignKey(d => d.SubscriberUid)
                .HasConstraintName("subscriber_onboarding_data_fk");
        });

        modelBuilder.Entity<SubscriberOnboardingTemplate>(entity =>
        {
            entity.HasKey(e => e.TemplateId).HasName("subscriber_onboarding_templates_pkey");

            entity.ToTable("subscriber_onboarding_templates");

            entity.HasIndex(e => e.TemplateName, "subscriber_onboarding_templates_template_name_key").IsUnique();

            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("approved_by");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.PublishedStatus)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("published_status");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("remarks");
            entity.Property(e => e.State)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("state");
            entity.Property(e => e.TemplateMethod)
                .HasMaxLength(64)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("template_method");
            entity.Property(e => e.TemplateName)
                .HasMaxLength(32)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("template_name");
            entity.Property(e => e.UpatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("upated_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<SubscriberPaymentHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("subscriber_payment_history");

            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(100)
                .HasColumnName("organization_id");
            entity.Property(e => e.PaymentCategory)
                .HasMaxLength(100)
                .HasColumnName("payment_category");
            entity.Property(e => e.PaymentInfo).HasColumnName("payment_info");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(16)
                .HasColumnName("payment_status");
            entity.Property(e => e.SubscriberStatus)
                .HasMaxLength(16)
                .HasColumnName("subscriber_status");
            entity.Property(e => e.SubscriberSuid)
                .HasColumnType("character varying")
                .HasColumnName("subscriber_suid");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
            entity.Property(e => e.TransactionReferenceId)
                .HasMaxLength(100)
                .HasColumnName("transaction_reference_id");
        });

        modelBuilder.Entity<SubscriberPaymentsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriber_payments_history_pkey");

            entity.ToTable("subscriber_payments_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AggregatorAcknowledgementId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("aggregator_acknowledgement_id");
            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.EncryptedEmail)
                .HasMaxLength(100)
                .HasColumnName("encrypted_email");
            entity.Property(e => e.EncryptedMobileNumber)
                .HasMaxLength(16)
                .HasColumnName("encrypted_mobile_number");
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(100)
                .HasColumnName("organization_id");
            entity.Property(e => e.PaymentCategory)
                .HasMaxLength(100)
                .HasColumnName("payment_category");
            entity.Property(e => e.PaymentChannel)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("payment_channel");
            entity.Property(e => e.PaymentForOrganization).HasColumnName("payment_for_organization");
            entity.Property(e => e.PaymentInfo)
                .IsRequired()
                .HasColumnName("payment_info");
            entity.Property(e => e.PaymentStatus)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("payment_status");
            entity.Property(e => e.SubscriberSuid)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("subscriber_suid");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
            entity.Property(e => e.TransactionReferenceId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("transaction_reference_id");
        });

        modelBuilder.Entity<SubscriberPersonalDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriber_personal_documents_pkey");

            entity.ToTable("subscriber_personal_documents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Document).HasColumnName("document");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<SubscriberRaDatum>(entity =>
        {
            entity.HasKey(e => e.SubscriberUid).HasName("subscriber_ra_data_pkey");

            entity.ToTable("subscriber_ra_data");

            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.CertificateType)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnName("certificate_type");
            entity.Property(e => e.CommonName)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("common_name");
            entity.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedDate)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.PkiPassword)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("pki_password");
            entity.Property(e => e.PkiPasswordHash)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("pki_password_hash");
            entity.Property(e => e.PkiUserName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("pki_user_name");
            entity.Property(e => e.PkiUserNameHash)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("pki_user_name_hash");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("character varying")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<SubscriberSimDatum>(entity =>
        {
            entity.HasKey(e => e.SubscriberSimId).HasName("subscriber_sim_data_pkey");

            entity.ToTable("subscriber_sim_data");

            entity.Property(e => e.SubscriberSimId).HasColumnName("subscriber_sim_id");
            entity.Property(e => e.Age)
                .HasMaxLength(100)
                .HasColumnName("age");
            entity.Property(e => e.AlternateMobileNumber)
                .HasMaxLength(100)
                .HasColumnName("alternate_mobile_number");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .HasColumnName("gender");
            entity.Property(e => e.MobileNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.Nation)
                .HasMaxLength(100)
                .HasColumnName("nation");
            entity.Property(e => e.Passport)
                .HasMaxLength(100)
                .HasColumnName("passport");
            entity.Property(e => e.SelfieImage).HasColumnName("selfie_image");
            entity.Property(e => e.SimDataJson).HasColumnName("sim_data_json");
            entity.Property(e => e.SimForm).HasColumnName("sim_form");
            entity.Property(e => e.SimNo)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("sim_no");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.VisitorIdCardNumber)
                .HasMaxLength(100)
                .HasColumnName("visitor_id_card_number");
        });

        modelBuilder.Entity<SubscriberStatus>(entity =>
        {
            entity.HasKey(e => e.SubscriberStatusId).HasName("subscriber_status_pkey");

            entity.ToTable("subscriber_status");

            entity.HasIndex(e => e.SubscriberUid, "subscriber_status_un").IsUnique();

            entity.Property(e => e.SubscriberStatusId).HasColumnName("subscriber_status_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("character varying")
                .HasColumnName("created_date");
            entity.Property(e => e.OtpVerifiedStatus)
                .HasMaxLength(16)
                .HasColumnName("otp_verified_status");
            entity.Property(e => e.SubscriberStatus1)
                .HasMaxLength(16)
                .HasColumnName("subscriber_status");
            entity.Property(e => e.SubscriberStatusDescription)
                .HasMaxLength(128)
                .HasColumnName("subscriber_status_description");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("character varying")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.SubscriberU).WithOne(p => p.SubscriberStatus)
                .HasPrincipalKey<Subscriber>(p => p.SubscriberUid)
                .HasForeignKey<SubscriberStatus>(d => d.SubscriberUid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subscriber_status_fk");
        });

        modelBuilder.Entity<SubscriberUgpassIdCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriber_ugpass_id_card_pkey");

            entity.ToTable("subscriber_ugpass_id_card");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(100)
                .HasColumnName("card_number");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(100)
                .HasColumnName("country_code");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(50)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(16)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.MobNo)
                .HasMaxLength(20)
                .HasColumnName("mob_no");
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .HasColumnName("nationality");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PidDocument).HasColumnName("pid_document");
            entity.Property(e => e.PidExpiryDate)
                .HasMaxLength(50)
                .HasColumnName("pid_expiry_date");
            entity.Property(e => e.PidIssueDate)
                .HasMaxLength(50)
                .HasColumnName("pid_issue_date");
            entity.Property(e => e.SubscriberUid)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<SubscriberView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("subscriber_view");

            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(16)
                .HasColumnName("certificate_status");
            entity.Property(e => e.Country)
                .HasMaxLength(8)
                .HasColumnName("country");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(50)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfExpiry).HasColumnName("date_of_expiry");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(128)
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.FcmToken)
                .HasMaxLength(255)
                .HasColumnName("fcm_token");
            entity.Property(e => e.Gender)
                .HasMaxLength(16)
                .HasColumnName("gender");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(16)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.IdDocType)
                .HasMaxLength(16)
                .HasColumnName("id_doc_type");
            entity.Property(e => e.IsSmartphoneUser).HasColumnName("is_smartphone_user");
            entity.Property(e => e.Loa)
                .HasMaxLength(16)
                .HasColumnName("loa");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(16)
                .HasColumnName("mobile_number");
            entity.Property(e => e.NationalId)
                .HasMaxLength(100)
                .HasColumnName("national_id");
            entity.Property(e => e.UaeKycId)
                .HasMaxLength(100)
                .HasColumnName("uae_kyc_id");
            entity.Property(e => e.OrgEmailsList).HasColumnName("org_emails_list");
            entity.Property(e => e.SelfieUri).HasColumnName("selfie_uri");
            entity.Property(e => e.Selfie).HasColumnName("selfie");
            entity.Property(e => e.SubscriberStatus)
                .HasMaxLength(16)
                .HasColumnName("subscriber_status");
            entity.Property(e => e.SubscriberType)
                .HasMaxLength(16)
                .HasColumnName("subscriber_type");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.LanguagePreferred)
                .HasMaxLength(36)
                .HasColumnName("language_preferred");
        });

        modelBuilder.Entity<SubscriberWrappedKey>(entity =>
        {
            entity.HasKey(e => e.CertificateSerialNumber).HasName("subscriber_wrapped_key_pkey");

            entity.ToTable("subscriber_wrapped_key");

            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(200)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.WrappedKey)
                .IsRequired()
                .HasColumnName("wrapped_key");
        });

        modelBuilder.Entity<SystemRateCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("system_rate_card_pkey");

            entity.ToTable("system_rate_card");

            entity.HasIndex(e => e.ServiceCode, "uk_system_rate_card_service_code").IsUnique();

            entity.HasIndex(e => e.ServiceType, "uk_system_rate_card_service_type").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.GovernmentVatPercent)
                .HasPrecision(10, 2)
                .HasColumnName("government_vat_percent");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.PlatformFeePercent)
                .HasPrecision(10, 2)
                .HasColumnName("platform_fee_percent");
            entity.Property(e => e.PricePerTransaction)
                .HasPrecision(10, 2)
                .HasColumnName("price_per_transaction");
            entity.Property(e => e.ProjectId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("project_id");
            entity.Property(e => e.RateEffectiveFrom)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("rate_effective_from");
            entity.Property(e => e.RateEffectiveUpto)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("rate_effective_upto");
            entity.Property(e => e.ServiceCode)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("service_code");
            entity.Property(e => e.ServiceName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("service_name");
            entity.Property(e => e.ServiceType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("service_type");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.VatIncludedInPricing).HasColumnName("vat_included_in_pricing");
        });

        modelBuilder.Entity<SystemRateCardCustomPricing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("system_rate_card_custom_pricing_pkey");

            entity.ToTable("system_rate_card_custom_pricing");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DiscountPercentage)
                .HasPrecision(10, 2)
                .HasColumnName("discount_percentage");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.StakeholderId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("stakeholder_id");
            entity.Property(e => e.StakeholderName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("stakeholder_name");
            entity.Property(e => e.StakeholderType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("stakeholder_type");
            entity.Property(e => e.SystemServicePkFk).HasColumnName("system_service_pk_fk");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.SystemServicePkFkNavigation).WithMany(p => p.SystemRateCardCustomPricings)
                .HasForeignKey(d => d.SystemServicePkFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_system_rate_card_custom_pricing_service");
        });

        modelBuilder.Entity<SystemRateCardVolume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("system_rate_card_volumes_pkey");

            entity.ToTable("system_rate_card_volumes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.MaxVolume).HasColumnName("max_volume");
            entity.Property(e => e.MinVolume).HasColumnName("min_volume");
            entity.Property(e => e.PricePerTransaction)
                .HasPrecision(10, 2)
                .HasColumnName("price_per_transaction");
            entity.Property(e => e.RateEffectiveFrom)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("rate_effective_from");
            entity.Property(e => e.RateEffectiveUpto)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("rate_effective_upto");
            entity.Property(e => e.SystemServicePkFk).HasColumnName("system_service_pk_fk");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");

            entity.HasOne(d => d.SystemServicePkFkNavigation).WithMany(p => p.SystemRateCardVolumes)
                .HasForeignKey(d => d.SystemServicePkFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_system_rate_card_volumes_service");
        });

        modelBuilder.Entity<TaxPayerRegistrationDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tax_payer_registration_details_pkey");

            entity.ToTable("tax_payer_registration_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.CustomsAgent)
                .HasMaxLength(100)
                .HasColumnName("customs_agent");
            entity.Property(e => e.LicenceNumber)
                .HasMaxLength(100)
                .HasColumnName("licence_number");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.PostalAddress)
                .HasMaxLength(100)
                .HasColumnName("postal_address");
            entity.Property(e => e.RegistrationStatus)
                .HasMaxLength(100)
                .HasColumnName("registration_status");
            entity.Property(e => e.TaxPayerEmail)
                .HasMaxLength(100)
                .HasColumnName("tax_payer_email");
            entity.Property(e => e.TaxPayerName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("tax_payer_name");
            entity.Property(e => e.Tin)
                .HasMaxLength(100)
                .HasColumnName("tin");
            entity.Property(e => e.TypeOfUser)
                .HasMaxLength(100)
                .HasColumnName("type_of_user");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<TelecomSimIssuance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("telecom_sim_issuance_pkey");

            entity.ToTable("telecom_sim_issuance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(100)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.JsonBody).HasColumnName("json_body");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.SimNumber).HasColumnName("sim_number");
        });

        modelBuilder.Entity<TempDocStatusDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("temp_doc_status_data_pkey");

            entity.ToTable("temp_doc_status_data");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Agent)
                .IsRequired()
                .HasDefaultValueSql("0")
                .HasColumnType("character varying")
                .HasColumnName("agent");
            entity.Property(e => e.DocId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("doc_id");
            entity.Property(e => e.Notary)
                .IsRequired()
                .HasDefaultValueSql("0")
                .HasColumnType("character varying")
                .HasColumnName("notary");
            entity.Property(e => e.PoaId).HasColumnName("poa_id");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("status");
        });

        modelBuilder.Entity<TemporaryPoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("temporary_poa_pkey");

            entity.ToTable("temporary_poa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalFields).HasColumnName("additional_fields");
            entity.Property(e => e.Agent).HasColumnName("agent");
            entity.Property(e => e.AgentEmail)
                .HasMaxLength(255)
                .HasColumnName("agent_email");
            entity.Property(e => e.AgentIdDocNumber)
                .HasMaxLength(255)
                .HasColumnName("agent_id_doc_number");
            entity.Property(e => e.AgentName)
                .HasMaxLength(255)
                .HasColumnName("agent_name");
            entity.Property(e => e.AgentSuid)
                .HasMaxLength(255)
                .HasColumnName("agent_suid");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.DocId)
                .HasMaxLength(255)
                .HasColumnName("doc_id");
            entity.Property(e => e.EffectiveDate)
                .HasMaxLength(100)
                .HasColumnName("effective_date");
            entity.Property(e => e.Notary).HasColumnName("notary");
            entity.Property(e => e.NotaryEmail)
                .HasMaxLength(255)
                .HasColumnName("notary_email");
            entity.Property(e => e.NotaryIdDocNumber)
                .HasMaxLength(255)
                .HasColumnName("notary_id_doc_number");
            entity.Property(e => e.NotaryName)
                .HasMaxLength(255)
                .HasColumnName("notary_name");
            entity.Property(e => e.NotarySuid)
                .HasMaxLength(255)
                .HasColumnName("notary_suid");
            entity.Property(e => e.PoaId).HasColumnName("poa_id");
            entity.Property(e => e.PoaRequestForm).HasColumnName("poa_request_form");
            entity.Property(e => e.PrincipleEmail)
                .HasMaxLength(255)
                .HasColumnName("principle_email");
            entity.Property(e => e.PrincipleIdDocNumber)
                .HasMaxLength(255)
                .HasColumnName("principle_id_doc_number");
            entity.Property(e => e.PrincipleName)
                .HasMaxLength(255)
                .HasColumnName("principle_name");
            entity.Property(e => e.PrincipleSuid)
                .HasMaxLength(255)
                .HasColumnName("principle_suid");
            entity.Property(e => e.Scope)
                .HasMaxLength(255)
                .HasColumnName("scope");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<TemporaryTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("temporary_table_pkey");

            entity.ToTable("temporary_table");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.DeviceId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("device_id");
            entity.Property(e => e.DeviceInfo).HasColumnName("device_info");
            entity.Property(e => e.IdDocNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.LivelinessVideo)
                .HasColumnType("oid")
                .HasColumnName("liveliness_video");
            entity.Property(e => e.NextStep).HasColumnName("next_step");
            entity.Property(e => e.NiraResponse).HasColumnName("nira_response");
            entity.Property(e => e.OptionalData1)
                .HasMaxLength(100)
                .HasColumnName("optional_data1");
            entity.Property(e => e.Selfie).HasColumnName("selfie");
            entity.Property(e => e.Step1Data).HasColumnName("step_1_data");
            entity.Property(e => e.Step1Status)
                .HasMaxLength(100)
                .HasColumnName("step_1_status");
            entity.Property(e => e.Step2Data).HasColumnName("step_2_data");
            entity.Property(e => e.Step2Status)
                .HasMaxLength(100)
                .HasColumnName("step_2_status");
            entity.Property(e => e.Step3Data).HasColumnName("step_3_data");
            entity.Property(e => e.Step3Status)
                .HasMaxLength(100)
                .HasColumnName("step_3_status");
            entity.Property(e => e.Step4Data).HasColumnName("step_4_data");
            entity.Property(e => e.Step4Status)
                .HasMaxLength(100)
                .HasColumnName("step_4_status");
            entity.Property(e => e.Step5Data)
                .HasMaxLength(100)
                .HasColumnName("step_5_data");
            entity.Property(e => e.Step5Status)
                .HasMaxLength(100)
                .HasColumnName("step_5_status");
            entity.Property(e => e.StepCompleted).HasColumnName("step_completed");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaction_pkey");

            entity.ToTable("transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Currency)
                .HasMaxLength(255)
                .HasColumnName("currency");
            entity.Property(e => e.DeductedAmount)
                .HasPrecision(38)
                .HasColumnName("deducted_amount");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.FundId).HasColumnName("fund_id");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.RemainingFund)
                .HasPrecision(38)
                .HasColumnName("remaining_fund");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(255)
                .HasColumnName("transaction_type");

            entity.HasOne(d => d.Fund).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FundId)
                .HasConstraintName("transaction_fund_fk");

            entity.HasOne(d => d.Program).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("transaction_program_fk");
        });

        modelBuilder.Entity<TrustedSpoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trusted_spocs_pkey");

            entity.ToTable("trusted_spocs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IdDocumentNo)
                .HasMaxLength(100)
                .HasColumnName("id_document_no");
            entity.Property(e => e.InvitedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("invited_on");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.SpocEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_email");
            entity.Property(e => e.SpocName)
                .HasMaxLength(100)
                .HasColumnName("spoc_name");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.Suid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("suid");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<TrustedStakeholder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trusted_stakeholders_pkey");

            entity.ToTable("trusted_stakeholders");

            entity.HasIndex(e => e.ReferenceId, "trusted_stakeholders_reference_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_time");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.OnboardingTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("onboarding_time");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.ReferenceId)
                .HasMaxLength(100)
                .HasColumnName("reference_id");
            entity.Property(e => e.ReferredBy)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("referred_by");
            entity.Property(e => e.SpocUgpassEmail)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.StakeholderType)
                .HasMaxLength(10)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("stakeholder_type");
            entity.Property(e => e.Status)
                .HasDefaultValue((short)0)
                .HasColumnName("status");
        });

        modelBuilder.Entity<TrustedUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trusted_users_pkey");

            entity.ToTable("trusted_users");

            entity.HasIndex(e => e.Email, "trusted_users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Mobile)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue((short)1)
                .HasColumnName("status");
        });

        modelBuilder.Entity<TsaDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tsa_data_pkey");

            entity.ToTable("tsa_data");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Certificate).HasColumnName("certificate");
            entity.Property(e => e.PrivateKey).HasColumnName("private_key");
        });

        modelBuilder.Entity<UrabDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("urab_details_pkey");

            entity.ToTable("urab_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuditorUraEncryptedDetails).HasColumnName("auditor_ura_encrypted_details");
            entity.Property(e => e.AuditorUraReportPdf)
                .HasMaxLength(5000)
                .HasColumnName("auditor_ura_report_pdf");
            entity.Property(e => e.CeoUraEncryptedDetails).HasColumnName("ceo_ura_encrypted_details");
            entity.Property(e => e.CeoUraReportPdf)
                .HasMaxLength(5000)
                .HasColumnName("ceo_ura_report_pdf");
            entity.Property(e => e.OrgOnboardingFormId).HasColumnName("org_onboarding_form_id");
            entity.Property(e => e.OrgUraEncryptedDetails).HasColumnName("org_ura_encrypted_details");
            entity.Property(e => e.OrgUraReportPdf)
                .HasMaxLength(5000)
                .HasColumnName("org_ura_report_pdf");
            entity.Property(e => e.SpocUraEncryptedDetails).HasColumnName("spoc_ura_encrypted_details");
            entity.Property(e => e.SpocUraReportPdf)
                .HasMaxLength(5000)
                .HasColumnName("spoc_ura_report_pdf");
        });

        modelBuilder.Entity<UserKey>(entity =>
        {
            entity.HasKey(e => e.UserIdentifier).HasName("user_key_pkey");

            entity.ToTable("user_key");

            entity.Property(e => e.UserIdentifier)
                .HasMaxLength(100)
                .HasColumnName("user_identifier");
            entity.Property(e => e.Counter)
                .HasDefaultValue(0L)
                .HasColumnName("counter");
            entity.Property(e => e.RevocationList).HasColumnName("revocation_list");
            entity.Property(e => e.UserCert).HasColumnName("user_cert");
            entity.Property(e => e.UserKey1)
                .IsRequired()
                .HasColumnName("user_key");
        });

        modelBuilder.Entity<UserVcCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_vc_credential_pkey");

            entity.ToTable("user_vc_credential");

            entity.HasIndex(e => new { e.Suid, e.DocType }, "user_vc_credential_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("doc_type");
            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.RlcUrl)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("rlc_url");
            entity.Property(e => e.Suid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("suid");
            entity.Property(e => e.VcData)
                .IsRequired()
                .HasColumnName("vc_data");
        });

        modelBuilder.Entity<ValidationRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("validation_rules_pkey");

            entity.ToTable("validation_rules");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FieldName)
                .HasMaxLength(255)
                .HasColumnName("field_name");
            entity.Property(e => e.LogicalOperator)
                .HasMaxLength(255)
                .HasColumnName("logical_operator");
            entity.Property(e => e.Operator)
                .HasMaxLength(255)
                .HasColumnName("operator");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");

            entity.HasOne(d => d.Program).WithMany(p => p.ValidationRules)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("validation_rules_program_fk");
        });

        modelBuilder.Entity<VehcileRental>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehcile_rental_pkey");

            entity.ToTable("vehcile_rental");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicantName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("applicant_name");
            entity.Property(e => e.CarNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("car_number");
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("created_on");
            entity.Property(e => e.DrivingLicenseDocument)
                .IsRequired()
                .HasColumnName("driving_license_document");
            entity.Property(e => e.DrivingLicenseNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("driving_license_number");
            entity.Property(e => e.InternationalPermit)
                .HasMaxLength(100)
                .HasColumnName("international_permit");
            entity.Property(e => e.JsonData)
                .IsRequired()
                .HasColumnName("json_data");
            entity.Property(e => e.NoOfDays)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("no_of_days");
            entity.Property(e => e.PassportDocument).HasColumnName("passport_document");
            entity.Property(e => e.PassportNumber)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("passport_number");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PickUpDate)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("pick_up_date");
            entity.Property(e => e.PidDocument).HasColumnName("pid_document");
            entity.Property(e => e.RentalAgreementFile).HasColumnName("rental_agreement_file");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<ViewOrgUserEmailList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_org_user_email_list");

            entity.Property(e => e.OrgEmailsList).HasColumnName("org_emails_list");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(100)
                .HasColumnName("subscriber_uid");
        });

        modelBuilder.Entity<VisitorCompleteDetailsNodoc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("visitor_complete_details_nodocs");

            entity.Property(e => e.AuthPinSetDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auth_pin_set_date");
            entity.Property(e => e.CerificateExpiryDate).HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateIssueDate).HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(1000)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(16)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CountryName)
                .HasMaxLength(8)
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(50)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DeviceRegistrationTime)
                .HasColumnType("character varying")
                .HasColumnName("device_registration_time");
            entity.Property(e => e.DeviceStatus)
                .HasMaxLength(16)
                .HasColumnName("device_status");
            entity.Property(e => e.EmailId)
                .HasMaxLength(64)
                .HasColumnName("email_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(128)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(16)
                .HasColumnName("gender");
            entity.Property(e => e.GeoLocation)
                .HasMaxLength(255)
                .HasColumnName("geo_location");
            entity.Property(e => e.IdDocNumber)
                .HasMaxLength(16)
                .HasColumnName("id_doc_number");
            entity.Property(e => e.IdDocType)
                .HasMaxLength(16)
                .HasColumnName("id_doc_type");
            entity.Property(e => e.LevelOfAssurance)
                .HasMaxLength(16)
                .HasColumnName("level_of_assurance");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(16)
                .HasColumnName("mobile_number");
            entity.Property(e => e.OnBoardingMethod)
                .HasMaxLength(16)
                .HasColumnName("on_boarding_method");
            entity.Property(e => e.OnBoardingTime).HasColumnName("on_boarding_time");
            entity.Property(e => e.RevocationDate)
                .HasColumnType("character varying")
                .HasColumnName("revocation_date");
            entity.Property(e => e.RevocationReason)
                .HasMaxLength(128)
                .HasColumnName("revocation_reason");
            entity.Property(e => e.SelfieThumbnailUri).HasColumnName("selfie_thumbnail_uri");
            entity.Property(e => e.SelfieUri).HasColumnName("selfie_uri");
            entity.Property(e => e.SignPinSetDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sign_pin_set_date");
            entity.Property(e => e.SubscriberStatus)
                .HasMaxLength(16)
                .HasColumnName("subscriber_status");
            entity.Property(e => e.SubscriberType)
                .HasMaxLength(16)
                .HasColumnName("subscriber_type");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(36)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.VideoType)
                .HasMaxLength(50)
                .HasColumnName("video_type");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(600)
                .HasColumnName("video_url");
        });

        modelBuilder.Entity<VoiceEnrollment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("voice_enrollment_pkey");

            entity.ToTable("voice_enrollment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("email");
            entity.Property(e => e.Suid)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("suid");
            entity.Property(e => e.VoiceRecordingFile).HasColumnName("voice_recording_file");
        });

        modelBuilder.Entity<WalletCertInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("wallet_cert_info");

            entity.Property(e => e.CertificateExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("certificate_expiry_date");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateStatus).HasColumnName("certificate_status");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrganizationDetailsId).HasColumnName("organization_details_id");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.SpocUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<WalletCertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateSerialNumber).HasName("wallet_certificate_pkey");

            entity.ToTable("wallet_certificate");

            entity.HasIndex(e => e.PkiKeyId, "wallet_certificate_pki_key_id_key").IsUnique();

            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(200)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CerificateExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateData)
                .HasMaxLength(4096)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("certificate_data");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("certificate_status");
            entity.Property(e => e.CertificateType)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("certificate_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.OrganizationUid)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("organization_uid");
            entity.Property(e => e.PkiKeyId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("pki_key_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(128)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("remarks");
            entity.Property(e => e.TransactionReferenceId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("transaction_reference_id");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.WrappedKey)
                .HasMaxLength(5000)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("wrapped_key");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.WalletCertificates)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("wallet_sign_certificate_fk");
        });

        modelBuilder.Entity<WalletCertificateNative>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("wallet_certificate_native");

            entity.Property(e => e.CertificateData)
                .HasMaxLength(4096)
                .HasColumnName("certificate_data");
            entity.Property(e => e.CertificateExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("certificate_expiry_date");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(200)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.PkiKeyId)
                .HasMaxLength(100)
                .HasColumnName("pki_key_id");
            entity.Property(e => e.WrappedData)
                .HasMaxLength(5000)
                .HasColumnName("wrapped_data");
        });

        modelBuilder.Entity<WalletIssuer>(entity =>
        {
            entity.HasKey(e => new { e.IssuerIdentifier, e.Category }).HasName("wallet_issuer_pkey");

            entity.ToTable("wallet_issuer");

            entity.Property(e => e.IssuerIdentifier)
                .HasMaxLength(100)
                .HasColumnName("issuer_identifier");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .HasColumnName("category");
            entity.Property(e => e.IssuerCounter)
                .HasDefaultValue(0L)
                .HasColumnName("issuer_counter");
            entity.Property(e => e.RevocationList)
                .IsRequired()
                .HasColumnName("revocation_list");
        });
        modelBuilder.HasSequence("car_rental_simulation_seq");
        modelBuilder.HasSequence("money_exchange_simulation_seq").IncrementsBy(50);
        modelBuilder.HasSequence("tax_payer_registration_details_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
