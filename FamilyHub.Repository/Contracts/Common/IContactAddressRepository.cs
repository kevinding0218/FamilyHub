using FamilyHub.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Common
{
    public interface IContactAddressRepository : IRepository<ContactAddress>
    {
        Task<int> AddContactAddressAsync(ContactAddress entity);

        Task<int> UpdateContactAddressAsync(ContactAddress changes);
    }
}
