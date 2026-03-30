using AppShieldRestAPICore.Filters;
using DTPortal.Core.ConfigProviders;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Persistence.Repositories;
using DTPortal.Core.Services;
using DTPortal.Core.Utilities;
using DTPortal.IDP.Attribute;
using DTPortal.IDP.Converter;
using DTPortal.IDP.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var securityConfig = builder.Configuration
                         .GetSection("SecurityConfig")
                         .Get<SecurityConfig>();

    // Call each setup function only if the feature is enabled
    if (securityConfig?.UseRateLimiting == true)
        DTPortal.IDP.Extensions.WebHostExtensions.ConfigureRateLimiting(builder.Services, securityConfig, logger);

    if (securityConfig?.UseKestrelSettings == true)
        DTPortal.IDP.Extensions.WebHostExtensions.ConfigureKestrel(builder.WebHost, securityConfig, logger);


    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SupportNonNullableReferenceTypes();

        options.OperationFilter<FormSchemaOperationFilter>();

    });

    await ConfigureServices(builder);

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddNLogWeb();
    builder.Services.AddProblemDetails();

    var app = builder.Build();

    logger.Info("WebApplication build successful");

    if (securityConfig?.UseSecurityHeaders == true)
        DTPortal.IDP.Extensions.WebHostExtensions.ConfigureSecurityHeaders(app, securityConfig, logger);

    string basePath = builder.Configuration["BasePath"];
    if (!string.IsNullOrEmpty(basePath))
    {
        app.Use(async (context, next) =>
        {
            context.Request.PathBase = basePath;
            await next.Invoke();
        });
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseForwardedHeaders();
    }
    else
    {
        app.UseForwardedHeaders();
    }

    app.Use(async (context, next) =>
    {
        if (context.Request.ContentLength > 0 &&
            (context.Request.Method == "POST" ||
             context.Request.Method == "PUT" ||
             context.Request.Method == "PATCH"))
        {
            var contentType = context.Request.ContentType ?? "";

            if (contentType.Contains("application/json"))
            {
                context.Request.EnableBuffering();

                var buffer = new byte[1];
                var bytesRead = await context.Request.Body.ReadAsync(buffer, 0, 1);

                context.Request.Body.Position = 0;

                if (bytesRead == 1)
                {
                    var first = buffer[0];

                    bool looksLikeJson =
                        first == '{' ||
                        first == '[' ||
                        first == '"' ||
                        first == 't' ||
                        first == 'f' ||
                        first == 'n' ||
                        first == ' ' ||
                        first == '\t' ||
                        first == '\r' ||
                        first == '\n' ||
                        (first >= '0' && first <= '9');

                    if (!looksLikeJson)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(new
                        {
                            success = false,
                            message = "Invalid request payload",
                            result = new[]
                            {
                            new
                            {
                                field = "body",
                                errors = new[]
                                {
                                    "Request body must be valid JSON."
                                }
                            }
                        }
                        });

                        return;
                    }
                }
            }
        }

        await next.Invoke();
    });

    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;

            var isApiRequest =
                context.Request.Path.StartsWithSegments("/api") ||
                context.GetEndpoint()?.Metadata?.GetMetadata<ApiControllerAttribute>() != null;

            if (isApiRequest)
            {
                context.Response.ContentType = "application/json";

                if (exception is JsonSerializationException || exception is JsonReaderException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    await context.Response.WriteAsJsonAsync(new
                    {
                        success = false,
                        message = "Invalid request format.",
                        result = (object)null,
                        detail = app.Environment.IsDevelopment() ? exception.Message : null
                    });

                    return;
                }

                if (exception is ArgumentException || exception is FormatException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    await context.Response.WriteAsJsonAsync(new
                    {
                        success = false,
                        message = exception.Message,
                        result = (object)null
                    });

                    return;
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = "An unexpected error occurred.",
                    result = (object)null,
                    detail = app.Environment.IsDevelopment() ? exception?.Message : null
                });
            }
            else
            {
                context.Response.Redirect("/Error");
            }
        });
    });

    app.UseStatusCodePages(async statusCodeContext =>
    {
        var response = statusCodeContext.HttpContext.Response;
        var request = statusCodeContext.HttpContext.Request;

        if (!response.HasStarted && !request.Path.StartsWithSegments("/api"))
        {
            await statusCodeContext.Next(statusCodeContext.HttpContext);
        }
    });

    app.UseStaticFiles();

    app.UseRouting();

    app.UseSwagger();

    app.UseSwaggerUI();

    app.UseCors();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseSession();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    await app.RunAsync();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, ex.Message);
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

async Task ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddHttpClient();

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders =
            ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        // Only loopback proxies are allowed by default.
        // Clear that restriction because forwarders are enabled by explicit 
        // configuration.
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    });
    var environment = builder.Environment;

    // Load secrets from Vault only in Staging or Production
    if (environment.IsStaging() || environment.IsProduction())
    {
        var vaultAddress = builder.Configuration["Vault:Address"];
        var vaultToken = builder.Configuration["Vault:Token"];
        var secretPath = builder.Configuration["Vault:SecretPath"];

        // Initialize Vault client
        var authMethod = new TokenAuthMethodInfo(vaultToken);
        var vaultClientSettings = new VaultClientSettings(vaultAddress, authMethod);
        var vaultClient = new VaultClient(vaultClientSettings);

        // Fetch secret data from Vault
        Secret<SecretData> secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(
            path: secretPath,
            mountPoint: "secret"
        );

        var data = secret.Data.Data;

        // Override configuration values
        var memoryConfig = new Dictionary<string, string>
        {
            ["ConnectionStrings:IDPConnString"] = data["ConnectionStrings:IDPConnString"]?.ToString(),
            ["ConnectionStrings:RAConnString"] = data["ConnectionStrings:RAConnString"]?.ToString(),
            ["ConnectionStrings:PKIConnString"] = data["ConnectionStrings:PKIConnString"]?.ToString(),
            ["RedisConnString"] = data["RedisConnString"]?.ToString(),
            ["JWTConfig"] = data["JWTConfig"]?.ToString(),
        };

        // Inject Vault secrets into configuration
        builder.Configuration.AddInMemoryCollection(memoryConfig);
    }
    else
    {
        Console.WriteLine("Skipping Vault secrets loading (Development environment).");
    }
    // Now you can access them like normal config values
    var idpConnectionString = builder.Configuration.GetConnectionString("IDPConnString");
    var raConnectionString = builder.Configuration.GetConnectionString("RAConnString");
    var pkiConnectionString = builder.Configuration.GetConnectionString("PKIConnString");
    var redisConn = builder.Configuration["RedisConnString"];
    var jwtConfig = builder.Configuration["JWTConfig"];
    if (environment.IsDevelopment())
    {
        var jwtConfiguration = builder.Configuration.GetSection("JWTConfig").Get<JWTConfig>();
        jwtConfig = JsonConvert.SerializeObject(jwtConfiguration);
    }

    if (String.Equals(builder.Configuration["DataPersistenceRequired"], "True"))
    {
        Console.WriteLine(builder.Configuration["RedisConnString"]);

        string configString = builder.Configuration["RedisConnString"];

        if (builder.Configuration.GetValue<bool>("EncryptionEnabled"))
        {
            logger.Info("Decrypt Text Started");
            configString = PKIMethods.Instance.
                    PKIDecryptSecureWireData(configString);
            logger.Info("Decrypt Text completed : :" + configString);
        }
        ;


        var redis = await ConnectionMultiplexer.ConnectAsync(configString);
        builder.Services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");

        Console.WriteLine("Redis connection succedded");
    }

    var jwtConfigDeserialized = JsonConvert.DeserializeObject<JWTConfig>(jwtConfig);
    builder.Services.AddSingleton(jwtConfigDeserialized);

    builder.Services.AddCors();
    builder.Services.AddTransient<ICorsPolicyProvider, CustomCorsPolicyProvider>();
    Console.WriteLine("Initialized started");

    builder.Services.AddScoped<Microsoft.Extensions.Logging.ILogger, Microsoft.Extensions.Logging.Logger<UnitOfWork>>();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddSingleton<ILogClient, LogClient>();
    builder.Services.AddScoped<ILocalJWTManager, LocalJWTManager>();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(Config =>
       {
           var SessionName = builder.Configuration["IDPSessionName"].ToString();
           if (string.IsNullOrEmpty(SessionName))
               SessionName = "IDPSession";
           Config.Cookie.Name = SessionName;
       });

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
    });

    builder.Services.AddAuthorization();
    builder.Services.AddFido2(options =>
    {
        options.ServerDomain = builder.Configuration["fido2:serverDomain"];
        options.ServerName = builder.Configuration["fido2:serverName"];
        options.Origin = builder.Configuration["fido2:origin"];
        options.TimestampDriftTolerance = builder.Configuration.GetValue<int>("fido2:timestampDriftTolerance");
    });
    builder.Services.AddControllersWithViews();
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession();
    builder.Services.AddScoped<IPushNotificationClient, PushNotificationClient>();
    builder.Services.AddScoped<ITokenManager, TokenManager>();
    builder.Services.AddScoped<IPKIServiceClient, PKIServiceClient>();
    builder.Services.AddScoped<IRAServiceClient, RAServiceClient>();


    Console.WriteLine("Database initialization started");


    if (builder.Configuration.GetValue<bool>("EncryptionEnabled"))
    {
        try
        {
            logger.Info("Decrypt Text Started");
            idpConnectionString = PKIMethods.Instance.
                    PKIDecryptSecureWireData(idpConnectionString);
            logger.Info("WebApplication build successful");
            logger.Info("Decrypt Text completed : :" + idpConnectionString);
            raConnectionString = PKIMethods.Instance.
                    PKIDecryptSecureWireData(raConnectionString);
            pkiConnectionString = PKIMethods.Instance.
                    PKIDecryptSecureWireData(pkiConnectionString);
        }
        catch (Exception ex)
        {
            logger.Error("Decrypt Text Error : :" + ex.Message);
        }
    }

    builder.Services.AddDbContext<idp_dtplatformContext>(options =>
        options.UseNpgsql(idpConnectionString));

    builder.Services.AddDbContext<ra_0_2Context>(options =>
        options.UseNpgsql(raConnectionString));

    logger.Info("Database initialization success");
    Console.WriteLine("Database initialization success");
    //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ICacheClient, CacheClient>();
    builder.Services.AddScoped<IEmailSender, EmailSender>();
    builder.Services.AddScoped<IIpRestriction, IPRrestriction>();
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<IUserConsentService, UserConsentService>();
    builder.Services.AddScoped<ITokenManagerService, TokenManagerService>();
    builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
    builder.Services.AddScoped<IGlobalConfiguration, GlobalConfiguration>();
    builder.Services.AddSingleton<IKafkaConfigProvider, KafkaConfigProvider>();
    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<IUserManagementService, UserManagementService>();
    builder.Services.AddScoped<CustomAuthorizationAttribute>();
    builder.Services.AddScoped<IUserInfoService, UserInfoService>();
    builder.Services.AddScoped<IAssertionValidationClient, AssertionValidationClient>();
    builder.Services.AddScoped<IPKILibrary, PKILibrary>();
    builder.Services.AddScoped<ICertificateService, CertificateService>();
    builder.Services.AddScoped<IAssertionValidationClient, AssertionValidationClient>();
    builder.Services.AddScoped<IRoleManagementService, RoleManagementService>();
    builder.Services.AddScoped<ICredentialService, CredentialService>();
    builder.Services.AddScoped<IHelper, Helper>();
    builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();
    builder.Services.AddScoped<ISubscriberService, SubscriberService>();
    builder.Services.AddScoped<IGoogleMapService, GoogleMapService>();
    builder.Services.AddScoped<IPurposeService, PurposeService>();
    builder.Services.AddScoped<ITransactionProfileRequestService, TransactionProfileRequestService>();
    builder.Services.AddScoped<ITransactionProfileConsentService, TransactionProfileConsentService>();
    builder.Services.AddScoped<ITransactionProfileStatusService, TransactionProfileStatusService>();
    builder.Services.AddScoped<IUserClaimService, UserClaimService>();
    builder.Services.AddScoped<IScopeService, ScopeService>();
    builder.Services.AddScoped<IAttributeServiceTransactionsService, AttributeServiceTransactionsService>();
    builder.Services.AddScoped<IUserProfileService, UserProfileService>();
    builder.Services.AddScoped<IEConsentService, EConsentService>();
    builder.Services.AddScoped<IUserProfilesConsentService, UserProfilesConsentService>();
    builder.Services.AddScoped<IAuthSchemeSevice, AuthSchemeService>();
    builder.Services.AddScoped<ISDKAuthenticationService, SDKAuthenticationService>();
    builder.Services.AddScoped<IMobileAuthenticationService, MobileAuthenticationService>();
    builder.Services.AddScoped<IOrganizationService, DTPortal.Core.Services.OrganizationService>();
    builder.Services.AddScoped<IUserDataService, UserDataService>();
    builder.Services.AddScoped<ICertificateIssuanceService, CertificateIssuanceService>();
    builder.Services.AddScoped<ILogReportService, LogReportService>();
    builder.Services.AddScoped<ISessionService, SessionService>();
    builder.Services.AddScoped<Helper>();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddScoped<IMessageLocalizer, MessageLocalizer>();


    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<JsonExceptionFilter>();
    })
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore;

    options.SerializerSettings.MissingMemberHandling =
        Newtonsoft.Json.MissingMemberHandling.Error;

    options.SerializerSettings.Converters.Add(new StrictStringConverter());
    options.SerializerSettings.Converters.Add(new StrictIntConverter());
    options.SerializerSettings.Converters.Add(new StrictBoolConverter());

    options.SerializerSettings.Error = (sender, args) =>
    {
        var currentError = args.ErrorContext.Error;

        if (args.ErrorContext.Member != null &&
            args.ErrorContext.Error.Message.Contains("member"))
        {
            return;
        }

        args.ErrorContext.Handled = false;
    };
});

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            return new BadRequestObjectResult(new
            {
                success = false,
                message = "Validation failed",
                errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value.Errors.Select(e => e.ErrorMessage)
                    )
            });
        };
    });
    // Initialize Monitor with full config
    DTPortal.Core.Utilities.Monitor.Initialize(builder.Configuration, logger);
}