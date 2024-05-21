using DDD.Core.RequestResponse.Library.Queries;
using DDD.Utilities.Library.Extensions;
using Microsoft.EntityFrameworkCore;
namespace DDD.Core.Data.Sql.Queries.Library;

public static class QueryRepositoryExtensions
{
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
