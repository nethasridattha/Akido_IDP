using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using DTPortal.Core.Utilities;

namespace DTPortal.Core.Persistence.Repositories
{
    public class ConfigurationRepository : GenericRepository<Configuration, idp_dtplatformContext>,
            IConfigurationRepository
    {
        private readonly ILogger _logger;
        public ConfigurationRepository(idp_dtplatformContext context,
            ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public Configuration GetConfigurationByName(string name)
        {
            //var connstring = Context.Database.GetDbConnection().ConnectionString;

            //StringBuilder stringBuilder = new StringBuilder();
            //foreach (var config in Context.Configurations)
            //{
            //    stringBuilder.AppendLine(config.Name);
            //}
            //stringBuilder.AppendLine(Context.Database.GetDbConnection().ConnectionString);
            //System.IO.File.WriteAllText("MyFile99.txt", stringBuilder.ToString());


            try
            {
                return Context.Configurations.FirstOrDefault(c => c.Name == name);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetConfigurationByName:: Database exception");
                Monitor.SendException(ex);
                return null;
            }
        }

        public async Task<Configuration> GetConfigurationByNameAsync(string name)
        {
            try
            {
                return await Context.Configurations.FirstOrDefaultAsync(c => c.Name == name);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetConfigurationByName:: Database exception");
                Monitor.SendException(ex);
                return null;
            }
        }
    }
}
