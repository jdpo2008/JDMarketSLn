using Microsoft.Extensions.Configuration;
using NetCoreApiTemplate.Application.Common.Interfaces;
using NetCoreApiTemplate.Domain.Settings;
using NetCoreApiTemplate.Infraestructure.Shared.Services;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddSharedInfrastructureServices(this IServiceCollection services, IConfiguration _config)
    {
        services.Configure<MailSettings>(_config.GetSection("MailSettings"));
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
