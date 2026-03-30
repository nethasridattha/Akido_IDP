using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Domain.Lookups;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DTPortal.Core.Services
{
    public class AuthSchemeService: IAuthSchemeSevice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthSchemeService> _logger;

        public AuthSchemeService(IUnitOfWork unitOfWork,
             ILogger<AuthSchemeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

      

        public async Task<AuthSchemeResponse> CreateAuthSchemeAsync(AuthScheme authScheme, IList<string> primaryAuthSchemes)
        {
            authScheme.CreatedBy = "sysadmin";
            authScheme.UpdatedBy = "sysadmin";
            authScheme.CreatedDate = DateTime.Now;
            authScheme.ModifiedDate = DateTime.Now;

            await _unitOfWork.AuthScheme.AddAsync(authScheme);

            try
            {
                await _unitOfWork.SaveAsync();
                //return new AuthSchemeResponse(authScheme);
            }
            catch (Exception)
            {
                return new AuthSchemeResponse("An error occurred while creating the authentication scheme. Please contact the admin.");
            }

            var AuthSchmeinDb = await _unitOfWork.AuthScheme.GetAuthSchemeByNameAsync(authScheme.Name);
            if(null == AuthSchmeinDb)
            {
                return new AuthSchemeResponse("An error occurred while creating the authentication scheme. Please contact the admin.");
            }

            foreach (var item in primaryAuthSchemes) 
            {
                var primaryAuthSchemeId = await _unitOfWork.PrimaryAuthScheme.GetPrimaryAuthSchemeByPrimaryAuthSchemeAsync(item);

                var norAuthScheme = new NorAuthScheme
                {
                    AuthSchId = AuthSchmeinDb.Id,
                    PriAuthSchId = primaryAuthSchemeId.Id
                };
                await _unitOfWork.NorAuthScheme.AddAsync(norAuthScheme);
                
                try
                {
                    await _unitOfWork.SaveAsync();
                    //return new AuthSchemeResponse(authScheme);
                }
                catch (Exception)
                {
                    return new AuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
                }
            }

            return new AuthSchemeResponse(authScheme, "Authentication scheme created successfully");
        }

        public async Task<AuthScheme> GetAuthSchemeByIdAsync(int id)
        {
            return await _unitOfWork.AuthScheme.GetByIdAsync(id);
        }

        public async Task<AuthScheme> GetAuthSchemeAsync(string AuthSchemeName)
        {
            return await _unitOfWork.AuthScheme.GetAuthSchemeByNameAsync(AuthSchemeName);
        }

        public async Task<IEnumerable<string>> GetPrimaryAuthSchemesOfAuthScheme(int id)
        {
            var primaryAuthSchemesIds = await _unitOfWork.NorAuthScheme.GetPrimaryAuthSchemeIds(id);

            var Ids = new List<string>();
            foreach(var item in primaryAuthSchemesIds)
            {
                var primaryAuthSchemeName = await _unitOfWork.PrimaryAuthScheme.GetByIdAsync((int)item.PriAuthSchId);
                Ids.Add(primaryAuthSchemeName.Name);
            }

            return Ids;
        }

        public async Task<AuthSchemeResponse> UpdateAuthSchemeAsync(AuthScheme authScheme, IList<string> primaryAuthSchemes)
        {
            var authSchemeInDb = await _unitOfWork.AuthScheme.GetByIdAsync(authScheme.Id);

            authSchemeInDb.Name = authScheme.Name;
            authSchemeInDb.PriAuthSchCnt = authScheme.PriAuthSchCnt;
            authSchemeInDb.Description = authScheme.Description;
            authSchemeInDb.ModifiedDate = DateTime.Now;
            authSchemeInDb.UpdatedBy = "system";

            _unitOfWork.AuthScheme.Update(authSchemeInDb);
            try
            {
                await _unitOfWork.SaveAsync();
                //return new AuthSchemeResponse(authScheme);
            }
            catch (Exception)
            {
                return new AuthSchemeResponse("An error occurred while updating the authentication scheme. Please contact the admin.");
            }

            var norAuthSchms = await _unitOfWork.NorAuthScheme.GetNorAuthSchmIDsbyAuthSchm(authScheme.Id);

            foreach (var norauthId in norAuthSchms)
            {
                _unitOfWork.NorAuthScheme.Remove(norauthId);

                // Commit changes
                await _unitOfWork.SaveAsync();
            }

            foreach (var item in primaryAuthSchemes)
            {
                var primaryAuthSchemeId = await _unitOfWork.PrimaryAuthScheme.GetPrimaryAuthSchemeByPrimaryAuthSchemeAsync(item);

                var norAuthScheme = new NorAuthScheme
                {
                    AuthSchId = authSchemeInDb.Id,
                    PriAuthSchId = primaryAuthSchemeId.Id
                };

                await _unitOfWork.NorAuthScheme.AddAsync(norAuthScheme);
                try
                {
                    await _unitOfWork.SaveAsync();
                    //return new AuthSchemeResponse(authScheme);
                }
                catch (Exception)
                {
                    return new AuthSchemeResponse("An error occurred while updating the authentication scheme. Please contact the admin.");
                }
            }
            return new AuthSchemeResponse(authScheme, "Authentication scheme updated successfully");
            //try
            //{
            //    await _unitOfWork.SaveAsync();
            //    return new AuthSchemeResponse(authScheme);
            //}
            //catch (Exception error)
            //{
            //    return new AuthSchemeResponse("An error occurred while updating the authentication scheme. Please contact the admin.");
            //}
        }

        public async Task<AuthSchemeResponse> DeleteAuthSchemeAsync(int id)
        {
            try
            {
                var authSchemeInDb = await _unitOfWork.AuthScheme.GetByIdAsync(id);

                authSchemeInDb.Status = "DELETED";

                _unitOfWork.AuthScheme.Update(authSchemeInDb);

                var norAuthSchms = await _unitOfWork.NorAuthScheme.GetNorAuthSchmIDsbyAuthSchm(id);

                foreach (var norauthId in norAuthSchms)
                {
                    _unitOfWork.NorAuthScheme.Remove(norauthId);

                    // Commit changes
                    await _unitOfWork.SaveAsync();
                }

                await _unitOfWork.SaveAsync();

                return new AuthSchemeResponse(authSchemeInDb, "Authentication scheme deleted successfully");
            }
            catch
            {
                return new AuthSchemeResponse("Authentication scheme cannot be deleted, Please contact admin");
            }

            
            //var isAssigned = await _unitOfWork.AuthScheme.IsAuthSchemeAssignedAsync(authSchemeInDb.Id);
            //if (true == isAssigned)
            //{
            //    return new AuthSchemeResponse("authentication could not be deleted, as it is assigned to operation.");
            //}

            //_unitOfWork.AuthScheme.Remove(authSchemeInDb);

            //try
            //{
            //    await _unitOfWork.SaveAsync();

            //    return new AuthSchemeResponse(authSchemeInDb);
            //}
            //catch
            //{
            //    return new AuthSchemeResponse("authentication scheme delete failed. Please contact the admin.");
            //}
        }

        public async Task<IEnumerable<AuthScheme>> ListAuthSchemesAsync()
        {
            return await _unitOfWork.AuthScheme.ListAuthSchemesAsync();
        }

        public async Task<IEnumerable<AuthSchemesLookupItem>> GetAuthSchemesLookupItemsAsync()
        {
            return await _unitOfWork.AuthScheme.GetAuthSchemeLookupItemsAsync();
        }

        public async Task<List<string>> GetDefaultAuthScheme()
        {
            List<string> authSchemeList = new List<string>();

            var configuration = await _unitOfWork.Configuration.GetConfigurationByNameAsync("DEFAULT_AUTH_SCHEME");
            if (configuration == null)
            {
                _logger.LogError("Failed to get Configuration");
                return null;
            }
            var authSchemeId = int.Parse(configuration.Value);

            var norAuthSchemes = await _unitOfWork.NorAuthScheme.GetNorAuthSchmIDsbyAuthSchm(authSchemeId);

            foreach (var norAuthId in norAuthSchemes)
            {
                var primaryAuthScheme = await _unitOfWork.PrimaryAuthScheme.GetPrimaryAuthSchemeByIdAsync((int)norAuthId.PriAuthSchId);
                if (primaryAuthScheme == null)
                {
                    _logger.LogError("Failed to get primaryAuthScheme");
                    return null;
                }
                authSchemeList.Add(primaryAuthScheme.Name);
            }
            return authSchemeList;
        }
        public async Task<List<string>> GetAuthSchemesListById(int authSchemeId)
        {
            List<string> authSchemeList = new List<string>();

            var norAuthSchemes = await _unitOfWork.NorAuthScheme.GetNorAuthSchmIDsbyAuthSchm(authSchemeId);

            foreach (var norAuthId in norAuthSchemes)
            {
                var primaryAuthScheme = await _unitOfWork.PrimaryAuthScheme.GetPrimaryAuthSchemeByIdAsync((int)norAuthId.PriAuthSchId);
                if (primaryAuthScheme == null)
                {
                    _logger.LogError("Failed to get primaryAuthScheme");
                    return null;
                }
                authSchemeList.Add(primaryAuthScheme.Name);
            }
            return authSchemeList;
        }
    }
}
