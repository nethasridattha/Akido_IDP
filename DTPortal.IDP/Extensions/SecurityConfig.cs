namespace DTPortal.IDP.Extensions
{
    public class SecurityConfig
    {
        public bool UseKestrelSettings { get; set; }
        public bool UseRateLimiting { get; set; }
        public bool UseSecurityHeaders { get; set; }

        public SecurityHeadersConfig SecurityHeadersConfig { get; set; }
        public KestrelConfig KestrelConfig { get; set; }
        public RateLimitingConfig RateLimitingConfig { get; set; }
    }

    public class SecurityHeadersConfig
    {
        public string XContentTypeOptions { get; set; }
        public string ReferrerPolicy { get; set; }
        public string XFrameOptions { get; set; }
        public string PermissionsPolicy { get; set; }
    }

    public class KestrelConfig
    {
        public bool AddServerHeader { get; set; }
        public KestrelLimits Limits { get; set; }
    }

    public class KestrelLimits
    {
        public string KeepAliveTimeout { get; set; }
        public string RequestHeadersTimeout { get; set; }
        public int MaxRequestLineSize { get; set; }
        public int MaxRequestHeadersTotalSize { get; set; }
        public long MaxRequestBodySize { get; set; }
        public Http2Limits Http2 { get; set; }
    }

    public class Http2Limits
    {
        public int MaxStreamsPerConnection { get; set; }
        public int HeaderTableSize { get; set; }
        public int InitialConnectionWindowSize { get; set; }
        public int InitialStreamWindowSize { get; set; }
    }

    public class RateLimitingConfig
    {
        public TokenBucketConfig TokenBucket { get; set; }
        public ConcurrencyConfig Concurrency { get; set; }
        public int RejectionStatusCode { get; set; }
    }

    public class TokenBucketConfig
    {
        public int TokenLimit { get; set; }
        public int TokensPerPeriod { get; set; }
        public int ReplenishmentPeriodSeconds { get; set; }
        public int QueueLimit { get; set; }
    }

    public class ConcurrencyConfig
    {
        public int PermitLimit { get; set; }
        public int QueueLimit { get; set; }
    }
}
