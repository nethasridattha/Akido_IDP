using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace DTPortal.Core.Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserTable, idp_dtplatformContext>,
        IUserRepository
    {
        private readonly ILogger _logger;
        public UserRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsUserExists(UserTable user)
        {
            try
            {
                return await Context.UserTables
                    .AsNoTracking().AnyAsync(u => u.MobileNo == user.MobileNo || u.MailId == user.MailId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsUserExists::Database exception");
                return false;
            }
        }
        public async Task<bool> IsUserExistsWithEmail(UserTable user)
        {
            try
            {
                return await Context.UserTables
                    .AsNoTracking().AnyAsync(u => u.MailId == user.MailId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsUserExistsWithEmail::Database exception");
                return false;
            }
        }
        public async Task<bool> IsUserExistsWitMobile(UserTable user)
        {
            try
            {
                return await Context.UserTables
                    .AsNoTracking().AnyAsync(u => u.MobileNo == user.MobileNo);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsUserExistsWitMobile::Database exception");
                return false;
            }
        }
        public async Task<PaginatedList<UserTable>> GetAllUsersWithRolesAsync(int offset, int count)
        {
            //return await Context.UserTables
            //    .Where(x => x.Status != "Deleted")
            //    .Include(u => u.Role)
            //    .AsNoTracking().Skip((offset - 1) * count).Take(count).ToListAsync();
            try
            {
                IQueryable<UserTable> list = Context.UserTables
                    .Where(x => x.Status != "DELETED")
                    .Include(u => u.Role);

                return await PaginatedList<UserTable>.CreateAsync(list, offset, 10);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetAllUsersWithRolesAsync::Database exception");
                return null;
            }
        }

        public async Task<List<UserTable>> GetAllUsersWithRolesAsync()
        {
            try
            {
                return  await Context.UserTables
                    .Where(x => x.Status != "DELETED")
                    .Include(u => u.Role).ToListAsync();

            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetAllUsersWithRolesAsync::Database exception");
                return null;
            }
        }

        public async Task<UserTable> GetUserByIdWithRoleAsync(int id)
        {
            try
            {
                return await Context.UserTables
                    .Include(u => u.Role)
                    .AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserByIdWithRoleAsync::Database exception");
                return null;
            }
        }

        public async Task<bool> IsUserExistsbyUserNameAsync(string userName)
        {
            try
            {
                return await Context.UserTables
                    .AsNoTracking().AnyAsync(u => u.FullName == userName);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsUserExistsbyUserNameAsync::Database exception");
                return false;
            }
        }
        public async Task<bool> IsUserExistsbyUuidAsync(string Uuid)
        {
            try
            {
                return await Context.UserTables
                    .AsNoTracking().AnyAsync(u => u.Uuid == Uuid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsUserExistsbyUuidAsync::Database exception");
                return false;
            }
        }
        public async Task<UserTable> GetUserbyNameAsync(string userName)
        {
            try
            {
                return await Context.UserTables.AsNoTracking().SingleOrDefaultAsync(uu => uu.FullName == userName);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserbyNameAsync::Database exception");
                return null;
            }
        }
        public async Task<UserTable> GetUserbyUuidAsync(string Uuid)
        {
            try
            {
                return await Context.UserTables.AsNoTracking().SingleOrDefaultAsync(uu => uu.Uuid == Uuid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserbyUuidAsync::Database exception");
                return null;
            }
        }
        public async Task<UserTable> GetUserbyEmailAsync(string emailId)
        {
            try
            {
                return await Context.UserTables.AsNoTracking().SingleOrDefaultAsync(uu => uu.MailId == emailId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserbyEmailAsync::Database exception");
                return null;
            }
        }

        public async Task<UserTable> GetUserbyPhonenoAsync(string phoneNo)
        {
            try
            {
                return await Context.UserTables.AsNoTracking().SingleOrDefaultAsync(uu => uu.MobileNo == phoneNo);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserbyPhonenoAsync::Database exception");
                return null;
            }
        }
        public async Task<List<string>> SearchUserbyEmailAsync(string emailId)
        {
            try
            {
                return await Context.UserTables.Where(uu=>uu.MailId.Contains(emailId)).Select(mm=>mm.MailId).Take(10).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserbyEmailAsync::Database exception");
                return null;
            }
        }

        public async Task<List<string>> SearchUserbyPhoneAsync(string phone)
        {
            try
            {
                return await Context.UserTables.Where(uu => uu.MobileNo.Contains(phone)).Select(mm => mm.MobileNo).Take(10).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserbyEmailAsync::Database exception");
                return null;
            }
        }
    }
}
