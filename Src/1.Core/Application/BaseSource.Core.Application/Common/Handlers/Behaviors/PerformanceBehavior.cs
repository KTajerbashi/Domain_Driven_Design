using MediatR;

namespace BaseSource.Core.Application.Common.Handlers.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
    private readonly System.Diagnostics.Stopwatch _timer;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
        _timer = new System.Diagnostics.Stopwatch();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        var requestName = typeof(TRequest).Name;

        if (elapsedMilliseconds > 500)
        {
            _logger.LogWarning("Long-running Request: {RequestName} ({ElapsedMilliseconds} milliseconds) with payload: {Request}",
                requestName, elapsedMilliseconds, System.Text.Json.JsonSerializer.Serialize(request));
        }
        else
        {
            _logger.LogDebug("Request {RequestName} completed in {ElapsedMilliseconds} ms",
                requestName, elapsedMilliseconds);
        }

        return response;
    }
}


