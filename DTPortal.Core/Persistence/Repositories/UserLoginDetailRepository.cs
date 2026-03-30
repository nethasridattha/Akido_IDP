using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Persistence.Repositories
{
    public class UserLoginDetailRepository : GenericRepository<UserLoginDetail, idp_dtplatformContext>,
        IUserLoginDetailRepository
    {
        private readonly ILogger _logger;
        public UserLoginDetailRepository(idp_dtplatformContext context,
            ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<UserLoginDetail> GetUserLoginDetailAsync(string userId)
        {
            try
            {
                return await Context.UserLoginDetails.SingleOrDefaultAsync(uu => uu.UserId == userId);
            }
            catch(Exception error)
            {
                _logger.LogError(error, "GetUserLoginDetailAsync:: Database exception");
                throw;
            }

        }
    }
}
