using AutoMapper;
using FamilyHub.Data;
using FamilyHub.Data.Member;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
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
        public async Task<IListResponse<vmMemberRelationship>> PrepareMemberRelationshipRequestAsync()
        {
            var response = new ListResponse<vmMemberRelationship>();

            try
            {
                var listMemberRelationshipFromDb = await MemberRelationshipRepository.GetListMemberRelationshipAsync();
                response.Model = _mapper.Map<IEnumerable<MemberRelationship>, IEnumerable<vmMemberRelationship>>(listMemberRelationshipFromDb);
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
        public async Task<IListResponse<vmMemberContactListRequest>> GetMemberContactListByCreatedAsync(int createdBy)
        {
            var response = new ListResponse<vmMemberContactListRequest>();

            try
            {
                var listMemberContactFromDb = await MemberContactRepository.GetListMemberContactAsync(createdBy);
                response.Model = _mapper.Map<IEnumerable<MemberContact>, IEnumerable<vmMemberContactListRequest>>(listMemberContactFromDb);
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public Task<IListResponse<vmMemberContactListRequest>> GetMemberContactListByRelationshipAsync(int createdBy, int memberRelationshipID)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse> AddMemberContactAsync(vmMemberContactCreateRequest newMemberContactRequest)
        {
            var response = new Response();

            try
            {
                var newMemberContact = _mapper.Map<vmMemberContactCreateRequest, MemberContact>(newMemberContactRequest);
                // Create new payment payor
                await MemberContactRepository.AddMemberContactAsync(newMemberContact);

                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> UpdateMemberContactAsync(int memberContactId, vmMemberContactUpdateRequest updateMemberContactRequest)
        {
            var response = new Response();

            try
            {
                var duplicateMemberContact = await MemberContactRepository.GetSingleMemberContactByNameAsync(updateMemberContactRequest.FullName);

                if (duplicateMemberContact != null && duplicateMemberContact.MemberContactID != memberContactId)
                {
                    response.Message = ResponseMessageDisplay.Duplicate;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(MemberMessageDisplay.MemberContactAlreadyExistedMessage, updateMemberContactRequest.FullName));
                }
                else
                {
                    var memberContactFromDB = await MemberContactRepository.GetSingleMemberContactByIDAsync(memberContactId);
                    if (memberContactFromDB == null)
                    {
                        response.Message = ResponseMessageDisplay.NotFound;
                        // Throw exception if duplicate existed
                        throw new FamilyHubException(string.Format(MemberMessageDisplay.MemberContactNotFoundMessage));
                    }
                    else
                    {
                        _mapper.Map<vmMemberContactUpdateRequest, MemberContact>(updateMemberContactRequest, memberContactFromDB);
                        await MemberContactRepository.UpdateMemberContactAsync(memberContactFromDB);

                        response.Message = ResponseMessageDisplay.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> DeleteMemberContactAsync(int memberContactId)
        {
            var response = new Response();

            try
            {
                var memberContactFromDB = await MemberContactRepository.GetSingleMemberContactByIDAsync(memberContactId);
                if (memberContactFromDB == null)
                {
                    response.Message = ResponseMessageDisplay.NotFound;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(MemberMessageDisplay.MemberContactNotFoundMessage));
                }
                else
                {
                    await MemberContactRepository.DeleteMemberContactAsync(memberContactFromDB);

                    response.Message = ResponseMessageDisplay.Success;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion
    }
}
