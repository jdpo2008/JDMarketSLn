using Microsoft.EntityFrameworkCore;

namespace JDMarketSLn.Application.Common.Models;

public class PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public Pagination Pagination { get; set; }

    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        Pagination = new Pagination(count, pageNumber, pageSize);
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}

public class Pagination
{
    public int nPageNumber { get; }
    public int nPageSize { get; set; }
    public int nTotalPages { get; }
    public int nTotalCount { get; }

    public bool HasPreviousPage => nPageNumber > 1;

    public bool HasNextPage => nPageNumber < nTotalPages;

    public Pagination(int count, int pageNumber, int pageSize)
    {
        nPageNumber = pageNumber;
        nTotalPages = (int)Math.Ceiling(count / (double)pageSize);
        nTotalCount = count;
        nPageSize = pageSize;
    }
}
