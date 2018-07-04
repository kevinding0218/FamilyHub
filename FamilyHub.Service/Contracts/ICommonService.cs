using FamilyHub.Data.Common;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Contracts
{
    public interface ICommonService
    {
        #region User
        Task<IResponse> CheckSingleUserExistedAsync(string email);

        Task<ISingleResponse<vmValidateLoginUserResponse>> GetSingleUserForLoginAsync(
            string email,
            bool withCredential = true);

        Task<ISingleResponse<User>> GetSingleUserForUpdateAsync(string email);

        Task<ISingleResponse<vmRegisterUserResponse>> RegisterNewUser(vmRegisterUserRequest registerUser);

        Task<ISingleResponse<User>> UpdateUserRefreshTokenAsync(User loginUser);
        #endregion

        #region User Contact Address
        Task<ISingleResponse<User>> GetUserContactAddressAsync(int userId);
        #endregion
    }
}
