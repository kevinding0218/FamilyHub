using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyHub.Data;
using FamilyHub.Service.Contracts;
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
        #endregion

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
