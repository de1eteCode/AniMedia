using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationExtensions {

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {
        return services;
    }
}