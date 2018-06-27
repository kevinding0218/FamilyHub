using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.AuthService.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims, string Issuer, string Audience, byte[] ServerSigningPassword, int AccessTokenDurationInMinutes);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, byte[] ServerSigningPassword);

        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, bool InternalUser = false);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
        ClaimsIdentity GenerateClaimsIdentityAdmin(string userName, string id);
    }
}
