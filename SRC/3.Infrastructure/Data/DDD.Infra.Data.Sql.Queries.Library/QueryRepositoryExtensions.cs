using DDD.Core.RequestResponse.Library.Queries;
using DDD.Utilities.Library.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infra.Data.Sql.Queries.Library;

/// <summary>
/// 
/// </summary>
public static class QueryRepositoryExtensions
{
    /// <summary>
    /// پیجینگ را پیاده سازی میکند
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="entities"></param>
    /// <param name="query"></param>
    /// <param name="selectFunc"></param>
    /// <returns></returns>
    public static async Task<PagedData<TResult>> ToPagedData<TEntity, TQuery, TResult>(this IQueryable<TEntity> entities, PageQuery<PagedData<TQuery>> query, Func<TEntity, TResult> selectFunc)
    {
        var result = new PagedData<TResult>
        {
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
        if (query.NeedTotalCount)
            result.TotalCount = await entities.CountAsync();

        if (!string.IsNullOrWhiteSpace(query.SortBy))
            entities = entities.OrderByField(query.SortBy, query.SortAscending);
        entities = entities.Skip(query.SkipCount).Take(query.PageSize);

        result.QueryResult = await entities.Select(
               c => selectFunc(c)).ToListAsync();
        return result;
    }
}
