using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FamilyHub.API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FamilyHub.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public ValuesController()
        {

        }
        // GET api/values
        [HttpGet, Authorize(Policy = AuthService.Helper.Constants.JwtPolicys.RoleAdminRequired)]
        public IEnumerable<string> Get()
        {
            #region Test to get Identity Claims
            //var userEmail = User.Identity.Name;
            //var userClaims = User.Claims;
            //var name = userClaims.Where(c => c.Type == ClaimTypes.Name)
            //                   .Select(c => c.Value).SingleOrDefault();
            //var role = userClaims.Where(c => c.Type == ClaimTypes.Role)
            //       .Select(c => c.Value).SingleOrDefault();
            #endregion

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
