using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Application.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Infrastructure.DI;

public static class BinaryFileDependencyInjection {

    public static IServiceCollection AddBinaryFileServices(this IServiceCollection serviceCollection, IConfiguration configuration) {
        serviceCollection.Configure<BinaryFileSettings>(configuration.GetSection(nameof(BinaryFileSettings)));

        serviceCollection.AddScoped<IDirectoryService, DirectoryService>();

        return serviceCollection;
    }
}