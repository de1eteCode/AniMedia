using AniMedia.Domain.Interfaces;
using AniMedia.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Domain;

public static class DomainDependencyInjection {

    public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection) {
        serviceCollection.AddSingleton<IDateTimeService, DateTimeService>();
        
        return serviceCollection;
    }
}