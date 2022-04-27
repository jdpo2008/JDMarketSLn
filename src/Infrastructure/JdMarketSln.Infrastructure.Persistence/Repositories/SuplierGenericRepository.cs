using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Domain.Entities;
using JdMarketSln.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Infrastructure.Persistence.Repositories
{
    public class SuplierGenericRepository : GenericRepository<Suplier>, ISuplierGenericRepository
    {
        public SuplierGenericRepository(JDMarketDbContext context) : base(context)
        {

        }
    }
}
