using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class CacheClient : ICacheClient
    {
        private readonly SSOConfig ssoConfig;
        private readonly ILogger<CacheClient> _logger;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IConfiguration _configuration;
        private static Lazy<IConnectionMultiplexer> _Connection = null;
        private readonly ConfigurationOptions configuration;
        public static bool initLibrary = false;

        public CacheClient(ILogger<CacheClient> logger,
            IGlobalConfiguration globalConfiguration, IConfiguration Configuration)
        {
            _logger = logger;
            _globalConfiguration = globalConfiguration;
            _configuration = Configuration;

            _logger.LogDebug("-->CacheClient");

            // Get SSO Configuration
            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed in cache client");
                throw new NullReferenceException();
            }

            if (false == initLibrary)
            {
                try
                {
                    // Get Configuration string from database.
                    string configString = Configuration["RedisConnString"];

                    if (Configuration.GetValue<bool>("EncryptionEnabled"))
                    {
                        configString = PKIMethods.Instance.
                                PKIDecryptSecureWireData(configString);
                    };
                    configuration = ConfigurationOptions.Parse(configString);
                    configuration.ClientName = "REDIS CLIENT";

                    _Connection = new Lazy<IConnectionMultiplexer>(() =>
                    {
                        ConnectionMultiplexer redis =
                        ConnectionMultiplexer.Connect(configuration);
                        return redis;
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError("Redis Server Initialization Failed:{0}",
                        ex.Message);
                    throw;
                }
                initLibrary = true;
            }

            _logger.LogDebug("<--CacheClient");
        }

        private TimeSpan? GetExpiry(string name)
        {
            _logger.LogDebug("-->GetExpiry");

            // Validate input parameters
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError("Invalid Input Parameter");
                return null;
            }

            TimeSpan? expiry;

            try { 
                if (name == "TemporarySession" || name == "MobileAuthTemporarySession")
                {
                    // Set TemporarySession expirty for 10 minutes
                    expiry = new TimeSpan(0,
                        ssoConfig.sso_config.temporary_session_timeout, 0);
                }
                else if (name == "GlobalSession")
                {
                    // Set GlobalSession expirty for 90 minutes
                    expiry = new TimeSpan(0,
                        ssoConfig.sso_config.session_timeout, 0);
                }
                else if (name == "AuthorizationCode")
                {
                    // Set AuthorizationCode expirty for 5 minutes
                    expiry = new TimeSpan(0,
                        ssoConfig.sso_config.authorization_code_timeout, 0);
                }
                else if (name == "AccessToken")
                {
                    // Set AccessToken expiry
                    expiry = new TimeSpan(0,
                        ssoConfig.sso_config.access_token_timeout, 0);
                }
                else if (name == "UserSessions")
                {
                    // Set GlobalSession expiry
                    expiry = new TimeSpan(0,
                        ssoConfig.sso_config.session_timeout, 0);
                }
                else if (name == "Refreshtoken")
                {
                    // Set Refreshtoken expiry
                    expiry = new TimeSpan(0,
                        ssoConfig.sso_config.session_timeout, 0);
                }
                else
                {
                    expiry = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetExpiry Failed:{0}", ex.Message);
                expiry = null;
                return expiry;
            }

            _logger.LogDebug("<--GetExpiry");
            return expiry;
        }

        public IConnectionMultiplexer Connection {
            get {
                return _Connection.Value;
            }
        }

        // For the default database
        public IDatabase Database => Connection.GetDatabase();

        // Get record from cache
        public async Task<T> Get1<T>(string name, string key,
            CommandFlags flags = CommandFlags.None)
        {
            _logger.LogDebug("-->GetCacheRecord");

            // Validate input parameters
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(key))
            {
                _logger.LogError("Invalid Input Parameter");
                return default;
            }

            // Prepare key
            var finalKey = name + ":" + key;

            try
            {
                // Get the serialized object from cache
                var serializedObject = await Database.StringGetAsync(finalKey, flags);
                if (!serializedObject.HasValue)
                {
                    _logger.LogWarning("Failed to get cache record");
                    return default;
                }

                // Return deserialized object
                return JsonConvert.DeserializeObject<T>(serializedObject);
            }
            catch(Exception error)
            {
                _logger.LogError("Get Cache Record Failed:{0}", error.Message);
                return default;
            }
        }

        // Get record from cache
        public async Task<T> Get<T>(string name, string key,
            CommandFlags flags = CommandFlags.None)
        {
            _logger.LogDebug("-->GetCacheRecord");

            // Validate input parameters
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(key))
            {
                _logger.LogError("Invalid Input Parameter");
                return default;
            }

            // Prepare key
            var finalKey = name + ":" + key;

            try
            {
                // Get the serialized object from cache
                var serializedObject = await Database.StringGetAsync(finalKey, flags);
                if (!serializedObject.HasValue)
                {
                    _logger.LogWarning("Failed to get cache record");
                    return default;
                }

                // Return deserialized object
                return JsonConvert.DeserializeObject<T>(serializedObject);
            }
            catch (RedisCommandException error)
            {
                _logger.LogError("Add Cache Record Failed:{0}", error.Message);
                throw new CacheException(CacheCodes.CommandException, error.Message);
            }
            catch (RedisTimeoutException error)
            {
                _logger.LogError("Add Cache Record Failed:{0}", error.Message);
                throw new CacheException(CacheCodes.TimeoutException, error.Message);
            }
            catch (RedisConnectionException error)
            {
                _logger.LogError("Add Cache Record Failed:{0}", error.Message);
                throw new CacheException(CacheCodes.ConnectionException, error.Message);
            }
            catch (Exception error)
            {
                _logger.LogError("Add Cache Record Failed:{0}", error.Message);
                throw new CacheException(CacheCodes.E_FAIL, error.Message);
            }
        }

        // Set record in cache
        public async Task<(int retValue, string errorMsg)> Add(string name, string key,
            object value, When when = When.Always,
            CommandFlags flags = CommandFlags.None)
        {
            _logger.LogDebug("-->AddCacheRecord");

            // Validate input parameters
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(key) ||
                null == value)
            {
                _logger.LogError("Invalid Input Parameter");
                return (CacheCodes.E_FAIL, "Invalid Parameters");
            }

            // Serialize the object
            var serializedObject = JsonConvert.SerializeObject(value);
            if (serializedObject == null)
            {
                _logger.LogError("JsonConvert.SerializeObject() failed");
                return (CacheCodes.E_FAIL, "JsonConvert.SerializeObject() failed");
            }

            //get expiry time
            var expirytime = GetExpiry(name);

            // Prepare key
            var finalKey = name + ":" + key;

            try { 
                // Set the object
                bool status = await Database.StringSetAsync(finalKey,
                    serializedObject, expirytime, when, flags);
                if (!status)
                {
                    _logger.LogError("Failed to set object");
                    return (CacheCodes.E_FAIL, "Failed to set object");
                }
            }
            catch (RedisCommandException error)
            {
                _logger.LogError("Add Cache Record Failed:{0}", error.Message);
                return (CacheCodes.CommandException, error.Message);
            }
            catch (RedisTimeoutException error)
            {
                _logger.LogError("Add Cache Record Failed:{0}", error.Message);
                return (CacheCodes.TimeoutException, error.Message);
            }
            catch (RedisConnectionException error)
            {
                _logger.LogError("Add Cache Record Failed:{0}", error.Message);
                return (CacheCodes.ConnectionException, error.Message);
            }
            catch (Exception error)
            {
                _logger.LogError("Add Cache Record Failed:{0}", error.Message);
                return (CacheCodes.E_FAIL, error.Message);
            }

            _logger.LogDebug("<--AddCacheRecord");
            return (0, null);
        }

        // Delete record from cache
        public async Task<(int retValue, string errorMsg)> Remove(string name,
            string key, CommandFlags flags = CommandFlags.FireAndForget)
        {
            _logger.LogDebug("-->DeleteCacheRecord");

            // Validate input parameters
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(key))
            {
                _logger.LogError("Invalid Input Parameter");
                return (CacheCodes.E_FAIL, "Invalid Parameters");
            }

            // Prepare key
            var finalKey = name + ":" + key;

            try
            {
                // Delete object from cache
                bool isDeleted = await Database.KeyDeleteAsync(finalKey, flags);
                if (!isDeleted)
                {
                    _logger.LogWarning("Failed to delete record from cache");
                    return (CacheCodes.FailedToDelete, "Failed to delete");
                }
            }
            catch (RedisCommandException error)
            {
                _logger.LogError("Delete Cache Record Failed:{0}", error.Message);
                return (CacheCodes.CommandException, error.Message);
            }
            catch (RedisTimeoutException error)
            {
                _logger.LogError("Delete Cache Record Failed:{0}", error.Message);
                return (CacheCodes.TimeoutException, error.Message);
            }
            catch (RedisConnectionException error)
            {
                _logger.LogError("Delete Cache Record Failed:{0}", error.Message);
                return (CacheCodes.ConnectionException, error.Message);
            }
            catch (Exception error)
            {
                _logger.LogError("Delete Cache Record Failed:{0}", error.Message);
                return (CacheCodes.E_FAIL, error.Message);
            }

            return (0, null);
        }

        // Check if record exists in cache
        public async Task<(int retValue, string errorMsg)> Exists(string name,
            string key, CommandFlags flags = CommandFlags.None)
        {
            _logger.LogDebug("-->IsCacheRecordExists");

            // Validate input parameters
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(key))
            {
                _logger.LogError("Invalid Input Parameter");
                return (CacheCodes.E_FAIL, "Invalid Parameters");
            }

            // Prepare key
            var finalKey = name + ":" + key;

            try
            {
                // Check if key exist or not
                var isExist = await Database.KeyExistsAsync(finalKey, flags);
                if (!isExist)
                {
                    _logger.LogWarning("Cache Record Not Exists");
                    return (0, null);
                }
            }
            catch (RedisCommandException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.CommandException, error.Message);
            }
            catch (RedisTimeoutException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.TimeoutException, error.Message);
            }
            catch (RedisConnectionException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.ConnectionException, error.Message);
            }
            catch (Exception error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.E_FAIL, error.Message);
            }

            _logger.LogDebug("<--IsCacheRecordExists");
            return (CacheCodes.KeyExist, null);
        }

        // Check if record exists in cache
        public (int retValue, string errorMsg) KeyExists(string name,
            string key, CommandFlags flags = CommandFlags.None)
        {
            _logger.LogDebug("-->IsCacheRecordExists");

            // Validate input parameters
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(key))
            {
                _logger.LogError("Invalid Input Parameter");
                return (CacheCodes.E_FAIL, "Invalid Parameters");
            }

            // Prepare key
            var finalKey = name + ":" + key;

            try
            {
                // Check if key exist or not
                var isExist = Database.KeyExists(finalKey, flags);
                if (!isExist)
                {
                    _logger.LogWarning("Cache Record Not Exists");
                    return (0, null);
                }
            }
            catch (RedisCommandException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.CommandException, error.Message);
            }
            catch (RedisTimeoutException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.TimeoutException, error.Message);
            }
            catch (RedisConnectionException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.ConnectionException, error.Message);
            }
            catch (Exception error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.E_FAIL, error.Message);
            }

            _logger.LogDebug("<--IsCacheRecordExists");
            return (CacheCodes.KeyExist, null);
        }

        // Check time to leave of key in cache
        public async Task<(int retValue, double totalSeconds)> TimeToLeave(string name,
            string key)
        {
            _logger.LogDebug("-->TimeToLeave");

            // Validate input parameters
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(key))
            {
                _logger.LogError("Invalid Input Parameter");
                return (-1, 0);
            }

            // Prepare key
            var finalKey = name + ":" + key;
            
            try
            {
                // Check if key exist or not
                var seconds = await Database.KeyTimeToLiveAsync(finalKey);
                if (null == seconds)
                {
                    _logger.LogWarning("Record does not exists or does not have timeout");
                    return (104, 0);
                }

                return (0, seconds.Value.TotalSeconds);
            }
            catch (RedisCommandException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.CommandException, 0);
            }
            catch (RedisTimeoutException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.TimeoutException, 0);
            }
            catch (RedisConnectionException error)
            {
                _logger.LogError("Check Cache Record Exists:{0}", error.Message);
                return (CacheCodes.ConnectionException, 0);
            }
            catch (Exception error)
            {
                _logger.LogError("Check Cache Record TimeToLeave Exists:{0}",
                    error.Message);
                return (-1, 0);
            }
        }

        // GetAll Results from Session Manager(Redis)
        public async Task<IList<T>> GetAll<T>(string name)
        {
            _logger.LogDebug("-->GetAllRecords");
            var command = string.Format("*{0}*", name);
            var nextCursor = 0;
            var isTrue = true;
            IList<T> result = new List<T>();

            try
            {
                while (isTrue)
                {
                    var list = (RedisResult[])await Database.ExecuteAsync("Scan", nextCursor,
                        "match", command);
                    nextCursor = (int)list[0];
                    if (nextCursor == 0)
                    {
                        isTrue = false;
                    }
                    var keys = (RedisKey[])list[1];

                    foreach (var item in keys)
                    {
                        //Get the serialized object from cache
                        var serializedObject = await Database.StringGetAsync(item.ToString());
                        if (!serializedObject.HasValue)
                            return default;

                        // Return deserialized object
                        result.Add(JsonConvert.DeserializeObject<T>(serializedObject));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllRecords Failed:{0}", ex.Message);
                return null;
            }

            _logger.LogDebug("<--GetAllRecords");
            return result;
        }

        public async Task<(int nextIndex, IList<T>)> GetAll<T>(string name,
            int index, int count)
        {
            _logger.LogDebug("-->GetAllRecords");

            // Validate input parameters
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError("Invalid Input Parameter");
                return (0, null);
            }

            var command = string.Format("*{0}*", name);
            var nextIndex = 0;
            IList<T> result = new List<T>();

            try
            {
                var list = (RedisResult[])await Database.ExecuteAsync("Scan", index,
                    "match", command, "count", count);
                nextIndex = (int)list[0];

                var keys = (RedisKey[])list[1];

                foreach (var item in keys)
                {
                    //Get the serialized object from cache
                    var serializedObject = await Database.StringGetAsync(item.ToString());
                    if (!serializedObject.HasValue)
                        return default;

                    // Return deserialized object
                    result.Add(JsonConvert.DeserializeObject<T>(serializedObject));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllRecords Failed:{0}", ex.Message);
                return (0, null);
            }

            _logger.LogDebug("<--GetAllRecords");
            return (nextIndex, result);
        }
    }
}
