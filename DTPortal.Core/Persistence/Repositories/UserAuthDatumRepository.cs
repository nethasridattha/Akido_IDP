using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.Core.Persistence.Repositories
{
    public class UserAuthDatumRepository : GenericRepository<UserAuthDatum, idp_dtplatformContext>,
            IUserAuthDatumRepository
    {
        private readonly ILogger _logger;
        public UserAuthDatumRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }
        public async Task<bool> IsUserAuthDataExists(string userId, string PrimauthSchm)
        {
            try
            {
                return await Context.UserAuthData.AsNoTracking().AnyAsync(uu => uu.UserId == userId && uu.AuthScheme == PrimauthSchm);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsUserAuthDataExists::Database exception");
                return false;
            }
        }

        public async Task<UserAuthDatum> GetUserAuthDataAsync(string userId, string PrimauthSchm)
        {
            try
            {
                return await Context.UserAuthData.SingleOrDefaultAsync(uu => uu.UserId == userId && uu.AuthScheme == PrimauthSchm && uu.Status == "ACTIVE");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserAuthDataAsync::Database exception");
                return null;
            }
        }

        public async Task<List<UserAuthDatum>> GetAllUserAuthDataAsync(string userId, string PrimauthSchm)
        {
            try
            {
                return await Context.UserAuthData.Where(uu => uu.UserId == userId && uu.AuthScheme == PrimauthSchm).ToListAsync(); ;
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserAuthDataAsync::Database exception");
                return null;
            }
        }

        public async Task<UserAuthDatum> GetUserTempAuthDataAsync(string userId, string PrimauthSchm)
        {
            try
            {
                return await Context.UserAuthData.AsNoTracking().SingleOrDefaultAsync(uu => uu.UserId == userId && uu.AuthScheme == PrimauthSchm && uu.Status == "HOLD");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserAuthDataAsync::Database exception");
                return null;
            }
        }

        public async Task<UserAuthDatum> GetUserInactiveAuthDataAsync(string userId, string PrimauthSchm)
        {
            try
            {
                return await Context.UserAuthData.AsNoTracking().SingleOrDefaultAsync(uu => uu.UserId == userId && uu.AuthScheme == PrimauthSchm && uu.Status == "INACTIVE");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserAuthDataAsync::Database exception");
                return null;
            }
        }
    }
}
