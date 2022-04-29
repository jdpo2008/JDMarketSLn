using JdMarketSln.Application.Request;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Interfaces.Repositories
{
    public interface IProductGenericRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetAllIncludeAsync(int pageNumber, int pageSize);
        Task<List<Product>> GetProductsByIdSuplier(Guid idSuplier);
    }
}
