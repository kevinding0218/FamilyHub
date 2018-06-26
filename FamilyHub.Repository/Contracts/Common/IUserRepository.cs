using FamilyHub.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Common
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserInfo(User user, bool withCredential = false, bool withContact = false);

        Task<Int32> AddUserAsync(User entity);

        Task<Int32> UpdateUserAsync(User changes);
    }
}
