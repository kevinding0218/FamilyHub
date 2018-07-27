using FamilyHub.Data.Member;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Core;
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
        Task<IIOptionResponse<IOptions>> PrepareMemberRelationshipRequestAsync();
        #endregion

        #region Member Contact        
        Task<IListResponse<vmMemberContactListResponse>> GetMemberContactListByCreatedAsync(int createdBy);
        Task<IListResponse<vmMemberContactListResponse>> GetMemberContactListByRelationshipAsync(int createdBy, int memberRelationshipID);

        Task<ISingleResponse<vmMemberContactDetailResponse>> AddMemberContactAsync(vmMemberContactDetailRequest newMemberContactRequest);
        Task<ISingleResponse<vmMemberContactDetailResponse>> UpdateMemberContactAsync(int memberContactId, vmMemberContactDetailRequest updateMemberContactRequest);
        Task<IResponse> DeleteMemberContactAsync(int memberContactId);
        #endregion
    }
}
