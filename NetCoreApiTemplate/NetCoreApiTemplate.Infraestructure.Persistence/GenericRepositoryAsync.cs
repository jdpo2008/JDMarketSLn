using Microsoft.EntityFrameworkCore;
using NetCoreApiTemplate.Application.Common.Interfaces;
using NetCoreApiTemplate.Application.Common.Linq;
using NetCoreApiTemplate.Domain.Common;
using NetCoreApiTemplate.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Infraestructure.Persistence
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseAuditableEntity 
    {
        #region Fields

        protected ApplicationDbContext Context;

        #endregion

        public GenericRepositoryAsync(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Public Methods

        public virtual async Task<T> GetByIdAsync(Guid id) 
            => await Context.Set<T>().FindAsync(id);
        

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => await Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            await Context.SaveChangesAsync();

            return entities;
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);

            await Context.SaveChangesAsync();
        }

        public Task RemoveAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() 
            => await Context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllFiltered(Expression<Func<T, bool>> predicate) 
            => await Context.Set<T>().Where(predicate).ToListAsync();

        public Task<int> CountAll() => Context.Set<T>().CountAsync();

        public Task<int> CountAllFiltered(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().CountAsync(predicate);

        public IQueryable<T> FindWithSpecificationPattern(ISpecification<T> specification = null)
            => SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable().AsNoTracking(), specification);

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
            => await Context
                .Set<T>()
                .Page(pageNumber, pageSize)
                .OrderBy(s => s.CreatedAt)
                .AsNoTracking()
                .ToListAsync();

        #endregion
    }
}
