using JdMarketSln.Application.Interfaces;
using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Infrastructure.Persistence.Context;
using JdMarketSln.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<JDMarketDbContext>(options =>
                    options.UseInMemoryDatabase("JDMarketApplicationtDb"));
            }
            else
            {
                services.AddDbContext<JDMarketDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConectionString"),
                   b => b.MigrationsAssembly(typeof(JDMarketDbContext).Assembly.FullName)));
            }

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IProductGenericRepository, ProductGenericRepository>();
            services.AddTransient<ISuplierGenericRepository, SuplierGenericRepository>();
            //services.AddTransient<ICategoryRepositoryAsync, CategoryRepositoryAsync>();
            #endregion
        }

    }
}
