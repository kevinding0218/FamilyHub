using AutoMapper;
using FamilyHub.Data.Member;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class IOptionService : ServiceFactory, IIOptionService
    {
        public IOptionService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public IOptionService(IMapper mapper, FamilyHubDbContext dbContext)
            : base(mapper, dbContext)
        {
        }

        public async Task<IIOptionResponse<IOptions>> IOptionMemberRelationshipAsync()
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
    }
}
