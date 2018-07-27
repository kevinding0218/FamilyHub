using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Contracts
{
    public interface IIOptionService
    {
        Task<IIOptionResponse<IOptions>> IOptionMemberRelationshipAsync();
    }
}
