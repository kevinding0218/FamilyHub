using AutoMapper;
using FamilyHub.Data;
using FamilyHub.Data.Member;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Core;
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
        public async Task<IIOptionResponse<IOptions>> PrepareMemberRelationshipRequestAsync()
        {
            var response = new IOptionResponse<IOptions>();

            try
            {
                var listMemberRelationshipFromDb = await MemberRelationshipRepository.GetListMemberRelationshipAsync();
                response.Model = _mapper.Map<IEnumerable<MemberRelationship>, IEnumerable<IOptions>>(listMemberRelationshipFromDb);
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
        public async Task<IListResponse<vmMemberContactListResponse>> GetMemberContactListByCreatedAsync(int createdBy)
        {
            var response = new ListResponse<vmMemberContactListResponse>();

            try
            {
                if (createdBy == 0)
                {
                    var listMemberContactFromDb = await MemberContactRepository.GetListMemberContactAsync(createdBy);
                    response.Model = _mapper.Map<IEnumerable<MemberContact>, IEnumerable<vmMemberContactListResponse>>(listMemberContactFromDb);
                    response.Message = ResponseMessageDisplay.Success;
                }
                else
                {
                    response.DidError = true;
                    response.ErrorMessage = "createdBy must be greater than 0.";
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public Task<IListResponse<vmMemberContactListResponse>> GetMemberContactListByRelationshipAsync(int createdBy, int memberRelationshipID)
        {
            throw new NotImplementedException();
        }

        public async Task<ISingleResponse<vmMemberContactDetailResponse>> AddMemberContactAsync(vmMemberContactDetailRequest newMemberContactRequest)
        {
            var response = new SingleResponse<vmMemberContactDetailResponse>();

            try
            {
                var newMemberContact = _mapper.Map<vmMemberContactDetailRequest, MemberContact>(newMemberContactRequest);
                // Create new payment payor
                await MemberContactRepository.AddMemberContactAsync(newMemberContact);

                // Fetch complete object from database
                newMemberContact = await MemberContactRepository.GetSingleMemberContactByIDAsync(newMemberContact.MemberContactID);
                // Convert from Domain Model to View Model
                var newMemberContactResponse = _mapper.Map<MemberContact, vmMemberContactDetailResponse>(newMemberContact);

                response.Model = newMemberContactResponse;
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<ISingleResponse<vmMemberContactDetailResponse>> UpdateMemberContactAsync(int memberContactId, vmMemberContactDetailRequest updateMemberContactRequest)
        {
            var response = new SingleResponse<vmMemberContactDetailResponse>();

            try
            {
                var duplicateMemberContact = await MemberContactRepository.GetSingleMemberContactByNameAsync($"{updateMemberContactRequest.FirstName} {updateMemberContactRequest.LastName}");

                if (duplicateMemberContact != null && duplicateMemberContact.MemberContactID != memberContactId)
                {
                    response.Message = ResponseMessageDisplay.Duplicate;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(MemberMessageDisplay.MemberContactAlreadyExistedMessage, $"{updateMemberContactRequest.FirstName} {updateMemberContactRequest.LastName}"));
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
                        _mapper.Map<vmMemberContactDetailRequest, MemberContact>(updateMemberContactRequest, memberContactFromDB);
                        await MemberContactRepository.UpdateMemberContactAsync(memberContactFromDB);

                        // Fetch complete object from database
                        memberContactFromDB = await MemberContactRepository.GetSingleMemberContactByIDAsync(memberContactFromDB.MemberContactID);
                        // Convert from Domain Model to View Model
                        var newMemberContactResponse = _mapper.Map<MemberContact, vmMemberContactDetailResponse>(memberContactFromDB);

                        response.Model = newMemberContactResponse;
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
