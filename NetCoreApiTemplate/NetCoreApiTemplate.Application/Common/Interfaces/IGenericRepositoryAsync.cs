using NetCoreApiTemplate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Common.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : BaseAuditableEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllFiltered(Expression<Func<T, bool>> predicate);
        Task<int> CountAll();
        Task<int> CountAllFiltered(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindWithSpecificationPattern(ISpecification<T> specification = null);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
    }
}
