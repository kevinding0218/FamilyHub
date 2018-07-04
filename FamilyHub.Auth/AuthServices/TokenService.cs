using FamilyHub.AuthService.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Linq;
using FamilyHub.Data.Common;
using FamilyHub.Service.Contracts;
using FamilyHub.ViewModel;

namespace FamilyHub.AuthService.AuthServices
{
    public class TokenService : ITokenService
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ICommonService _commonService;

        public TokenService(IOptions<JwtIssuerOptions> jwtOptions, ICommonService commonService)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _commonService = commonService;
        }

        protected static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        public string GenerateAccessToken(string Email, string UID, JwtIssuerOptions jwtOptions)
        {
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.NameIdentifier, UID),
                //new Claim(ClaimTypes.Email, userFromDb.Email),
                //new Claim(ClaimTypes.Role, "ADMIN"),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: userClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ValidFor.TotalMinutes),
                signingCredentials: _jwtOptions.SigningCredentials
            );

            #region Create the JWT security token and encode it.
            //jwtToken.Payload["issueAt"] = _jwtOptions.IssuedAt.ToString();
            //jwtToken.Payload["expiredOn"] = _jwtOptions.Expiration.ToString();
            //jwtToken.Payload["customIssueAt"] = DateTime.Now.ToString();
            //jwtToken.Payload["customExpiredOn"] = DateTime.Now.AddMinutes(3).ToString();

            //if (true)
            //    jwtToken.Payload[Helper.Constants.JwtClaimIdentifiers.InternalUser] = Helper.Constants.JwtClaims.ApiInternalAccess;
            //else
            //    jwtToken.Payload[Helper.Constants.JwtClaimIdentifiers.Rol] = Helper.Constants.JwtClaims.ApiAccess;
            #endregion

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, byte[] ServerSigningPassword)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(ServerSigningPassword),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            #region Get the claims values
            //var name = principal.Claims.Where(c => c.Type == ClaimTypes.Name)
            //                   .Select(c => c.Value).SingleOrDefault();
            //var sid = principal.Claims.Where(c => c.Type == ClaimTypes.Sid)
            //                   .Select(c => c.Value).SingleOrDefault();
            #endregion

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public async Task<vmAuthorizedUserResponse> AssignTokenToLoginUserAsync(vmValidateLoginUserResponse validateLoginUserResponse)
        {
            var jwtToken = GenerateAccessToken(
                validateLoginUserResponse.Email,
                validateLoginUserResponse.UserID.ToString(),
                _jwtOptions);

            var refreshToken = GenerateRefreshToken();

            var userFromDb = await _commonService.GetSingleUserForUpdateAsync(validateLoginUserResponse.Email);

            userFromDb.Model.RefreshToken = refreshToken;

            await _commonService.UpdateUserRefreshTokenAsync(userFromDb.Model);

            return new vmAuthorizedUserResponse() { UserID = validateLoginUserResponse.UserID, Email = validateLoginUserResponse.Email, JwtToken = jwtToken, RefreshToken = refreshToken };
        }

        public async Task<vmRefreshTokenResponse> AssignRefreshTokenAsync(User userFromDb)
        {
            var jwtToken = GenerateAccessToken(
                userFromDb.Email,
                userFromDb.UserID.ToString(),
                _jwtOptions);

            var refreshToken = GenerateRefreshToken();

            userFromDb.RefreshToken = refreshToken;

            await _commonService.UpdateUserRefreshTokenAsync(userFromDb);

            return new vmRefreshTokenResponse(jwtToken, refreshToken);
        }
    }
}
