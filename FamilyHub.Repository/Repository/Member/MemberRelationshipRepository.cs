using FamilyHub.Data;
using FamilyHub.Data.Member;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Member;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Member
{
    public class MemberRelationshipRepository : Repository<MemberRelationship>, IMemberRelationshipRepository
    {
        public MemberRelationshipRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public MemberRelationshipRepository(IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<IEnumerable<MemberRelationship>> GetListMemberRelationshipAsync()
            => await GetListAsync();
    }
}
