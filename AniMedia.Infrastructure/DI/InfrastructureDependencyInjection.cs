using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Infrastructure.DI;

public static class InfrastructureDependencyInjection {
    public const string CorsPolicy = nameof(CorsPolicy);

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration) {
        serviceCollection.AddMediator();
        serviceCollection.AddDatabaseServices(configuration);
        serviceCollection.AddPasswordHashServices();
        serviceCollection.AddJwtGeneratorServices(configuration);
        serviceCollection.AddAppAuthentication(configuration);
        serviceCollection.AddAppAuthorization();
        serviceCollection.AddBinaryFileServices(configuration);

        foreach (var address in configuration.GetSection("CORSAllowed").Get<List<string>>() ?? Enumerable.Empty<string>()) {
            serviceCollection.ConfigureCors(CorsPolicy, address);
        }

        serviceCollection.AddSwagger();

        return serviceCollection;
    }

    public static IApplicationBuilder UseInfrastructureServices(this IApplicationBuilder app, bool isDev) {
        if (isDev) {
            app.UseSwagger();
        }

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(CorsPolicy);

        return app;
    }
}