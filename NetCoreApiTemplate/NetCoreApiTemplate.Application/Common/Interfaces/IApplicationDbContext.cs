//using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NetCoreApiTemplate.Domain.Entities;

namespace NetCoreApiTemplate.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    //DbSet<TodoList> TodoLists { get; }

    DbSet<Log> Logs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
