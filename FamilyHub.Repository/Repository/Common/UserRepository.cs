using FamilyHub.Data.Common;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Common
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<User> GetUserInfo(User user, bool withCredential = false, bool withContact = false)
        {
            if (withCredential)
            {
                return await GetSingleOrDefaultAsync(
                        predicate: u => (u.Email == user.Email && u.UserPasswords.Any(up => up.Active == true)),
                        include: obj => obj.Include(entity => entity.UserPasswords)
                    );
            }
            else if (withContact)
            {
                return await GetSingleOrDefaultAsync(
                        predicate: u => u.UserID == user.UserID,
                        include: obj => obj.Include(entity => entity.ContactAddressFk)
                    );
            }
            else
            {
                return await GetSingleOrDefaultAsync(
                        predicate: u => u.UserID == user.UserID
                    );
            }
        }

        public async Task<int> AddUserAsync(User entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }



        public async Task<int> UpdateUserAsync(User changes)
        {
            Update(changes);

            return await CommitChangesAsync();
        }
    }
}
