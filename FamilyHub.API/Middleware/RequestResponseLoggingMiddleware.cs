using FamilyHub.AuthService.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
            ITokenService tokenService,
            IConfiguration configuration)
        {
            _next = next;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = await FormatRequest(context.Request);

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                var response = await FormatResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            var body = request.Body;

            #region Get Token from HttpRequest Header
            request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authToken);
            if (authToken.Count > 0 && authToken[0].StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                var token = authToken[0].Substring("Bearer ".Length).Trim();

                #region Get Principal Info from Token
                var principal = _tokenService.GetPrincipalFromExpiredToken(token,
                    Encoding.UTF8.GetBytes(_configuration["JwtIssuerOptions:ServerSigningPassword"]));
                var userEmail = principal.Identity.Name; //this is mapped to the Name claim by default
                #endregion

                #region Extract Claims Info from token principal
                //var name = principal.Claims.Where(c => c.Type == ClaimTypes.Name)
                //   .Select(c => c.Value).SingleOrDefault();
                //var role = principal.Claims.Where(c => c.Type == ClaimTypes.Role)
                //   .Select(c => c.Value).SingleOrDefault();
                //var email = principal.Claims.Where(c => c.Type == ClaimTypes.Email)
                //   .Select(c => c.Value).SingleOrDefault();
                #endregion
            }
            #endregion

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(Microsoft.AspNetCore.Http.HttpResponse response)
        {
            var text = string.Empty;
            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                text = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
            }
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                text = "Not Authorized";
            else if (response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                if (response.HttpContext.User.Identity.IsAuthenticated)
                    text = "Authenticated but Forbidden";
                else
                    text = "Not Authorized";
            }

            return $"Response {text}";
        }
    }

    #region Register Middleware - replaced with directly calling from Configure in Startup.cs
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
    #endregion
}
