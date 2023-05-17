using System;
using System.Linq;
using System.Linq.Expressions;


namespace JDMarketSLn.Application.Common.Linq
{
    public static class LinqExtension
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {

            if (condition)
                return source.Where(predicate);
            else
                return source;

        }
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int? page, int? size)
        {
            if (page != null && size != null)
                return source.Skip((int)((page) * size)).Take((int)size);
            return source;

        }
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int? page, int? size, ref int count)
        {
            count = source.Count();
            return source.Page(page, size);

        }
    }
}
