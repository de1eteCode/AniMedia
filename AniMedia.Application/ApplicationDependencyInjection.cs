using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.Common.Behaviours;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Application; 

public static class ApplicationDependencyInjection {

    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration) {
        serviceCollection.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        serviceCollection.Configure<BinaryFileSettings>(configuration.GetSection(nameof(BinaryFileSettings)));
        
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ApplicationAuthorizeBehaviour<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        serviceCollection.AddMediatR(e => e.RegisterServicesFromAssembly(typeof(AuthorizationCommand).Assembly));

        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IHashService, HashService>();
        serviceCollection.AddSingleton<IDirectoryService, DirectoryService>();
        
        return serviceCollection;
    }
}