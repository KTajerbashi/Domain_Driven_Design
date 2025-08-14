using BaseSource.Kernel.Utilities.Extensions;

namespace BaseSource.Core.Application.Providers.DapperQuery;

public class QueryExecute : IQueryExecute, IDisposable
{
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;
    private bool _disposed = false;
    public QueryExecute(IConfiguration configuration, IDbConnection connection, IJsonSerializer jsonSerializer)
    {
        _configuration = configuration;
        _connection = connection;
        InitializeConnection();
        _jsonSerializer = jsonSerializer;
    }
    private void InitializeConnection()
    {
        _connection.ConnectionString = _configuration.GetConnectionString("QueryConnection");
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Closed)
                    {
                        _connection.Close();
                    }
                    _connection.Dispose();
                }
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #region ReadPaging
    //public async Task<PagedData<TModel>> ReadPagingAsync<TEntity, TModel>(FilterObject filterObject)
    //    where TEntity : class
    //    where TModel : QueryModel
    //{
    //    var tableInfo = AttributeHelper.GetTableAttributeInfo(typeof(TEntity));


    //    filterObject.ColumnSort ??= "Id";
    //    filterObject.DirectionSort ??= "ASC";

    //    string isActiveCondition = filterObject.IgnoreIsActive ? string.Empty : "AND IsActive = @IsActive";
    //    string innerWhereCondition = filterObject.IsClient ? string.Empty : filterObject.InnerWhere;

    //    // Deserialize SearchValue JSON into a List of dynamic objects
    //    var searchConditions = new List<string>();
    //    var parameters = new DynamicParameters();
    //    parameters.Add("IsActive", 1);
    //    parameters.Add("Skip", filterObject.Skip);
    //    parameters.Add("Take", filterObject.Take);
    //    if (filterObject.Parameters is not null)
    //        parameters.AddDynamicParams(filterObject.Parameters);
    //    if (!string.IsNullOrWhiteSpace(filterObject.SearchValue))
    //    {
    //        var searchFilters = _jsonSerializer.Deserialize<IEnumerable<ColumnValueFilter>>(filterObject.SearchValue);
    //        int index = 0;
    //        foreach (var filter in searchFilters!)
    //        {
    //            string paramName = $"@SearchValue{index}";
    //            searchConditions.Add($"{filter.ColumnName} LIKE {paramName}");
    //            parameters.Add(paramName, $"%{filter.Value}%"); // Using LIKE for partial search
    //            index++;
    //        }
    //    }
    //    else
    //    {

    //    }

    //    string searchValueCondition = searchConditions.Any() ? "AND (" + string.Join(" OR ", searchConditions) + ")" : string.Empty;

    //    string query = $@"
    //SELECT *
    //FROM [{tableInfo.Schema}].[{tableInfo.Name}]
    //WHERE IsDeleted = 0
    //{isActiveCondition}
    //{innerWhereCondition}
    //{searchValueCondition}
    //ORDER BY {filterObject.ColumnSort} {filterObject.DirectionSort}
    //OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

    //    var countQ = $@"
    //                 SELECT COUNT(Id)
    //                    FROM [{tableInfo.Schema}].[{tableInfo.Name}]
    //                    WHERE IsDeleted = 0
    //                    {isActiveCondition}
    //                    {innerWhereCondition}
    //                    {searchValueCondition}";

    //    var data = await _connection.QueryAsync<TModel>(query, parameters, commandType: CommandType.Text);
    //    var count = await _connection.QueryAsync<int>(countQ, parameters, commandType: CommandType.Text);

    //    return new PagedData<TModel>
    //    {
    //        PageNumber = filterObject.PageNumber,
    //        Take = filterObject.Take,
    //        QueryResult = data.ToList(),
    //        TotalCount = count.FirstOrDefault()
    //    };
    //}
    //public async Task<PagedData<TModel>> ReadPagingListAsync<TModel>(string query, object parameters) where TModel : QueryModel
    //{
    //    var data = await _connection.QueryAsync<TModel>(query, parameters, commandType: CommandType.Text);
    //    return new PagedData<TModel>
    //    {
    //        QueryResult = data.ToList(),
    //        //TotalCount = GetCount()
    //        TotalCount = data.Count()
    //    };
    //}

    //public async Task<PagedData<TModel>> ReadPagingSpAsync<TModel>(string spName, FilterObject filterObject) where TModel : QueryModel
    //{
    //    try
    //    {
    //        var searchConditions = new List<string>();
    //        var parameters = new DynamicParameters();

    //        //if (!string.IsNullOrWhiteSpace(filterObject.SearchValue))
    //        //{
    //        //    var searchFilters = _jsonSerializer.Deserialize<IEnumerable<ColumnValueFilter>>(filterObject.SearchValue);
    //        //    int index = 0;
    //        //    foreach (var filter in searchFilters!)
    //        //    {
    //        //        string paramName = $"@SearchValue{index}";
    //        //        searchConditions.Add($"{filter.ColumnName} LIKE {paramName}");
    //        //        parameters.Add(paramName, $"%{filter.Value}%"); // Using LIKE for partial search
    //        //        index++;
    //        //    }
    //        //}
    //        //string searchValueCondition = searchConditions.Any() ? "AND (" + string.Join(" OR ", searchConditions) + ")" : string.Empty;

    //        parameters.Add("@PageNumber", filterObject.PageNumber);
    //        parameters.Add("@Take", filterObject.Take);
    //        parameters.Add("@NeedTotalCount", true);
    //        parameters.Add("@SearchValue", filterObject.SearchValue);
    //        parameters.Add("@SortBy", filterObject.DirectionSort);
    //        parameters.Add("@SortAscending", filterObject.DirectionSort.ToLower().Equals("ASC".ToLower()) ? true : false);
    //        parameters.Add("@ColumnSort", filterObject.ColumnSort);
    //        parameters.Add("@InnerWhere", filterObject.InnerWhere);
    //        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

    //        parameters.AddDynamicParams(filterObject.Parameters);


    //        var result = await _connection.QueryAsync<TModel>(
    //            spName,
    //            parameters,
    //            commandType: CommandType.StoredProcedure
    //            );

    //        var totalCount = parameters.Get<int>("@TotalCount");

    //        return new PagedData<TModel>
    //        {
    //            QueryResult = result.ToList(),
    //            PageNumber = filterObject.PageNumber,
    //            Take = filterObject.Take,
    //            TotalCount = totalCount
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new ApplicationException(string.Format("{0} \n {1}", ex.Message, ""));
    //    }
    //}
    //public async Task<PagedData<TModel>> ReadPagingSpAsync<TModel>(string spName, object parameters) where TModel : QueryModel
    //{


    //    // Convert parameters to DynamicParameters to handle output
    //    var dynamicParams = new DynamicParameters(parameters);

    //    // Ensure TotalCount parameter is included as an output parameter
    //    dynamicParams.Add("@count", dbType: DbType.Int32, direction: ParameterDirection.Output);
    //    // Execute stored procedure
    //    var data = await _connection.QueryAsync<TModel>(spName.Trim(), dynamicParams, commandType: CommandType.StoredProcedure);

    //    // Retrieve total count from the output parameter
    //    int totalCount = dynamicParams.Get<int>("@count");

    //    return new PagedData<TModel>
    //    {
    //        QueryResult = data.ToList(),
    //        TotalCount = totalCount,
    //        PageNumber = parameters is FilterObject filter ? filter.PageNumber : 1,
    //        Take = parameters is FilterObject filterObj ? filterObj.Take : 10
    //    };
    //}

    #endregion

    #region ReadFirstOrDefault
    //    public async Task<TModel> ReadFirstOrDefaultAsync<TEntity, TModel>(FilterObject filterObject)
    //        where TEntity : class
    //        where TModel : QueryModel
    //    {
    //        var tableInfo = AttributeHelper.GetTableAttributeInfo(typeof(TEntity));
    //        var data = await _connection.QueryAsync<TModel>($@"
    //SELECT *
    //FROM [{tableInfo.Schema}].[{tableInfo.Schema}]
    //WHERE IsDeleted = 0 
    //AND IsActive = 1
    //{filterObject.InnerWhere}
    //", filterObject, commandType: CommandType.Text);
    //        return data.FirstOrDefault()!;
    //    }
    //    public async Task<TModel> ReadFirstOrDefaultAsync<TModel>(string query, FilterObject filterObject) where TModel : QueryModel
    //        => (await _connection.QueryAsync<TModel>(query, filterObject, commandType: CommandType.Text)).FirstOrDefault()!;

    public async Task<TModel> ReadFirstOrDefaultAsync<TModel>(string query, object parameters) where TModel : QueryModel
        => (await _connection.QueryFirstOrDefaultAsync<TModel>(query, parameters, commandType: CommandType.Text))!;

    public async Task<TModel> ReadFirstOrDefaultBySpAsync<TModel>(string spName, object parameters) where TModel : QueryModel
        => (await _connection.QueryAsync<TModel>(spName.Trim(), parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault()!;

    public async Task<TModel> ReadFirstOrDefaultDynamicAsync<TModel>(string query, object parameters)
        => (await _connection.QueryAsync<TModel>(query, parameters, commandType: CommandType.Text)).FirstOrDefault()!;

    #endregion

    #region ReadSingle
    //    public async Task<TModel> ReadSingleAsync<TEntity, TModel>(FilterObject filterObject)
    //        where TEntity : class
    //        where TModel : QueryModel
    //    {
    //        var tableInfo = AttributeHelper.GetTableAttributeInfo(typeof(TEntity));

    //        var data = await _connection.QueryAsync<TModel>($@"
    //SELECT *
    //FROM [{tableInfo.Schema}].[{tableInfo.Schema}]
    //WHERE IsDeleted = 0 
    //AND IsActive = 1
    //{filterObject.InnerWhere}
    //", filterObject, commandType: CommandType.Text);
    //        return data.Single();
    //    }

    //    public async Task<TModel> ReadSingleAsync<TModel>(string query, FilterObject filterObject) where TModel : QueryModel
    //        => (await _connection.QueryAsync<TModel>(query, filterObject, commandType: CommandType.Text)).Single();

    public async Task<TModel> ReadSingleAsync<TModel>(string query, object parameters) where TModel : QueryModel
        => (await _connection.QueryAsync<TModel>(query, parameters, commandType: CommandType.Text)).Single();

    public async Task<TModel> ReadSingleBySpAsync<TModel>(string spName, object parameters) where TModel : QueryModel
        => await _connection.QuerySingleOrDefaultAsync<TModel>(spName.Trim(), parameters, commandType: CommandType.StoredProcedure);

    public async Task<TModel> ReadSingleDynamicAsync<TModel>(string query, object parameters)
        => (await _connection.QueryAsync<TModel>(query, parameters, commandType: CommandType.Text)).Single();

    #endregion

    #region ReadQuery

    //    public async Task<IEnumerable<TModel>> ReadQueryAsync<TEntity, TModel>(FilterObject filterObject)
    //        where TEntity : class
    //        where TModel : QueryModel
    //    {
    //        var tableInfo = AttributeHelper.GetTableAttributeInfo(typeof(TEntity));

    //        var data = await _connection.QueryAsync<TModel>($@"
    //SELECT *
    //FROM [{tableInfo.Schema}].[{tableInfo.Name}]
    //WHERE IsDeleted = 0 
    //AND IsActive = 1
    //{filterObject.InnerWhere}
    //", filterObject, commandType: CommandType.Text);
    //        return data;
    //    }

    //    public async Task<IEnumerable<TModel>> ReadQueryAsync<TModel>(string query, FilterObject filterObject) where TModel : QueryModel
    //        => await _connection.QueryAsync<TModel>(query, filterObject, commandType: CommandType.Text);

    public async Task<IEnumerable<TModel>> ReadQueryBySpAsync<TModel>(string spName, object parameters) where TModel : QueryModel
        => await _connection.QueryAsync<TModel>(spName.Trim(), parameters, commandType: CommandType.StoredProcedure);

    public async Task<IEnumerable<TModel>> ReadQueryAsync<TModel>(string query, object parameters) where TModel : QueryModel
        => await _connection.QueryAsync<TModel>(query, parameters, commandType: CommandType.Text);

    public async Task<IEnumerable<TModel>> ReadQueryDynamicAsync<TModel>(string query, object parameters)
        => await _connection.QueryAsync<TModel>(query, parameters, commandType: CommandType.Text);
    #endregion

    #region ReadList
    //    public virtual async Task<ReadList<TModel>> ReadListAsync<TEntity, TModel>(FilterObject @object)
    //      where TEntity : class
    //      where TModel : QueryModel
    //    {
    //        var tableInfo = AttributeHelper.GetTableAttributeInfo(typeof(TEntity));

    //        var count = await _connection.ExecuteScalarAsync<int>($@"
    //SELECT COUNT(1)
    //FROM [{tableInfo.Schema}].[{tableInfo.Name}]
    //WHERE IsDeleted = 0 
    //AND IsActive = 1
    //", commandType: CommandType.Text);

    //        var data = await _connection.QueryAsync<TModel>($@"
    //SELECT *
    //FROM [{tableInfo.Schema}].[{tableInfo.Name}]
    //WHERE IsDeleted = 0 
    //AND IsActive = 1
    //{@object.InnerWhere}
    //", @object, commandType: CommandType.Text);
    //        var response = new ReadList<TModel>(count);
    //        response.SetData(data);
    //        return response;
    //    }

    #endregion

    #region TreeList
    public async Task<IEnumerable<TModel>> ReadTreeListAsync<TEntity, TModel>(string innerWhere)
        where TEntity : class
        where TModel : class, ITreeViewModel<TModel>
    {
        innerWhere = innerWhere.Count() > 0 ? $"AND {innerWhere}" : string.Empty;
        Type typeEntity = typeof(TEntity);
        var infoEntity = typeEntity.GetAttribute();

        var result = await _connection.QueryAsync<TModel>($@"
            SELECT *
            FROM {infoEntity}
            WHERE IsDeleted = 0
            AND IsActive = 1
            {innerWhere}
            ORDER BY Id
            ", new { });
        return result.ToList().RecursiveTree();
    }

    #endregion

    #region Scalar
    public async Task<TModel> ExecuteScalarAsync<TModel>(string query, object parameters)
        => (await _connection.ExecuteScalarAsync<TModel>(query, parameters, commandType: CommandType.Text))!;

    public async Task<int> ExecuteAsync(string query, object parameters)
        => await _connection.ExecuteAsync(query, parameters, commandType: CommandType.Text);

    //public Task<IEnumerable<DropDownList<TKey>>> ReadDropDownAsync<TKey>(string query, object? parameters) where TKey : struct, IComparable, IComparable<TKey>, IConvertible, IEquatable<TKey>, IFormattable
    //    => throw new NotImplementedException();

    //public Task<IEnumerable<TModel>> ReadDropDownQueryAsync<TModel>(string query, object? parameters) where TModel : QueryModel, IDropDownList
    //    => throw new NotImplementedException();

    #endregion

}
