﻿using FamilyHub.API;
using FamilyHub.ViewModel;
using FamilyHub.AuthService;
using FamilyHub.AuthService.Contracts;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ICommonService _commonService;
        private readonly IConfiguration _configuration;

        public TokenController(
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            ICommonService commonService,
            IOptions<JwtIssuerOptions> jwtOptions,
            IConfiguration configuration)
        {
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _commonService = commonService;
            _jwtOptions = jwtOptions.Value;
            _configuration = configuration;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody]vmRefreshTokenRequest refreshTokenRequest)
        {
            var refreshTokenResponse = new SingleResponse<vmRefreshTokenResponse>();

            try
            {
                #region Get Principal Info from Token
                var principal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenRequest.JwtToken,
                    Encoding.UTF8.GetBytes(_configuration["JwtIssuerOptions:ServerSigningPassword"]));
                var userEmail = principal.Identity.Name; //this is mapped to the Name claim by default
                #endregion

                #region Get Claims Info from token principal
                //var name = principal.Claims.Where(c => c.Type == ClaimTypes.Name)
                //   .Select(c => c.Value).SingleOrDefault();
                //var role = principal.Claims.Where(c => c.Type == ClaimTypes.Role)
                //   .Select(c => c.Value).SingleOrDefault();
                //var email = principal.Claims.Where(c => c.Type == ClaimTypes.Email)
                //   .Select(c => c.Value).SingleOrDefault();
                #endregion

                var existedUserResponse = await _commonService.GetUserAsync(userEmail);

                if (existedUserResponse.Model == null || existedUserResponse.Model.RefreshToken != refreshTokenRequest.RefreshToken)
                    throw new FamilyHubException(string.Format(CommonMessageDisplays.UserNotFoundExceptionMessage, userEmail));

                refreshTokenResponse.Model = await _tokenService.AssignRefreshTokenAsync(existedUserResponse.Model);
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
