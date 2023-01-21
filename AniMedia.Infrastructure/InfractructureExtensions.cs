using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfractructureExtensions {

    public static IServiceCollection AddInfractructure(this IServiceCollection services, IConfiguration configuration) {
        return services;
    }
}