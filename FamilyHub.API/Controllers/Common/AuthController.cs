using AutoMapper;
using FamilyHub.API.HttpResponse;
using FamilyHub.API.ViewModel;
using FamilyHub.AuthService.Contracts;
using FamilyHub.Data.Common;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
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
    [Route("/api/auth")]
    public class AuthController
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        protected ICommonService _commonService;
        private readonly IConfiguration _configuration;

        public AuthController(
            IMapper mapper,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            ICommonService commonService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _commonService = commonService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Signup([FromBody]vmRegisterUser registerUser)
        {
            var validEmailResponse = await _commonService.GetUserAsync(registerUser.Email, checkIfExisted: true);

            if (validEmailResponse.Message.Equals(ResponseMessageDisplay.IsExisted))
                return validEmailResponse.ToHttpResponse();
            else
            {
                registerUser.Password = _passwordHasher.GenerateIdentityV3Hash(registerUser.Password);

                var addedUser = _mapper.Map<vmRegisterUser, User>(registerUser);
                var registerUserResponse = await _commonService.RegisterNewUser(addedUser);

                return registerUserResponse.ToHttpResponse();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]vmLoginUser loggingUser)
        {
            var loginResponse = new SingleResponse<vmLoginUserResponse>();

            try
            {
                var internalUserResponse = await _commonService.GetUserAsync(loggingUser.Email, withCredential: true);

                // if User does not exist or authentication fails
                if (internalUserResponse.Model == null || internalUserResponse.Model.UserID == 0 ||
                    !_passwordHasher.VerifyIdentityV3Hash(loggingUser.Password, internalUserResponse.Model.UserPasswords.First().Password))
                {
                    throw new FamilyHubException(string.Format(CommonMessageDisplays.FailedAuthenticationExceptionMessage));
                }
                else
                {
                    // otherwise assign token and refreshtoken
                    var userFromDb = internalUserResponse.Model;
                    var usersClaims = new[]
                    {
                        new Claim(ClaimTypes.Name, userFromDb.Email),
                        //new Claim(ClaimTypes.Email, userFromDb.Email),
                        //new Claim(ClaimTypes.Role, "ADMIN"),
                        new Claim(ClaimTypes.NameIdentifier, userFromDb.UserID.ToString())
                    };

                    var jwtToken = _tokenService.GenerateAccessToken(
                        usersClaims,
                        _configuration["JwtIssuerOptions:Issuer"].ToString(),
                        _configuration["JwtIssuerOptions:Audience"].ToString(),
                        Encoding.UTF8.GetBytes(_configuration["JwtIssuerOptions:ServerSigningPassword"]),
                        int.Parse(_configuration["JwtIssuerOptions:AccessTokenDurationInMinutes"]));

                    var refreshToken = _tokenService.GenerateRefreshToken();

                    userFromDb.RefreshToken = refreshToken;

                    await _commonService.UpdateUserRefreshTokenAsync(userFromDb);

                    loginResponse.Model = new vmLoginUserResponse(userFromDb.UserID, userFromDb.Email, jwtToken, refreshToken);
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
