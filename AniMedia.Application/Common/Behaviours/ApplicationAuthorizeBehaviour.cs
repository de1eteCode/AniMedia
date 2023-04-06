using System.Reflection;
using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Constants;
using AniMedia.Domain.Models.Responses;
using MediatR;

namespace AniMedia.Application.Common.Behaviours;

public class ApplicationAuthorizeBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result {
    private readonly ICurrentUserService _currentUser;

    public ApplicationAuthorizeBehaviour(ICurrentUserService currentUser) {
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        var authAttribute = request.GetType().GetCustomAttribute<ApplicationAuthorizeAttribute>(inherit: true);

        if (authAttribute == null || _currentUser.IsAuthenticated) {
            return await next();
        }

        var res = (TResponse?)Activator.CreateInstance(typeof(TResponse), args: new object?[] {
            new AuthenticationError("Non authorized", ErrorCodesConstants.AuthInvalidToken)
        });

        if (res == null) {
            throw new InvalidOperationException($"Cannot create instance typeof {typeof(TResponse)}");
        }

        return res;
    }
}