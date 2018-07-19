using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyHub.Data;
using FamilyHub.Service.Contracts;
using FamilyHub.ViewModel.Member;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FamilyHub.API.Controllers.Member
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/member")]
    public class MemberController : Controller
    {
        private readonly IUserInfo _userInfo;
        private readonly IMemberService _memberService;

        public MemberController(
            IUserInfo userInfo,
            IMemberService memberService)
        {
            _userInfo = userInfo;
            _memberService = memberService;
        }

        #region Member Relationship
        // GET: api/member/memberRelationship
        [HttpGet("memberRelationship")]
        public async Task<IActionResult> ListMemberRelationshipAsync()
        {
            // Get response from business logic
            var response = await _memberService.PrepareMemberRelationshipRequestAsync();

            // Return as http response
            return response.ToHttpResponse();
        }
        #endregion

        #region Member Contact
        // GET: api/member/memberContactByCreated/5
        [HttpGet("memberContactByCreated/{uid}")]
        public async Task<IActionResult> ListMemberContactCreatedAsync(int uid)
        {
            // Get response from business logic
            var response = await _memberService.GetMemberContactListByCreatedAsync(uid);

            // Return as http response
            return response.ToHttpResponse();
        }

        // GET: api/member/memberContactByCreated/5/3
        [HttpGet("memberContactByRelationship/{uid}/{relationshipID}")]
        public async Task<IActionResult> ListMemberContactByRelationshipAsync(int uid, int relationshipID)
        {
            // Get response from business logic
            var response = await _memberService.GetMemberContactListByRelationshipAsync(uid, relationshipID);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPost("createMemberContact")]
        public async Task<IActionResult> CreateMemberContactAsync([FromBody] vmMemberContactCreateRequest newMemberContactRequest)
        {
            // Get response from business logic
            var response = await _memberService.AddMemberContactAsync(newMemberContactRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPut("updateMemberContact")]
        public async Task<IActionResult> UpdateMemberContactAsync(int memberContactId, [FromBody] vmMemberContactUpdateRequest updateMemberContactRequest)
        {
            // Get response from business logic
            var response = await _memberService.UpdateMemberContactAsync(memberContactId, updateMemberContactRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpDelete("deleteMemberContact")]
        public async Task<IActionResult> DeleteMemberContactAsync(int memberContactId)
        {
            // Get response from business logic
            var response = await _memberService.DeleteMemberContactAsync(memberContactId);

            // Return as http response
            return response.ToHttpResponse();
        }
        #endregion        
    }
}
