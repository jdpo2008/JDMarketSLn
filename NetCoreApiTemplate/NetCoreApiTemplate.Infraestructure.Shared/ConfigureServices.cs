using Microsoft.Extensions.Configuration;
using JDMarketSLn.Application.Common.Interfaces;
using JDMarketSLn.Domain.Settings;
using JDMarketSLn.Infraestructure.Shared.Services;
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
