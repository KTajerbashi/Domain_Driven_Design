using BaseSource.Kernel.Utilities.Extensions;

namespace BaseSource.Core.Application.Providers.DapperQuery;

public interface IQueryExecute : IScopedLifetime
{
    #region PagedData
    //Task<PagedData<TModel>> ReadPagingAsync<TEntity, TModel>(FilterObject @filterObject)
    //    where TModel : QueryModel
    //    where TEntity : class
    //    ;
    //Task<PagedData<TModel>> ReadPagingSpAsync<TModel>(string spName, object parameters) where TModel : QueryModel;
    //Task<PagedData<TModel>> ReadPagingSpAsync<TModel>(string spName, FilterObject @filterObject) where TModel : QueryModel;
    //Task<PagedData<TModel>> ReadPagingListAsync<TModel>(string query, object parameters) where TModel : QueryModel;
    #endregion

    #region ReadSingle
    //Task<TModel> ReadSingleAsync<TEntity, TModel>(FilterObject @filterObject)
    //    where TModel : QueryModel
    //    where TEntity : class
    //    ;
    //Task<TModel> ReadSingleAsync<TModel>(string query, FilterObject @filterObject) where TModel : QueryModel;
    Task<TModel> ReadSingleBySpAsync<TModel>(string spName, object parameters) where TModel : QueryModel;
    Task<TModel> ReadSingleAsync<TModel>(string query, object parameters) where TModel : QueryModel;
    Task<TModel> ReadSingleDynamicAsync<TModel>(string query, object parameters);
    #endregion

    #region ReadFirstOrDefault
    //Task<TModel> ReadFirstOrDefaultAsync<TEntity, TModel>(FilterObject @filterObject)
    //    where TModel : QueryModel
    //    where TEntity : class
    //    ;
    //Task<TModel> ReadFirstOrDefaultAsync<TModel>(string query, FilterObject @filterObject) where TModel : QueryModel;
    Task<TModel> ReadFirstOrDefaultBySpAsync<TModel>(string spName, object parameters) where TModel : QueryModel;
    Task<TModel> ReadFirstOrDefaultAsync<TModel>(string query, object parameters) where TModel : QueryModel;
    Task<TModel> ReadFirstOrDefaultDynamicAsync<TModel>(string query, object parameters);
    #endregion

    #region ReadQuery
    //Task<IEnumerable<TModel>> ReadQueryAsync<TEntity, TModel>(FilterObject @filterObject)
    //    where TModel : QueryModel
    //    where TEntity : class
    //    ;
    //Task<IEnumerable<TModel>> ReadQueryAsync<TModel>(string query, FilterObject @filterObject) where TModel : QueryModel;
    Task<IEnumerable<TModel>> ReadQueryBySpAsync<TModel>(string spName, object parameters) where TModel : QueryModel;
    Task<IEnumerable<TModel>> ReadQueryAsync<TModel>(string query, object parameters) where TModel : QueryModel;
    Task<IEnumerable<TModel>> ReadQueryDynamicAsync<TModel>(string query, object parameters);

    #endregion

    #region Execute
    Task<TModel> ExecuteScalarAsync<TModel>(string query, object parameters);
    Task<int> ExecuteAsync(string query, object parameters);
    #endregion

    #region ReadDropDown
    //Task<IEnumerable<DropDownList<TKey>>> ReadDropDownAsync<TKey>(string query, object? parameters)
    //   where TKey : struct, IComparable, IComparable<TKey>, IConvertible, IEquatable<TKey>, IFormattable;
    //Task<IEnumerable<TModel>> ReadDropDownQueryAsync<TModel>(string query, object? parameters)
    //    where TModel : QueryModel, IDropDownList;

    #endregion

    #region ReadList
    //Task<ReadList<TModel>> ReadListAsync<TEntity, TModel>(FilterObject @object)
    //    where TEntity : class
    //    where TModel : QueryModel
    //    ;
    #endregion

    #region TreeList
    Task<IEnumerable<TModel>> ReadTreeListAsync<TEntity, TModel>(string query)
        where TModel : class, ITreeViewModel<TModel>
        where TEntity : class
        ;
    #endregion
}
