using JdMarketSln.Application.Interfaces;
using JdMarketSln.Application.Request;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Common;
using JdMarketSln.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Infrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        #region Fields

        protected readonly JDMarketDbContext _context;

        #endregion

        public GenericRepository(JDMarketDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {

            return await _context.Set<T>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> FindWithSpecificationPattern(ISpecification<T> specification = null)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable().AsNoTracking(), specification);
        }

        public async Task<IReadOnlyList<T>> GetAllPaginated(int pageNumber, int pageSize)
        {
            var query = _context.Set<T>().AsQueryable();

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(); 
        }
    }
}
