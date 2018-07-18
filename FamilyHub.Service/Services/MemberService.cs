using AutoMapper;
using FamilyHub.Data;
using FamilyHub.Data.Member;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Member;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class MemberService : ServiceFactory, IMemberService
    {
        public MemberService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public MemberService(IMapper mapper, FamilyHubDbContext dbContext)
            : base(mapper, dbContext)
        {
        }

        public MemberService(IMapper mapper, IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(mapper, userInfo, dbContext)
        {
        }

        #region Member Relationship
        public async Task<IListResponse<MemberRelationship>> PrepareMemberRelationshipRequestAsync()
        {
            var response = new ListResponse<MemberRelationship>();

            try
            {
                response.Model = await MemberRelationshipRepository.GetListMemberRelationshipAsync();
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion

        #region Member Contact
        public Task<IListResponse<MemberContact>> GetMemberContactListByCreatedAsync(int createdBy)
        {
            throw new NotImplementedException();
        }

        public Task<IListResponse<MemberContact>> GetMemberContactListByRelationshipAsync(int createdBy, int memberRelationshipID)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> AddMemberContactAsync(vmMemberContactCreateRequest newMemberContactRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> UpdateMemberContactAsync(int memberContactId, vmMemberContactUpdateRequest updateMemberContactRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> DeleteMemberContactAsync(MemberContact memberContact)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
