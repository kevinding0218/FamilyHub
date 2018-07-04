using AutoMapper;
using FamilyHub.Data;
using FamilyHub.Data.Common;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class CommonService : ServiceFactory, ICommonService
    {
        public CommonService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public CommonService(IMapper mapper, FamilyHubDbContext dbContext)
            : base(mapper, dbContext)
        {
        }

        public CommonService(IMapper mapper, IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(mapper, userInfo, dbContext)
        {
        }

        #region User
        public async Task<IResponse> CheckSingleUserExistedAsync(string email)
        {
            var response = new Response();

            try
            {
                var existedUser = await UserRepository
                    .GetSingleUserInfoAsync(new User(email));

                if (existedUser == null) response.Message = ResponseMessageDisplay.NonExisted;
                else
                {
                    response.Message = ResponseMessageDisplay.IsExisted;
                    // Throw exception if duplicate email account existed
                    throw new FamilyHubException(string.Format(CommonMessageDisplays.ExistedUserExceptionMessage, email));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<ISingleResponse<vmValidateLoginUserResponse>> GetSingleUserForLoginAsync(string email, bool withCredential = true)
        {
            var response = new SingleResponse<vmValidateLoginUserResponse>();

            try
            {
                var existedUser = await UserRepository.GetSingleUserInfoAsync(new User(email), withCredential: withCredential);

                response.Message = ResponseMessageDisplay.Valid;
                response.Model = _mapper.Map<User, vmValidateLoginUserResponse>(existedUser);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<ISingleResponse<User>> GetSingleUserForUpdateAsync(string email)
        {
            var response = new SingleResponse<User>();

            try
            {
                var existedUser = await UserRepository.GetSingleUserInfoAsync(new User(email));

                response.Message = ResponseMessageDisplay.Valid;
                response.Model = existedUser;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<ISingleResponse<vmRegisterUserResponse>> RegisterNewUser(vmRegisterUserRequest registerUser)
        {
            var response = new SingleResponse<vmRegisterUserResponse>();

            try
            {
                var newUser = _mapper.Map<vmRegisterUserRequest, User>(registerUser);
                newUser.Active = true;
                newUser.IsCoreUser = true;
                newUser.LastLogin = DateTime.Now;

                // Create new user
                await UserRepository.AddUserAsync(newUser);

                response.Message = ResponseMessageDisplay.Success;
                response.Model = _mapper.Map<User, vmRegisterUserResponse>(newUser);
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
        #endregion

        #region User Contact Address
        public async Task<ISingleResponse<User>> GetUserContactAddressAsync(int userId)
        {
            var response = new SingleResponse<User>();

            try
            {
                response.Model = await UserRepository
                    .GetSingleUserInfoAsync(new User(userId), withContact: true);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion
    }
}
