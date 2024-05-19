using DDD.Core.RequestResponse.Library.Common;

namespace DDD.Core.RequestResponse.Library.Queries;

/// <summary>
/// نتیجه یک کوئری را بازگشت می‌دهد
/// </summary>
/// <typeparam name="TData"></typeparam>
public sealed class QueryResult<TData> : ApplicationServiceResult
{
    public TData? _data;
    public TData? Data
    {
        get
        {
            return _data;
        }
    }
}

