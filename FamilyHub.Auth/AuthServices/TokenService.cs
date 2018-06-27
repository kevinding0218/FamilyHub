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

namespace FamilyHub.AuthService.AuthServices
{
    public class TokenService : ITokenService
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JwtHeader _jwtHeader;

        public TokenService()
        {

        }

        public TokenService(IOptions<JwtIssuerOptions> jwtOptions)
        {
            //_jwtOptions = jwtOptions.Value;
            //ThrowIfInvalidOptions(_jwtOptions);
            //_jwtHeader = new JwtHeader(_jwtOptions.SigningCredentials);
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims, string Issuer, string Audience, byte[] ServerSigningPassword, int AccessTokenDurationInMinutes)
        {
            var key = new SymmetricSecurityKey(ServerSigningPassword);

            var jwtToken = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(AccessTokenDurationInMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

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
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }



        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, bool InternalUser = false)
        {
            // Creates a JwtSecurityToken with a combination of registered claims (from the jwt spec) Sub, Jti, Iat and two specific to our app: Rol and Id.
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64),
                 new Claim(JwtRegisteredClaimNames.Exp, ToUnixEpochDate(DateTime.Now.AddMinutes(3)).ToString(), ClaimValueTypes.Integer64),
                 //identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.MHUser),
                 identity.FindFirst(Helper.Constants.Strings.JwtClaimIdentifiers.Id),
             };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                //expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            jwt.Payload["issueAt"] = _jwtOptions.IssuedAt.ToString();
            jwt.Payload["expiredOn"] = _jwtOptions.Expiration.ToString();
            jwt.Payload["customIssueAt"] = DateTime.Now.ToString();
            jwt.Payload["customExpiredOn"] = DateTime.Now.AddMinutes(3).ToString();
            if (InternalUser)
                jwt.Payload[Helper.Constants.Strings.JwtClaimIdentifiers.InternalUser] = Helper.Constants.Strings.JwtClaims.ApiInternalAccess;
            else
                jwt.Payload[Helper.Constants.Strings.JwtClaimIdentifiers.Rol] = Helper.Constants.Strings.JwtClaims.ApiAccess;

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Helper.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helper.Constants.Strings.JwtClaimIdentifiers.Rol, Helper.Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        public ClaimsIdentity GenerateClaimsIdentityAdmin(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Helper.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helper.Constants.Strings.JwtClaimIdentifiers.InternalUser, Helper.Constants.Strings.JwtClaims.ApiInternalAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
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
    }
}
