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
        public ProductGenericRepository(JDMarketDbContext context) : base(context)
        {
        }
      
    }
}
