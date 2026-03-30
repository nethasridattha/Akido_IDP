using DTPortal.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.API
{
    public class CustomConfigProvider : JsonConfigurationProvider
    {
        public CustomConfigProvider(CustomConfigSource source) : base(source)
        {

        }

        public override void Load(Stream stream)
        {
            var settings = DecryptConfiguration(stream);
            if(null == settings)
                throw new ArgumentNullException("Settings cannot be null");

            Data = settings;
        }

        private IDictionary<string, string> DecryptConfiguration(Stream stream)
        {
            try
            {
                var encConfigData = new StreamReader(stream).ReadToEnd();

                Console.WriteLine("Encrypted configuration\n");
                Console.WriteLine(encConfigData);
                var configData = PKIMethods.Instance.PKIDecryptSecureWireData(
                    encConfigData);

                var configObj = JsonConvert.DeserializeObject
                    <Settings>(configData);
                Console.WriteLine("Plain configuration\n");
                Console.WriteLine(configData);

                var configValues = new Dictionary<string, string>
                {
                    {"IDPConnString", configObj.connectionStrings.IDPConnString},
                    {"RAConnString", configObj.connectionStrings.RAConnString},
                    {"PKIConnString", configObj.connectionStrings.PKIConnString},
                    {"RedisConnString" , configObj.redisConnString }
                };

                return configValues;
            }
            catch(Exception error)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(error);
                Console.WriteLine(error.Message);
                return null;
            }
        }
    }

    public class CustomConfigSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new CustomConfigProvider(this);
        }
    }
}
