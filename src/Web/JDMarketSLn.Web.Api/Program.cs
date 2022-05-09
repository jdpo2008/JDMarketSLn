using JdMarketSln.Domain.Entities;
using JdMarketSln.Infrastructure.Identity.Contexts;
using JdMarketSln.Infrastructure.Identity.Seeds;
using JdMarketSln.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
                using var identityContext = services.GetRequiredService<JDMarketIdentityDbContext>();

                if (isDevelopment)
                {
                    await identityContext.Database.EnsureCreatedAsync();
                    await appContext.Database.EnsureCreatedAsync();

                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();

                    await DefaultRoles.SeedAsync(userManager, roleManager);
                    await DefaultSuperAdmin.SeedAsync(userManager, roleManager);
                    //await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);

                }
                else
                {
                    await identityContext.Database.MigrateAsync();
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
