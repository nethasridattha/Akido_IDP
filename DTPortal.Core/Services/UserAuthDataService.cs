using System.Threading.Tasks;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using System;

namespace DTPortal.Core.Services
{
    public class UserAuthDataService : IUserAuthDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAuthDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<UserAuthDataResponse> ProvisionUser(UserAuthDatum userAuthData)
        {

            var isExists = await _unitOfWork.UserAuthData.IsUserAuthDataExists(userAuthData.UserId, userAuthData.AuthScheme);
            if (false == isExists)
            {
                userAuthData.CreatedDate = DateTime.Now;
                userAuthData.ModifiedDate = DateTime.Now;

                await _unitOfWork.UserAuthData.AddAsync(userAuthData);
            }
            else
            {
                var userAuthDatainDb = await _unitOfWork.UserAuthData.GetByIdAsync(userAuthData.Id);

                userAuthDatainDb.AuthData = userAuthData.AuthData;
                userAuthDatainDb.UpdatedBy = userAuthData.UpdatedBy;
                userAuthDatainDb.ModifiedDate = DateTime.Now;
                userAuthDatainDb.FailedLoginAttempts = userAuthData.FailedLoginAttempts;

                _unitOfWork.UserAuthData.Update(userAuthData);
            }
            try
            {
                await _unitOfWork.SaveAsync();
                return new UserAuthDataResponse(userAuthData);
            }
            catch
            {
                return new UserAuthDataResponse("Provision user failed, Please contact admin");
            }
        }

    public async Task SaveAsync()
        {
            await _unitOfWork.SaveAsync();
        }
    }
}
