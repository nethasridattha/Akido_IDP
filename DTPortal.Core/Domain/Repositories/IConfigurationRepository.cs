using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IConfigurationRepository : IGenericRepository<Configuration>
    {
        public Configuration GetConfigurationByName(string name);
        public Task<Configuration> GetConfigurationByNameAsync(string name);
    }
}
