using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Domain.Entities;
using JdMarketSln.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Infrastructure.Persistence.Repositories
{
    public class SuplierGenericRepository : GenericRepository<Suplier>, ISuplierGenericRepository
    {
        private readonly DbSet<Suplier> _suplier;

        public SuplierGenericRepository(JDMarketDbContext context) : base(context)
        {
            _suplier = context.Set<Suplier>();
        }

        public Task<List<Suplier>> GetAllIncludeAsync(int pageNumber, int pageSize)
        {
            var query = _suplier.Include(p => p.Products).AsQueryable();

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

      
    }
}
