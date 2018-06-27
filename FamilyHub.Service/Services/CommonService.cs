using FamilyHub.Data.Common;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class CommonService : CoreService, ICommonService
    {
        public CommonService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<ISingleResponse<User>> GetUserAsync(
            string email,
            bool withCredential = false,
            bool withContact = false,
            bool checkIfExisted = false
        )
        {
            var response = new SingleResponse<User>();

            try
            {
                var existedUser = await UserRepository
                    .GetUserInfo(new User(email), withCredential, withContact);

                if (!checkIfExisted)
                {
                    response.Message = ResponseMessageDisplay.Valid;
                    response.Model = existedUser;
                }
                else
                {
                    if (existedUser == null) response.Message = ResponseMessageDisplay.NonExisted;
                    else
                    {
                        response.Message = ResponseMessageDisplay.IsExisted;
                        // Throw exception if duplicate email account existed
                        throw new FamilyHubException(string.Format(CommonMessageDisplays.ExistedUserExceptionMessage, email));
                    }
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<ISingleResponse<User>> GetUserContactAddressAsync(int userId)
        {
            var response = new SingleResponse<User>();

            try
            {
                response.Model = await UserRepository
                    .GetUserInfo(new User(userId), withContact: true);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<ISingleResponse<User>> RegisterNewUser(User newUser)
        {
            var response = new SingleResponse<User>();

            try
            {
                newUser.Active = true;
                newUser.IsCoreUser = true;
                newUser.LastLogin = DateTime.Now;
                newUser.CreatedOn = DateTime.Now;

                // Create new user
                await UserRepository.AddUserAsync(newUser);

                response.Message = ResponseMessageDisplay.Success;
                response.Model = newUser;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<ISingleResponse<User>> UpdateUserRefreshTokenAsync(User loginUser)
        {
            var response = new SingleResponse<User>();

            try
            {
                await UserRepository.UpdateUserAsync(loginUser);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
    }
}
