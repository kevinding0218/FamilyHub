using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FamilyHub.AuthService.Contracts;
using Newtonsoft.Json;

namespace FamilyHub.AuthService.Helper
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, ITokenService tokenService, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await tokenService.GenerateEncodedToken(userName, identity, userName.Contains("ran.ding")),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
