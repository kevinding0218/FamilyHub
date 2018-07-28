using FamilyHub.Data;
using FamilyHub.Data.Member;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Member;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Member
{
    public class MemberContactRepository : Repository<MemberContact>, IMemberContactRepository
    {
        public MemberContactRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public MemberContactRepository(IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<MemberContact> GetSingleMemberContactByIDAsync(int memberContactID)
        {
            return await GetSingleOrDefaultAsync(
                     predicate: (pm => pm.MemberContactID == memberContactID),
                     include: obj => obj.Include(entity => entity.MemberRelationshipFK)
                                     .Include(entity => entity.MemberImageSourceFK)
                 );
        }

        public async Task<MemberContact> GetSingleMemberContactByNameAsync(string contactFullName)
            => await GetSingleOrDefaultAsync(predicate: pm => pm.FullName == contactFullName);

        public async Task<IEnumerable<MemberContact>> GetListMemberContactAsync(int createdBy, int memberRelationshipID = 0)
        {
            if (memberRelationshipID > 0)
            {
                return await GetListAsync(
                        predicate: (p => p.CreatedBy == createdBy && p.MemberRelationshipID == memberRelationshipID),
                        include: obj => obj.Include(entity => entity.MemberRelationshipFK)
                                        .Include(entity => entity.MemberImageSourceFK)
                    );
            }
            else
            {
                return await GetListAsync(
                        predicate: (p => p.CreatedBy == createdBy),
                        include: obj => obj.Include(entity => entity.MemberRelationshipFK)
                                        .Include(entity => entity.MemberImageSourceFK)
                    );
            }
        }

        public async Task<int> AddMemberContactAsync(MemberContact entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateMemberContactAsync(MemberContact entity)
        {
            Update(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> DeleteMemberContactAsync(MemberContact entity)
        {
            // Remove(entity);

            return await CommitChangesAsync();
        }
    }
}
