namespace BaseSource.EndPoint.WebApi.Common.Models;

public class ApiResult<TData>
{
    public TData Data { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}

public static class ApiResult
{
    public static ApiResult<TData> Success<TData>(TData data, string msg = "")
    {
        return new ApiResult<TData>()
        {
            Data = data,
            Message = msg ?? "Success",
            Success = true
        };
    }

    public static ApiResult<TData> Faild<TData>(TData data, string msg = "")
    {
        return new ApiResult<TData>()
        {
            Data = data,
            Message = msg ?? "Faild",
            Success = false
        };
    }
}
