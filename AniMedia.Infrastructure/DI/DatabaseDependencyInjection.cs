using AniMedia.Application.Common.Interfaces;
using AniMedia.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Infrastructure.DI;

public static class DatabaseDependencyInjection {

    public static IServiceCollection AddDatabaseServices(this IServiceCollection serviceCollection, IConfiguration configuration) {
        serviceCollection.AddDbContext<IApplicationDbContext, DatabaseContext>(
            opt => {
                opt.UseNpgsql(configuration.GetConnectionString("ApplicationDB"));
                opt.UseLazyLoadingProxies();
            });

        return serviceCollection;
    }
}