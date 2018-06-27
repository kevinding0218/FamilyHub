using FamilyHub.API.HttpResponse;
using FamilyHub.API.ViewModel;
using FamilyHub.AuthService.Contracts;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.API.Controllers.Common
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/token")]
    public class TokenController : Controller
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly ICommonService _commonService;
        private readonly IConfiguration _configuration;

        public TokenController(
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            ICommonService commonService,
            IConfiguration configuration)
        {
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _commonService = commonService;
            _configuration = configuration;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody]vmRefreshTokenRequest refreshTokenRequest)
        {
            var refreshTokenResponse = new SingleResponse<vmRefreshTokenResponse>();

            try
            {
                var principal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenRequest.Token,
                    Encoding.UTF8.GetBytes(_configuration["JwtIssuerOptions:ServerSigningPassword"]));
                var userEmail = principal.Identity.Name; //this is mapped to the Name claim by default

                //var name = principal.Claims.Where(c => c.Type == ClaimTypes.Name)
                //   .Select(c => c.Value).SingleOrDefault();
                //var role = principal.Claims.Where(c => c.Type == ClaimTypes.Role)
                //   .Select(c => c.Value).SingleOrDefault();
                //var email = principal.Claims.Where(c => c.Type == ClaimTypes.Email)
                //   .Select(c => c.Value).SingleOrDefault();

                var internalUserResponse = await _commonService.GetUserAsync(userEmail);

                if (internalUserResponse.Model == null || internalUserResponse.Model.RefreshToken != refreshTokenRequest.RefreshToken)
                    throw new FamilyHubException(string.Format(CommonMessageDisplays.UserNotFoundExceptionMessage, userEmail));

                var userFromDb = internalUserResponse.Model;

                var newJwtToken = _tokenService.GenerateAccessToken(
                    principal.Claims,
                    _configuration["JwtIssuerOptions:Issuer"].ToString(),
                    _configuration["JwtIssuerOptions:Audience"].ToString(),
                    Encoding.UTF8.GetBytes(_configuration["JwtIssuerOptions:ServerSigningPassword"]),
                    int.Parse(_configuration["JwtIssuerOptions:AccessTokenDurationInMinutes"]));
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                userFromDb.RefreshToken = newRefreshToken;
                await _commonService.UpdateUserRefreshTokenAsync(userFromDb);

                refreshTokenResponse.Model = new vmRefreshTokenResponse(newJwtToken, newRefreshToken);
                refreshTokenResponse.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                refreshTokenResponse.SetError(ex);
            }

            return refreshTokenResponse.ToHttpResponse();
        }

        [HttpPost("revoke"), Authorize]
        public async Task<IActionResult> Revoke()
        {
            var userEmail = User.Identity.Name;

            var internalUserResponse = await _commonService.GetUserAsync(userEmail);

            if (internalUserResponse == null) return BadRequest();

            var userFromDb = internalUserResponse.Model;
            userFromDb.RefreshToken = null;

            await _commonService.UpdateUserRefreshTokenAsync(userFromDb);

            return NoContent();
        }

    }
}
