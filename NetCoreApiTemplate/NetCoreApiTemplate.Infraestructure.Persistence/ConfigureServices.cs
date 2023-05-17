using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using JDMarketSLn.Application.Common.Interfaces;
using JDMarketSLn.Infraestructure.Persistence;
using JDMarketSLn.Infraestructure.Persistence.Contexts;
using JDMarketSLn.Infraestructure.Persistence.Interceptors;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        string ConnectionString = "";
        bool isDevelopment = env.IsDevelopment();

        if(isDevelopment)
        {
           ConnectionString = configuration.GetConnectionString("DefaultConnectionLocal") ?? string.Empty;
        }
        else
        {

        }

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestApplicationDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(ConnectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
