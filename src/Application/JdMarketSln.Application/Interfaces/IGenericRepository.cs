using JdMarketSln.Application.Request;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> FindWithSpecificationPattern(ISpecification<T> specification = null);
        Task<IReadOnlyList<T>> GetAllPaginated(int pageNumber, int pageSize);
    }
}
