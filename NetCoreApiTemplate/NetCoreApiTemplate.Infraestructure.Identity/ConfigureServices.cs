using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using JDMarketSLn.Application.Common.Interfaces;
using JDMarketSLn.Application.Common.Models;
using JDMarketSLn.Domain.Entities.Identity;
using JDMarketSLn.Domain.Settings;
using JDMarketSLn.Infraestructure.Identity.Contexts;
using JDMarketSLn.Infraestructure.Identity.Middlewares;
using JDMarketSLn.Infraestructure.Identity.Services;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddIdentityInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
      
        string ConnectionString = "";
        bool isDevelopment = env.IsDevelopment();

        if(isDevelopment)
        {
           ConnectionString = configuration.GetConnectionString("IdentityConnectionLocal") ?? string.Empty;
        }
        else
        {

        }

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseInMemoryDatabase("TestApplicationDb"));
        }
        else
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(ConnectionString,
                    builder => builder.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
        }

        services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddRoles<ApplicationRole>()
               //.AddErrorDescriber<CustomIdentityErrorDescriber>()
               .AddEntityFrameworkStores<IdentityContext>()
               //.AddDefaultUI()
               .AddDefaultTokenProviders()
               .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");

        //services.AddIdentityServer(options =>
        //    {
        //        options.Events.RaiseErrorEvents = true;
        //        options.Events.RaiseInformationEvents = true;
        //        options.Events.RaiseFailureEvents = true;
        //        options.Events.RaiseSuccessEvents = true;
        //    }).AddApiAuthorization<ApplicationUser, IdentityContext>();

        //services.AddIdentityServer(options =>
        //{
        //    options.Events.RaiseErrorEvents = true;
        //    options.Events.RaiseInformationEvents = true;
        //    options.Events.RaiseFailureEvents = true;
        //    options.Events.RaiseSuccessEvents = true;
        //}).AddApiAuthorization<ApplicationUser, IdentityContext>()
        //   .AddConfigurationStore(options =>
        //  {
        //      options.ConfigureDbContext = b =>
        //      b.UseSqlServer(ConnectionString, opt => opt.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
        //  })
        //  .AddOperationalStore(options =>
        //  {
        //      options.ConfigureDbContext = b =>
        //      b.UseSqlServer(ConnectionString, opt => opt.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
        //  })
        //  .AddDeveloperSigningCredential();

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddScoped<IdentityDbContextInitialiser>();
        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            // Sing settings.
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedAccount = true;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            // Tokens settings.
            options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
        });

        services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromDays(3)
        );

        services.AddAuthorization(options =>
        {
            options.AddPolicy("SuperAdminPolicy", (policy) =>
            {
                policy.RequireClaim(ClaimTypes.Role, "SuperAdmin");
            });

            options.AddPolicy("AdminPolicy", (policy) =>
            {
                policy.RequireClaim(ClaimTypes.Role, "Admin");
            });

            options.AddPolicy("BasicPolicy", (policy) =>
            {
                policy.RequireClaim(ClaimTypes.Role, "Basic");
            });
           
        });
      
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        var errors = new List<string>(); 
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "application/json";
                        errors.Add("Error interno en el servidor");
                        var result = JsonConvert.SerializeObject(Result.Failure(errors, StatusCodes.Status500InternalServerError));
                        return c.Response.WriteAsync(result);
                    },
                    OnChallenge = context =>
                    {
                        var errors = new List<string>();
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        errors.Add("You are not Authorized");
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(Result.Failure(errors, StatusCodes.Status401Unauthorized));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        var errors = new List<string>();
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        errors.Add("You are not authorized to access this resource");
                        var result = JsonConvert.SerializeObject(Result.Failure(errors, StatusCodes.Status403Forbidden));
                        return context.Response.WriteAsync(result);
                    },
                };
            });


        return services;
    }
}
