﻿using AutoMapper;
using FamilyHub.API.HttpResponse;
using FamilyHub.ViewModel;
using FamilyHub.AuthService;
using FamilyHub.AuthService.Contracts;
using FamilyHub.Data.Common;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
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
    [Route("/api/auth")]
    public class AuthController
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly JwtIssuerOptions _jwtOptions;

        protected ICommonService _commonService;
        private readonly IConfiguration _configuration;

        public AuthController(
            IMapper mapper,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            ICommonService commonService,
            IOptions<JwtIssuerOptions> jwtOptions,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _commonService = commonService;
            _jwtOptions = jwtOptions.Value;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Signup([FromBody]vmRegisterUserRequest registerUser)
        {
            var validEmailResponse = await _commonService.GetUserAsync(registerUser.Email, checkIfExisted: true);

            if (validEmailResponse.Message.Equals(ResponseMessageDisplay.IsExisted))
                return validEmailResponse.ToHttpResponse();
            else
            {
                registerUser.Password = _passwordHasher.GenerateIdentityV3Hash(registerUser.Password);

                var addedUser = _mapper.Map<vmRegisterUserRequest, User>(registerUser);
                var registerUserResponse = await _commonService.RegisterNewUser(addedUser);

                return registerUserResponse.ToHttpResponse();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]vmLoginUserRequest loggingUser)
        {
            var loginResponse = new SingleResponse<vmLoginUserResponse>();

            try
            {
                var existedUserResponse = await _commonService.GetUserAsync(loggingUser.Email, withCredential: true);

                // if User does not exist or authentication fails
                if (existedUserResponse.Model == null || existedUserResponse.Model.UserID == 0 ||
                    !_passwordHasher.VerifyIdentityV3Hash(loggingUser.Password, existedUserResponse.Model.UserPasswords.First().Password))
                {
                    throw new FamilyHubException(string.Format(CommonMessageDisplays.FailedAuthenticationExceptionMessage));
                }
                else
                {
                    // otherwise assign token and refreshtoken
                    loginResponse.Model = await _tokenService.AssignTokenToLoginUserAsync(existedUserResponse.Model);
                    loginResponse.Message = ResponseMessageDisplay.Success;
                }
            }
            catch (Exception ex)
            {
                loginResponse.SetError(ex);
            }


            return loginResponse.ToHttpResponse();
        }
    }
}
