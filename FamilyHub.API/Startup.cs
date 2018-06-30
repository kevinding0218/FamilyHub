using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FamilyHub.API.Middleware;
using FamilyHub.ViewModel;
using FamilyHub.AuthService;
using FamilyHub.AuthService.AuthServices;
using FamilyHub.AuthService.Contracts;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FamilyHub.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region MVC
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            #endregion

            #region Inject EF DbContext
            services.AddDbContext<FamilyHubDbContext>(options => options.UseSqlServer(Configuration["AppSettings:ConnectionString"]));
            #endregion

            #region Inject AutoMapper
            services.AddAutoMapper();
            #endregion

            #region Inject Business Service Layer
            services.AddScoped<ICommonService, CommonService>();
            #endregion

            #region Inject Auth Service Layer
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            #endregion



            #region Configure JwtIssuerOptions
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtIssuerOptions:ServerSigningPassword"]));
            #endregion

            #region Configure Jwt Options
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
                options.ValidFor = TimeSpan.FromMinutes(Convert.ToInt32(jwtAppSettingOptions[nameof(JwtIssuerOptions.ValidFor)]));
            });
            #endregion

            #region Configure Jwt Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                #region Jwt Validation Rule
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                    ValidateAudience = true,
                    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                    // The signing key must match! 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["JwtIssuerOptions:ServerSigningPassword"])),

                    // Validate the token expiry  
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero //the default for this setting is 5 minutes
                };
                #endregion

                options.Events = new JwtBearerEvents
                {
                    #region Jwt After Validation Authenticated
                    //OnTokenValidated = async context =>
                    //{
                    //    #region Get user's immutable object id from claims that came from ClaimsPrincipal
                    //    //var userEmail = context.Principal.Claims.Where(c => c.Type == ClaimTypes.Name)
                    //    //       .Select(c => c.Value).SingleOrDefault();
                    //    //var role = context.Principal.Claims.Where(c => c.Type == ClaimTypes.Role)
                    //    //       .Select(c => c.Value).SingleOrDefault();
                    //    #endregion

                    //    #region Use Service
                    //    //var _commonService = context.HttpContext.RequestServices.GetRequiredService<ICommonService>();
                    //    //var internalUserResponse = await _commonService.GetUserAsync(userEmail);
                    //    #endregion
                    //},
                    #endregion

                    #region Jwt After Validation Failed
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add(AuthService.Helper.Constants.JwtTokenResult.TokenExpired, "true");
                        }
                        else if (context.Exception.GetType() == typeof(SecurityTokenInvalidSignatureException)
                          || context.Exception.GetType() == typeof(SecurityTokenInvalidSigningKeyException)
                          || context.Exception.GetType() == typeof(SecurityTokenInvalidIssuerException)
                          || context.Exception.GetType() == typeof(SecurityTokenInvalidAudienceException)
                        )
                        {
                            context.Response.Headers.Add(AuthService.Helper.Constants.JwtTokenResult.TokenInvalid, "true");
                        }
                        return Task.CompletedTask;
                    }
                    #endregion
                };
            });
            #endregion

            #region Configure Jwt Authorization
            services.AddAuthorization(options =>
            {
                #region Policy-based Authorization
                options.AddPolicy(AuthService.Helper.Constants.JwtPolicys.RoleAdminRequired,
                    policy => policy.RequireRole("ADMIN"));
                #endregion

                #region Claim-based Authorization
                // use a claims-based authorization check to give the role access to certain controllers and actions 
                // so that only users possessing the role claim may access those resources.
                // build and register a policy called ApiUser which checks for the presence of the Rol claim with a value of ApiAccess.
                //    options.AddPolicy("ApiUser", policy => policy.RequireClaim(
                //        AuthService.Helper.Constants.Strings.JwtClaimIdentifiers.Rol,
                //        AuthService.Helper.Constants.Strings.JwtClaims.ApiAccess));
                //    options.AddPolicy("InternalOnly", policy => policy.RequireClaim(
                //        AuthService.Helper.Constants.Strings.JwtClaimIdentifiers.InternalUser,
                //        AuthService.Helper.Constants.Strings.JwtClaims.ApiInternalAccess));
                #endregion
            });
            #endregion


            #region Setup CORS
            var corsBuilder = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.WithOrigins("http://localhost:4200"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Exception Handler
            /*app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    //when authorization has failed, should retrun a json message to client
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Unauthorized",
                            Msg = "token expired"
                        }));
                    }
                    //when orther error, retrun a error message json to client
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Internal Server Error",
                            Msg = error.Error.Message
                        }));
                    }
                    //when no error, do next.
                    else await next();
                });
            });*/
            #endregion

            #region Install Middleware
            app.UseMiddleware<AuthorizeForbiddenMiddleware>();
            //app.UseRequestResponseLogging();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            #endregion

            app.UseStaticFiles();

            #region Install Authentication
            app.UseAuthentication();
            #endregion

            app.UseMvc();

            #region Install CORS
            app.UseCors("SiteCorsPolicy");
            #endregion
        }
    }
}
