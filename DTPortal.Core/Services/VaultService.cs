using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace DTPortal.Core.Services
{
    public class VaultService
    {
        private readonly IVaultClient _vaultClient;
        private readonly string _secretPath;
        private readonly string _mountPoint;

        public VaultService(IConfiguration configuration)
        {
            var vaultAddress = configuration["Vault:Address"];
            var vaultToken = configuration["Vault:Token"];
            _secretPath = configuration["Vault:SecretPath"]; // should be "dotnet-app"
            _mountPoint = "secret"; // KV engine name

            var authMethod = new TokenAuthMethodInfo(vaultToken);
            var vaultClientSettings = new VaultClientSettings(vaultAddress, authMethod);
            _vaultClient = new VaultClient(vaultClientSettings);
        }

        public async Task<Dictionary<string, string>> GetSecretsAsync()
        {
            var secret = await _vaultClient.V1.Secrets.KeyValue.V2
                .ReadSecretAsync(_secretPath, mountPoint: _mountPoint);

            return secret.Data.Data.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        }
    }

}
