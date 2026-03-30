using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DTPortal.Core.Constants;

namespace DTPortal.Core.Services
{
    public class RoleManagementService : IRoleManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public RoleManagementService(IUnitOfWork unitOfWork,
            ILogger<RoleManagementService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<IEnumerable<RoleLookupItem>> GetRoleLookupItemsAsync()
        {
            _logger.LogInformation("-->GetRoleLookupItemsAsync");
            return await _unitOfWork.Roles.GetRoleLookupItemsAsync();
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            try
            {
                return await _unitOfWork.Roles.GetRoleByRoleIdWithActivities(id);
            }
            catch(Exception error)
            {
                _logger.LogError("GetRoleAsync Failed: {0}",
                    error.Message);
                return null;
            }
        }

        public async Task<RoleResponse> AddRoleAsync(Role role,
            IDictionary<int, bool> selectedActivityIds,
            bool makerCheckerFlag = false)
        {
            if(null == role)
            {
                _logger.LogError("Invalid input");
                return new RoleResponse("Invalid input");
            }

            var isExists = await _unitOfWork.Roles.IsRoleExistsByName(role);
            if(true == isExists)
            {
                _logger.LogError("IsRoleExistsByName: Role already exists: {0}", role.Name);
                return new RoleResponse("Role already exists, Please try with different name");
            }

            role.CreatedDate = DateTime.Now;
            role.ModifiedDate = DateTime.Now;
            role.Status = "ACTIVE";

            roleRequest request = new roleRequest()
            {
                role = role,
                selectedActivityIds = selectedActivityIds
            };

            try
            {
                await _unitOfWork.Roles.AddAsync(role);
                await _unitOfWork.SaveAsync();

                return new RoleResponse(role,"Role created successfully");
            }
            catch
            {
                // Log the exception 
                return new RoleResponse("An error occurred while creating the role." +
                    " Please contact the admin.");
            }
        }

        private async Task<RoleResponse> UpdateMCOffRoleAsync(Role role,
            IDictionary<int, bool> selectedActivityIds)
        {

            var rolesinDb = await _unitOfWork.Roles.GetRoleByRoleIdWithActivities(role.Id);
            if (null == rolesinDb)
            {
                _logger.LogError("No role activites found");
                return new RoleResponse("No role activites found");
            }

            //var roleActivity = new List<RoleActivity>();
            //foreach (var item in rolesinDb.RoleActivities)
            //{
            //    rolesinDb.RoleActivities.Remove(item);
            //    _unitOfWork.RoleActivity.Remove(item);
            //    await _unitOfWork.SaveAsync();
            //}

            foreach(var item in rolesinDb.RoleActivities)
            {
                if (selectedActivityIds.ContainsKey(item.ActivityId))

                {
                    selectedActivityIds.TryGetValue(item.ActivityId, out var val);

                    item.IsChecker = val;
                    item.IsEnabled = true;
                }
                else
                {
                    item.IsChecker = false;
                    item.IsEnabled = false;
                }
            }

            //foreach (var id in selectedActivityIds)
            //{
            //    var RoleActivity = new RoleActivity
            //    {
            //        ActivityId = id.Key,
            //        IsChecker = id.Value,
            //        LocationOnlyAccess = false,
            //        NativeAccess = true,
            //        WebAccess = false,
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        CreatedBy = role.CreatedBy,
            //        UpdatedBy = role.UpdatedBy,
            //        RoleId = role.Id
            //    };
            //    roleActivity.Add(RoleActivity);
            //}

            rolesinDb.Description = role.Description;
            rolesinDb.ModifiedDate = DateTime.Now;
            rolesinDb.UpdatedBy = role.UpdatedBy;
            //rolesinDb.RoleActivities = roleActivity;

            try
            {
                _unitOfWork.Roles.Update(rolesinDb);
                await _unitOfWork.SaveAsync();


                return new RoleResponse(role, "Role updated successfully");
            }
            catch (Exception error)
            {
                _logger.LogError("UpdateMCOffRoleAsync Failed: {0}",
                        error.Message);
                return new RoleResponse("An error occurred while updating the role. Please contact the admin.");
            }
        }

        public async Task<RoleResponse> DeleteRoleAsync(int id, string updatedBy,
            bool makerCheckerFlag = false)
        {
            var roleInDB = new Role();

            roleInDB = await _unitOfWork.Roles.GetByIdAsync(id);
            if (roleInDB == null)
            {
                return new RoleResponse("Role not found");
            }

            try
            {
                roleInDB.Status = "DELETED";
                roleInDB.UpdatedBy = updatedBy;
                roleInDB.ModifiedDate = DateTime.Now;

                _unitOfWork.Roles.Update(roleInDB);
                await _unitOfWork.SaveAsync();

                return new RoleResponse(roleInDB, "Role deleted successfully");
            }
            catch (Exception)
            {
                // Do some logging stuff
                return new RoleResponse($"An error occurred while deleting the role. Please contact the admin.");
            }
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public async Task<RoleResponse> UpdateRoleState(int id,
            bool isApproved, string reason = null)
        {
            var roleInDB = await _unitOfWork.Roles.GetByIdAsync(id);
            if (roleInDB == null)
            {
                return new RoleResponse("Role not found");
            }

            if (isApproved)
            {
                roleInDB.Status = "ACTIVE";
            }
            else
            {
                roleInDB.Status = "BLOCKED";
            }
            try
            {
                _unitOfWork.Roles.Update(roleInDB);
                await _unitOfWork.SaveAsync();

                return new RoleResponse(roleInDB);
            }
            catch (Exception)
            {
                // Do some logging stuff
                return new RoleResponse($"An error occurred while changing state of the role. Please contact the admin.");
            }
        }

        public async Task<RoleResponse> ActivateRoleAsync(int id)
        {

            var roleInDB = await _unitOfWork.Roles.GetByIdAsync(id);
            if (roleInDB == null)
            {
                return new RoleResponse("Role not found");
            }

            roleInDB.Status = "ACTIVE";

            try
            {
                _unitOfWork.Roles.Update(roleInDB);
                await _unitOfWork.SaveAsync();

                return new RoleResponse(roleInDB);
            }
            catch (Exception)
            {
                // Do some logging stuff
                return new RoleResponse($"An error occurred while changing state of the role. Please contact the admin.");
            }
        }

        public async Task<RoleResponse> DeActivateRoleAsync(int id)
        {

            var roleInDB = await _unitOfWork.Roles.GetByIdAsync(id);
            if (roleInDB == null)
            {
                return new RoleResponse("Role not found");
            }

            roleInDB.Status = "DEACTIVATED";

            try
            {
                _unitOfWork.Roles.Update(roleInDB);
                await _unitOfWork.SaveAsync();

                return new RoleResponse(roleInDB);
            }
            catch (Exception)
            {
                // Do some logging stuff
                return new RoleResponse($"An error occurred while changing state of the role. Please contact the admin.");
            }
        }
    }
}
