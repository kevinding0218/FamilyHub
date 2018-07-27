using FamilyHub.Service.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.API.Controllers.Shared
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/iOption")]
    public class IOptionController : Controller
    {
        private readonly IIOptionService _iOptionService;

        public IOptionController(
            IIOptionService iOptionService)
        {
            _iOptionService = iOptionService;
        }

        // GET: api/iOption/memberRelationship
        [HttpGet("memberRelationship")]
        public async Task<IActionResult> IOptionMemberRelationship()
        {
            // Get response from business logic
            var response = await _iOptionService.IOptionMemberRelationshipAsync();

            // Return as http response
            return response.ToHttpResponse();
        }
    }
}
