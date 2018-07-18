using FamilyHub.Data.Member;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Member
{
    public interface IMemberRelationshipRepository : IRepository<MemberRelationship>
    {
        Task<IEnumerable<MemberRelationship>> GetListMemberRelationshipAsync();
    }
}
