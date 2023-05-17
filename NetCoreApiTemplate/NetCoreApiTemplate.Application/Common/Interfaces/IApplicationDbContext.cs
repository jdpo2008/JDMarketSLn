//using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using JDMarketSLn.Domain.Entities;

namespace JDMarketSLn.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    //DbSet<TodoList> TodoLists { get; }

    DbSet<Log> Logs { get; }
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    DbSet<SubCategory> SubCategories { get; }
    DbSet<ProductDetail> ProductDetails { get; }
    DbSet<UnitMeasureProduct> UnitMeasureProducts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
