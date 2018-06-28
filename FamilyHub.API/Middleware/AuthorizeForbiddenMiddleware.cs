using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FamilyHub.API.Middleware
{
    public class AuthorizeForbiddenMiddleware
    {
        readonly RequestDelegate next;

        public AuthorizeForbiddenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Response.Headers.Add(AuthService.Helper.Constants.JwtTokenResult.TokenForbidden, "true");
                }
            }
        }
    }
}
