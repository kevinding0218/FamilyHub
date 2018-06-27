using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FamilyHub.AuthService;
using FamilyHub.AuthService.AuthServices;
using FamilyHub.AuthService.Contracts;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<FamilyHubDbContext>(options => options.UseSqlServer(Configuration["AppSettings:ConnectionString"]));
            services.AddAutoMapper();

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddScoped<ICommonService, CommonService>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var keyByteArray = Encoding.UTF8.GetBytes(Configuration["JwtIssuerOptions:ServerSigningPassword"]);
                var signingKey = new SymmetricSecurityKey(keyByteArray);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtIssuerOptions:ServerSigningPassword"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero //the default for this setting is 5 minutes
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // ********************
            // Setup CORS
            // ********************
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseAuthentication();

            app.UseStaticFiles();

            // ********************
            // USE CORS - might not be required.
            // ********************
            app.UseCors("SiteCorsPolicy");
        }

        public void ConfigureJwtAuthService(IServiceCollection services)
        {
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var keyByteArray = Encoding.UTF8.GetBytes(Configuration["JwtIssuerOptions:ServerSigningPassword"]);
            var _signingKey = new SymmetricSecurityKey(keyByteArray);

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                // Validate the JWT Issuer (iss) claim  
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                // Validate the JWT Audience (aud) claim  
                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                // The signing key must match! 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                // Validate the token expiry  
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            // Add Custom Authentication Middleware Service
            // introduced JWT authentication to the request pipeline, 
            // specified the validation parameters to dictate how we want received tokens validated and finally, 
            // created an authorization policy to guard our API controllers and actions which we'll apply
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            // use a claims-based authorization check to give the role access to certain controllers and actions so that only users possessing the role claim may access those resources.
            // build and register a policy called ApiUser which checks for the presence of the Rol claim with a value of ApiAccess.
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ApiUser", policy => policy.RequireClaim(
            //        AuthService.Helper.Constants.Strings.JwtClaimIdentifiers.Rol,
            //        AuthService.Helper.Constants.Strings.JwtClaims.ApiAccess));
            //    options.AddPolicy("InternalOnly", policy => policy.RequireClaim(
            //        AuthService.Helper.Constants.Strings.JwtClaimIdentifiers.InternalUser,
            //        AuthService.Helper.Constants.Strings.JwtClaims.ApiInternalAccess));
            //});
        }
    }
}
