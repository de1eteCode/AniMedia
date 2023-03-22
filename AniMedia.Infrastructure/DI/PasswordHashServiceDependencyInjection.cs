using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Infrastructure.DI;

public static class PasswordHashServiceDependencyInjection {

    public static IServiceCollection AddPasswordHashServices(this IServiceCollection serviceCollection) {
        serviceCollection.AddScoped<IHashService, HashService>();

        return serviceCollection;
    }
}