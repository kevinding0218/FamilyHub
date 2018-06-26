using FamilyHub.Data.Common;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Common
{
    public class ContactAddressRepository : Repository<ContactAddress>, IContactAddressRepository
    {
        public ContactAddressRepository(FamilyHubDbContext dbContext)
                    : base(dbContext)
        {
        }

        public async Task<int> AddContactAddressAsync(ContactAddress entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateContactAddressAsync(ContactAddress changes)
        {
            Update(changes);

            return await CommitChangesAsync();
        }
    }
}
