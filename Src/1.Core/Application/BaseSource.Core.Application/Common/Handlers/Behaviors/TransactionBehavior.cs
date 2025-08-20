using MediatR;

namespace BaseSource.Core.Application.Common.Handlers.Behaviors;

//public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//{
//    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
//    private readonly ApplicationDbContext _context;

//    public TransactionBehavior(
//        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
//        ApplicationDbContext context)
//    {
//        _logger = logger;
//        _context = context;
//    }

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        var requestName = typeof(TRequest).Name;
//        var isCommand = requestName.EndsWith("Command");

//        // Only apply transaction to commands (write operations)
//        if (!isCommand)
//        {
//            _logger.LogDebug("Skipping transaction for query: {RequestName}", requestName);
//            return await next();
//        }

//        _logger.LogInformation("Beginning transaction for command: {RequestName}", requestName);

//        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

//        try
//        {
//            var response = await next();

//            await _context.SaveChangesAsync(cancellationToken);
//            await transaction.CommitAsync(cancellationToken);

//            _logger.LogInformation("Transaction committed for command: {RequestName}", requestName);

//            return response;
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error handling transaction for command {RequestName}. Rolling back.", requestName);
//            await transaction.RollbackAsync(cancellationToken);
//            throw;
//        }
//    }
//}


