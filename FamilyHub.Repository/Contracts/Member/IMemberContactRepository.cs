using FamilyHub.Data.Member;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Member
{
    public interface IMemberContactRepository : IRepository<MemberContact>
    {
        Task<MemberContact> GetSingleMemberContactByIDAsync(int memberContactID);
        Task<MemberContact> GetSingleMemberContactByNameAsync(string contactFullName);

        Task<IEnumerable<MemberContact>> GetListMemberContactAsync(int createdBy = 0, int memberRelationshipID = 0);
        Task<int> AddMemberContactAsync(MemberContact entity);
        Task<int> UpdateMemberContactAsync(MemberContact entity);
        Task<int> DeleteMemberContactAsync(MemberContact entity);
    }
}
