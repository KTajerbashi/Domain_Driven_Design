namespace BaseSource.Core.Application.Common.Handlers.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        _logger.LogDebug("Validating request {RequestName} with {ValidatorCount} validators",
            typeof(TRequest).Name, _validators.Count());

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
        {
            var indexedErrors = failures
                .Select((f, i) => $"{i + 1}: {f.PropertyName} => {f.ErrorMessage}")
                .ToList();

            _logger.LogWarning("Validation failed for request {RequestName} with {ErrorCount} errors: {Errors}",
                typeof(TRequest).Name, failures.Count, string.Join(" | ", indexedErrors));

            // throw with indexed errors (optional enhancement)
            throw new ValidationException(indexedErrors.Select((msg, i) =>
                new FluentValidation.Results.ValidationFailure("", msg)).ToList());
        }

        _logger.LogDebug("Validation successful for request {RequestName}", typeof(TRequest).Name);

        return await next();
    }
}


