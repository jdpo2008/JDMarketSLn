using JdMarketSln.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDMarketSLn.Web.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var isDevelopment = services.GetRequiredService<IWebHostEnvironment>().IsDevelopment();

                using var appContext = services.GetRequiredService<JDMarketDbContext>();
                //using var identityC0otext = services.GetRequiredService<IdentityContext>();

                if (isDevelopment)
                {
                    //await identityC0otext.Database.EnsureCreatedAsync();
                    await appContext.Database.EnsureCreatedAsync();
                }
                else
                {
                    //await identityC0otext.Database.MigrateAsync();
                    await appContext.Database.MigrateAsync();
                }

            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
