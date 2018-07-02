using FamilyHub.Data.Common;
using FamilyHub.Service.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Contracts
{
    public interface ICommonService
    {
        #region User
        Task<ISingleResponse<User>> GetSingleUserAsync(
            string email,
            bool withCredential = false,
            bool withContact = false,
            bool checkIfExisted = false);

        Task<ISingleResponse<User>> RegisterNewUser(User newUser);

        Task<ISingleResponse<User>> UpdateUserRefreshTokenAsync(User loginUser);
        #endregion

        #region User Contact Address
        Task<ISingleResponse<User>> GetUserContactAddressAsync(int userId);
        #endregion
    }
}
