using JdMarketSln.Application.Interfaces;
using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Application.Request;
using JdMarketSln.Application.Wrappers;
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
    public class ProductGenericRepository : GenericRepository<Product>, IProductGenericRepository
    {
        private readonly DbSet<Product> _product;

        public ProductGenericRepository(JDMarketDbContext context) : base(context)
        {
            _product = context.Set<Product>();
        }

        public Task<List<Product>> GetAllIncludeAsync(int pageNumber, int pageSize)
        {
            var query = _product.Include(c => c.Category).Include(s => s.Suplier).AsQueryable();

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public Task<List<Product>> GetProductsByIdSuplier(Guid idSuplier)
        {
            return _product.Where(p => p.SuplierId == idSuplier).ToListAsync();
        }
    }
}
