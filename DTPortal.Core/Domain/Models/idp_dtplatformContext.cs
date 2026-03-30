using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DTPortal.Core.Domain.Models;

public partial class idp_dtplatformContext : DbContext
{
    public idp_dtplatformContext()
    {
    }

    public idp_dtplatformContext(DbContextOptions<idp_dtplatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<AuthScheme> AuthSchemes { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<EConsentClient> EConsentClients { get; set; }

    public virtual DbSet<NorAuthScheme> NorAuthSchemes { get; set; }

    public virtual DbSet<OperationsAuthscheme> OperationsAuthschemes { get; set; }

    public virtual DbSet<PrimaryAuthScheme> PrimaryAuthSchemes { get; set; }

    public virtual DbSet<Purpose> Purposes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleActivity> RoleActivities { get; set; }

    public virtual DbSet<Scope> Scopes { get; set; }

    public virtual DbSet<TransactionProfileConsent> TransactionProfileConsents { get; set; }

    public virtual DbSet<TransactionProfileRequest> TransactionProfileRequests { get; set; }

    public virtual DbSet<UserAuthDatum> UserAuthData { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    public virtual DbSet<UserConsent> UserConsents { get; set; }

    public virtual DbSet<UserLoginDetail> UserLoginDetails { get; set; }

    public virtual DbSet<UserProfilesConsent> UserProfilesConsents { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("activities_pkey");

            entity.ToTable("activities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(260)
                .HasColumnName("display_name");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasColumnName("hash");
            entity.Property(e => e.IsCritical).HasColumnName("is_critical");
            entity.Property(e => e.McEnabled).HasColumnName("mc_enabled");
            entity.Property(e => e.McSupported).HasColumnName("mc_supported");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(260)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
        });

        modelBuilder.Entity<AuthScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_scheme_pkey");

            entity.ToTable("auth_scheme");

            entity.HasIndex(e => e.Guid, "auth_scheme_guid_key").IsUnique();

            entity.HasIndex(e => e.Name, "auth_scheme_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasColumnName("display_name");
            entity.Property(e => e.Guid)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("guid");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasColumnName("hash");
            entity.Property(e => e.IsPrimaryAuthscheme).HasColumnName("is_primary_authscheme");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PriAuthSchCnt).HasColumnName("pri_auth_sch_cnt");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.SupportsProvisioning).HasColumnName("supports_provisioning");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Kid, e.SerialNumber }).HasName("certificates_pkey");

            entity.ToTable("certificates");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Kid)
                .HasMaxLength(50)
                .HasColumnName("kid");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(32)
                .HasColumnName("serial_number");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Data)
                .HasMaxLength(4096)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("data");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiry_date");
            entity.Property(e => e.IssueDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("issue_date");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Status)
                .HasMaxLength(16)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.HasIndex(e => e.ApplicationName, "clients_application_name_key").IsUnique();

            entity.HasIndex(e => e.ApplicationUrl, "clients_application_url_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "clients_client_id_key").IsUnique();

            entity.HasIndex(e => e.RedirectUri, "clients_redirect_uri_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("application_name");
            entity.Property(e => e.ApplicationNameArabic)
                .HasMaxLength(200)
                .HasColumnName("application_name_arabic");
            entity.Property(e => e.ApplicationType).HasColumnName("application_type");
            entity.Property(e => e.ApplicationUrl)
                .HasMaxLength(512)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("application_url");
            entity.Property(e => e.AuthScheme).HasColumnName("auth_scheme");
            entity.Property(e => e.ClientId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("client_id");
            entity.Property(e => e.ClientSecret)
                .HasMaxLength(64)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("client_secret");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.EncryptionCert).HasColumnName("encryption_cert");
            entity.Property(e => e.GrantTypes).HasColumnName("grant_types");
            entity.Property(e => e.Hash)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("hash");
            entity.Property(e => e.IsKycApplication).HasColumnName("is_kyc_application");
            entity.Property(e => e.LogoutUri).HasColumnName("logout_uri");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.OrganizationUid)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.PublicKeyCert).HasColumnName("public_key_cert");
            entity.Property(e => e.RedirectUri)
                .HasMaxLength(512)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("redirect_uri");
            entity.Property(e => e.ResponseTypes).HasColumnName("response_types");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WithPkce).HasColumnName("with_pkce");
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("configuration_pkey");

            entity.ToTable("configuration");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasColumnName("hash");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<EConsentClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("e_consent_clients_pkey");

            entity.ToTable("e_consent_clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Purposes).HasColumnName("purposes");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Client).WithMany(p => p.EConsentClients)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_client");
        });

        modelBuilder.Entity<NorAuthScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nor_auth_scheme_pkey");

            entity.ToTable("nor_auth_scheme");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthSchId).HasColumnName("auth_sch_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.PriAuthSchId).HasColumnName("pri_auth_sch_id");

            entity.HasOne(d => d.AuthSch).WithMany(p => p.NorAuthSchemes)
                .HasForeignKey(d => d.AuthSchId)
                .HasConstraintName("fk_auth_scheme");

            entity.HasOne(d => d.PriAuthSch).WithMany(p => p.NorAuthSchemes)
                .HasForeignKey(d => d.PriAuthSchId)
                .HasConstraintName("fk_pri_auth_scheme");
        });

        modelBuilder.Entity<OperationsAuthscheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("operations_authscheme_pkey");

            entity.ToTable("operations_authscheme");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthenticationRequired).HasColumnName("authentication_required");
            entity.Property(e => e.AuthenticationSchemeName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("authentication_scheme_name");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("display_name");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("hash");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.OperationName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("operation_name");
        });

        modelBuilder.Entity<PrimaryAuthScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primary_auth_scheme_pkey");

            entity.ToTable("primary_auth_scheme");

            entity.HasIndex(e => e.Guid, "primary_auth_scheme_guid_key").IsUnique();

            entity.HasIndex(e => e.Name, "primary_auth_scheme_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientVerify).HasColumnName("client_verify");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("display_name");
            entity.Property(e => e.Guid)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("guid");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("hash");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.RandPresent)
                .HasDefaultValue(0)
                .HasColumnName("rand_present");
            entity.Property(e => e.StrngMatch).HasColumnName("strng_match");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Purpose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purpose_pkey");

            entity.ToTable("purpose");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("display_name");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserConsentRequired).HasColumnName("user_consent_required");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "role_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("display_name");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("hash");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<RoleActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_activity_pkey");

            entity.ToTable("role_activity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.GeoLocCoordinates).HasColumnName("geo_loc_coordinates");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("hash");
            entity.Property(e => e.IsChecker).HasColumnName("is_checker");
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(true)
                .HasColumnName("is_enabled");
            entity.Property(e => e.LocationOnlyAccess).HasColumnName("location_only_access");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.NativeAccess).HasColumnName("native_access");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WebAccess).HasColumnName("web_access");

            entity.HasOne(d => d.Activity).WithMany(p => p.RoleActivities)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_activity");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleActivities)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role");
        });

        modelBuilder.Entity<Scope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scopes_pkey");

            entity.ToTable("scopes");

            entity.HasIndex(e => e.Name, "scopes_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClaimsList).HasColumnName("claims_list");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.DefaultScope).HasColumnName("default_scope");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(260)
                .HasColumnName("display_name");
            entity.Property(e => e.DisplayNameArabic)
                .HasMaxLength(200)
                .HasColumnName("display_name_arabic");
            entity.Property(e => e.IsClaimsPresent).HasColumnName("is_claims_present");
            entity.Property(e => e.MetadataPublish).HasColumnName("metadata_publish");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.SaveConsent).HasColumnName("save_consent");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UserConsent).HasColumnName("user_consent");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");
        });

        modelBuilder.Entity<TransactionProfileConsent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaction_profile_consent_pkey");

            entity.ToTable("transaction_profile_consent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovedProfileAttributes).HasColumnName("approved_profile_attributes");
            entity.Property(e => e.ConsentDataSignature).HasColumnName("consent_data_signature");
            entity.Property(e => e.ConsentStatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("consent_status");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.RequestedProfileAttributes).HasColumnName("requested_profile_attributes");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TransactionProfileConsents)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("fk_transaction_profile");
        });

        modelBuilder.Entity<TransactionProfileRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaction_profile_request_pkey");

            entity.ToTable("transaction_profile_request");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.FailedReason)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("failed_reason");
            entity.Property(e => e.RequestDetails).HasColumnName("request_details");
            entity.Property(e => e.Suid)
                .HasMaxLength(36)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("suid");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("transaction_id");
            entity.Property(e => e.TransactionStatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("transaction_status");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Client).WithMany(p => p.TransactionProfileRequests)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("fk_client");
        });

        modelBuilder.Entity<UserAuthDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_auth_data_pkey");

            entity.ToTable("user_auth_data");

            entity.HasIndex(e => e.UserId, "idx_user_auth_data_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthData)
                .IsRequired()
                .HasColumnName("auth_data");
            entity.Property(e => e.AuthScheme)
                .HasMaxLength(50)
                .HasColumnName("auth_scheme");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Expiry)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiry");
            entity.Property(e => e.FailedLoginAttempts).HasColumnName("failed_login_attempts");
            entity.Property(e => e.Istemporary).HasColumnName("istemporary");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_claims_pkey");

            entity.ToTable("user_claims");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.DefaultClaim).HasColumnName("default_claim");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(260)
                .HasColumnName("display_name");
            entity.Property(e => e.DisplayNameArabic)
                .HasMaxLength(200)
                .HasColumnName("display_name_arabic");
            entity.Property(e => e.MetadataPublish).HasColumnName("metadata_publish");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UserConsent).HasColumnName("user_consent");
        });

        modelBuilder.Entity<UserConsent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_consent_pkey");

            entity.ToTable("user_consent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("client_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Scopes)
                .HasMaxLength(2014)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("scopes");
            entity.Property(e => e.Suid)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("suid");
        });

        modelBuilder.Entity<UserLoginDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_login_details_pkey");

            entity.ToTable("user_login_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BadLoginTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bad_login_time");
            entity.Property(e => e.DeniedCount)
                .HasDefaultValue(0)
                .HasColumnName("denied_count");
            entity.Property(e => e.IsReversibleEncryption).HasColumnName("is_reversible_encryption");
            entity.Property(e => e.IsScrambled).HasColumnName("is_scrambled");
            entity.Property(e => e.LastAuthData)
                .HasMaxLength(360)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("last_auth_data");
            entity.Property(e => e.PriAuthSchId).HasColumnName("pri_auth_sch_id");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("user_id");
            entity.Property(e => e.WrongCodeCount)
                .HasDefaultValue(0)
                .HasColumnName("wrong_code_count");
            entity.Property(e => e.WrongPinCount)
                .HasDefaultValue(0)
                .HasColumnName("wrong_pin_count");
        });

        modelBuilder.Entity<UserProfilesConsent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_profiles_consent_pkey");

            entity.ToTable("user_profiles_consent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attributes).HasColumnName("attributes");
            entity.Property(e => e.ClientId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("client_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Profile)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("profile");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Suid)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("suid");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_table_pkey");

            entity.ToTable("user_table");

            entity.HasIndex(e => e.MailId, "user_table_mail_id_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "user_table_uuid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthData)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("auth_data");
            entity.Property(e => e.AuthScheme)
                .HasMaxLength(50)
                .HasDefaultValueSql("'DEFAULT'::character varying")
                .HasColumnName("auth_scheme");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CurrentLoginTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("current_login_time");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("full_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("hash");
            entity.Property(e => e.LastLoginTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_login_time");
            entity.Property(e => e.LockedTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("locked_time");
            entity.Property(e => e.MailId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("mail_id");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("mobile_no");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.OldStatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("old_status");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.Uuid)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("uuid");

            entity.HasOne(d => d.Role).WithMany(p => p.UserTables)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_role");
        });
        modelBuilder.HasSequence<int>("configuration_id_seq");
        modelBuilder.HasSequence<int>("kyc_method_types_id_seq");
        modelBuilder.HasSequence<int>("kyc_segments_id_seq");
        modelBuilder.HasSequence<int>("organization_verification_methods_id_seq");
        modelBuilder.HasSequence<int>("verification_methods_id_seq");
        modelBuilder.HasSequence<int>("wallet_consent_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
