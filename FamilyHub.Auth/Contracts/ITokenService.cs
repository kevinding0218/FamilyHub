using FamilyHub.Data.Common;
using FamilyHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.AuthService.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(string Email, string UID, JwtIssuerOptions jwtOptions);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, byte[] ServerSigningPassword);

        Task<vmAuthorizedUserResponse> AssignTokenToLoginUserAsync(vmValidateUserResponse validateLoginUserResponse);
        Task<vmRefreshTokenResponse> AssignRefreshTokenAsync(User userFromDb);
    }
}
