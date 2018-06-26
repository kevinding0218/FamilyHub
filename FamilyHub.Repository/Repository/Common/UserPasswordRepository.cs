using FamilyHub.Data.Common;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Common
{
    public class UserPasswordRepository : Repository<UserPassword>, IUserPasswordRepository
    {
        public UserPasswordRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<int> AddUserPasswordAsync(UserPassword entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateUserPasswordAsync(UserPassword changes)
        {
            Update(changes);

            return await CommitChangesAsync();
        }
    }
}
