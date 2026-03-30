using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using NLog;
using DTPortal.IDP.Extensions;

namespace DTPortal.IDP.Extensions
{
    public static class WebHostExtensions
    {
        public static void ConfigureRateLimiting(IServiceCollection services, SecurityConfig config, Logger logger)
        {
            if (config.UseRateLimiting && config.RateLimitingConfig != null)
            {
                logger.Info("Configuring Rate Limiting...");

                var tokenBucket = config.RateLimitingConfig.TokenBucket;
                var concurrency = config.RateLimitingConfig.Concurrency;

                services.AddRateLimiter(options =>
                {
                    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(ctx =>
                    {
                        var userKey = ctx.User?.Identity?.Name ?? ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                        return RateLimitPartition.GetTokenBucketLimiter(userKey, _ => new TokenBucketRateLimiterOptions
                        {
                            TokenLimit = tokenBucket.TokenLimit,
                            TokensPerPeriod = tokenBucket.TokensPerPeriod,
                            ReplenishmentPeriod = TimeSpan.FromSeconds(tokenBucket.ReplenishmentPeriodSeconds),
                            AutoReplenishment = true,
                            QueueLimit = tokenBucket.QueueLimit,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        });
                    });

                    //options.AddConcurrencyLimiter("signing", opt =>
                    //{
                    //    opt.PermitLimit = concurrency.PermitLimit;
                    //    opt.QueueLimit = concurrency.QueueLimit;
                    //});

                    options.RejectionStatusCode = config.RateLimitingConfig.RejectionStatusCode;
                });

                logger.Info($"Rate Limiting configured: TokenLimit={tokenBucket.TokenLimit}, QueueLimit={tokenBucket.QueueLimit}, " +
                            $"PermitLimit={concurrency.PermitLimit}, ConcurrencyQueueLimit={concurrency.QueueLimit}");
            }
            else
            {
                logger.Info("Rate Limiting is disabled or not configured.");
            }
        }

        public static void ConfigureSecurityHeaders(IApplicationBuilder app, SecurityConfig config, Logger logger)
        {
            if (config.UseSecurityHeaders && config.SecurityHeadersConfig != null)
            {
                logger.Info("Configuring Security Headers...");

                app.Use(async (ctx, next) =>
                {
                    var headers = ctx.Response.Headers;
                    headers["X-Content-Type-Options"] = config.SecurityHeadersConfig.XContentTypeOptions;
                    headers["X-Frame-Options"] = config.SecurityHeadersConfig.XFrameOptions;
                    headers["Referrer-Policy"] = config.SecurityHeadersConfig.ReferrerPolicy;
                    headers["Permissions-Policy"] = config.SecurityHeadersConfig.PermissionsPolicy;

                    await next();
                });

                logger.Info("Security Headers configured successfully.");
            }
            else
            {
                logger.Info("Security Headers are disabled or not configured.");
            }
        }

        public static void ConfigureKestrel(IWebHostBuilder webHost, SecurityConfig config, Logger logger)
        {
            if (config.UseKestrelSettings && config.KestrelConfig?.Limits != null)
            {
                logger.Info("Configuring Kestrel...");

                var limits = config.KestrelConfig.Limits;
                var http2 = limits.Http2;

                webHost.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.AddServerHeader = config.KestrelConfig.AddServerHeader;
                    serverOptions.Limits.KeepAliveTimeout = TimeSpan.Parse(limits.KeepAliveTimeout);
                    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.Parse(limits.RequestHeadersTimeout);
                    serverOptions.Limits.MaxRequestLineSize = limits.MaxRequestLineSize;
                    serverOptions.Limits.MaxRequestHeadersTotalSize = limits.MaxRequestHeadersTotalSize;
                    serverOptions.Limits.MaxRequestBodySize = limits.MaxRequestBodySize;

                    serverOptions.Limits.Http2.MaxStreamsPerConnection = http2.MaxStreamsPerConnection;
                    serverOptions.Limits.Http2.HeaderTableSize = http2.HeaderTableSize;
                    serverOptions.Limits.Http2.InitialConnectionWindowSize = http2.InitialConnectionWindowSize;
                    serverOptions.Limits.Http2.InitialStreamWindowSize = http2.InitialStreamWindowSize;
                });

                logger.Info("Kestrel configured successfully.");
            }
            else
            {
                logger.Info("Kestrel configuration is disabled or missing.");
            }
        }
    }

}
