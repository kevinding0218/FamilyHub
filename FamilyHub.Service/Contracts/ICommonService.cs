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
        Task<ISingleResponse<User>> GetUserAsync(
            string email,
            bool withCredential = false,
            bool withContact = false,
            bool checkIfExisted = false);

        Task<ISingleResponse<User>> GetUserContactAddressAsync(int userId);

        Task<ISingleResponse<User>> RegisterNewUser(User newUser);

        Task<ISingleResponse<User>> UpdateUserRefreshTokenAsync(User loginUser);
    }
}
