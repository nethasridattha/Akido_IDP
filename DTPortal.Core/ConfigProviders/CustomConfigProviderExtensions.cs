using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.API
{
    public static class CustomConfigProviderExtensions
    { 
        public static IConfigurationBuilder AddConfigurationProvider(
            this IConfigurationBuilder builder, string path, bool optional,
            bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("File path must be a non-empty string.");
            }

            var source = new CustomConfigSource
            {
                FileProvider = null,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };

            source.ResolveFileProvider();
            builder.Add(source);
            return builder;
        }
    }
}
