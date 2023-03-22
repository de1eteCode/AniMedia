using AniMedia.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Infrastructure.DI;

public static class AppAuthorizationDependencyInjection {

    public static IServiceCollection AddAppAuthorization(this IServiceCollection serviceCollection) {
        serviceCollection.AddScoped<IAuthorizationMiddlewareResultHandler, AuthorizationResultMiddleware>();

        serviceCollection.AddAuthorization();

        return serviceCollection;
    }
}