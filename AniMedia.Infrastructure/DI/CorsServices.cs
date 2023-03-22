using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Infrastructure.DI;

public static class CorsServices {

    public static IServiceCollection ConfigureCors(
        this IServiceCollection services, string policyName, string allowOrigins) {
        services.AddCors(options => {
            options.AddPolicy(policyName, builder => {
                builder.WithOrigins(allowOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}