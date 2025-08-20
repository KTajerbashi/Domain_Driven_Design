using Microsoft.AspNetCore.Http;

namespace BaseSource.Core.Application.Common.Handlers.Behaviors;

public interface IAuthorizable
{
    string[] Roles { get; }
}
public record DeleteUserCommand(Guid Id) : IRequest<Unit>, IAuthorizable
{
    public string[] Roles => new[] { "Admin" };
}
public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IAuthorizable authRequest)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true ||
                !authRequest.Roles.Any(role => user.IsInRole(role)))
            {
                throw new UnauthorizedAccessException("You are not authorized.");
            }
        }

        return await next();
    }
}
