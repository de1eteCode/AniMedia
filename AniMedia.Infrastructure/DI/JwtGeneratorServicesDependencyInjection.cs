using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Application.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Infrastructure.DI;

public static class JwtGeneratorServicesDependencyInjection {

    public static IServiceCollection AddJwtGeneratorServices(this IServiceCollection serviceCollection,
        IConfiguration configuration) {
        serviceCollection.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        serviceCollection.AddScoped<ITokenService, TokenService>();

        return serviceCollection;
    }
}