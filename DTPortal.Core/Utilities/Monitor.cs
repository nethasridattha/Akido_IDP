using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public static class Monitor
    {
        private static bool _sendExceptions;
        private static string _dsn;
        private static string _endpoint;
        private static string _publicKey;
        private static readonly HttpClient _client = new HttpClient();
        private static Logger _logger;

        public static void Initialize(IConfiguration configuration, Logger logger)
        {
            _sendExceptions = bool.TryParse(configuration["MonitorSettings:SendExceptions"], out var flag) && flag;
            _logger = logger;
            if (!_sendExceptions)
            {
                _logger.Info("Monitor is disabled (SendExceptions=false).");
                return;
            }

            _dsn = configuration["MonitorSettings:Dsn"];
            var baseEndpoint = configuration["MonitorSettings:Endpoint"];
            _publicKey = configuration["MonitorSettings:PublicKey"];
            var projectId = configuration["MonitorSettings:ProjectId"];

            if (string.IsNullOrWhiteSpace(_dsn) ||
                string.IsNullOrWhiteSpace(baseEndpoint) ||
                string.IsNullOrWhiteSpace(_publicKey) ||
                string.IsNullOrWhiteSpace(projectId))
            {
                throw new Exception("Failed to initialize Sentry - Required MonitorSettings are missing.");
            }

            _endpoint = $"{baseEndpoint.TrimEnd('/')}/{projectId}/store/";

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("X-Sentry-Auth",
                $"Sentry sentry_version=7, sentry_client=custom/1.0, sentry_key={_publicKey}");

            _logger.Info("Monitor initialized successfully.");
        }

        public static void SendException(Exception ex)
        {
            if (!_sendExceptions) return;
            Task.Run(async () =>
            {
                try
                {
                    _logger.Info(ex, " - Sending exception to monitoring server (fire-and-forget)...");
                    await SendToServer(ex.ToString());
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Error in fire-and-forget SendException.");
                }
            });
        }

        public static void SendMessage(string message)
        {
            if (!_sendExceptions) return;
            Task.Run(async () =>
            {
                try
                {
                    _logger.Info(message, " - Sending exception to monitoring server (fire-and-forget)...");
                    await SendToServer(message);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Error in fire-and-forget SendException.");
                }
            });
        }

        private static async Task SendToServer(string message)
        {
            string eventId = Guid.NewGuid().ToString("N");

            var payload = new
            {
                event_id = eventId,
                message = message,
                level = "error",
                timestamp = DateTime.UtcNow.ToString("o"),
                sdk = new
                {
                    name = "custom-sdk",
                    version = "1.0.0"
                }
            };

            try
            {
                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpResponseMessage response = await _client.PostAsync(_endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.Info("Error successfully sent to monitoring server.");
                }
                else
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    _logger.Error("Failed to send error. Status code: {StatusCode}. Details: {Details}",
                        response.StatusCode, errorDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception while sending error to monitoring server.");
            }
        }
    }
}
