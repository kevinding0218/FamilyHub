using FamilyHub.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Common
{
    public interface IUserPasswordRepository : IRepository<UserPassword>
    {
        Task<Int32> AddUserPasswordAsync(UserPassword entity);

        Task<Int32> UpdateUserPasswordAsync(UserPassword changes);
    }
}
