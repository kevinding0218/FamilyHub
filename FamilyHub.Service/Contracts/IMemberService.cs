using FamilyHub.Data.Member;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Member;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Contracts
{
    public interface IMemberService
    {
        #region Member Relationship
        Task<IListResponse<vmMemberRelationship>> PrepareMemberRelationshipRequestAsync();
        #endregion

        #region Member Contact        
        Task<IListResponse<vmMemberContactListRequest>> GetMemberContactListByCreatedAsync(int createdBy);
        Task<IListResponse<vmMemberContactListRequest>> GetMemberContactListByRelationshipAsync(int createdBy, int memberRelationshipID);

        Task<IResponse> AddMemberContactAsync(vmMemberContactCreateRequest newMemberContactRequest);
        Task<IResponse> UpdateMemberContactAsync(int memberContactId, vmMemberContactUpdateRequest updateMemberContactRequest);
        Task<IResponse> DeleteMemberContactAsync(int memberContactId);
        #endregion
    }
}
