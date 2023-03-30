using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.Infrastructure.DI;

public static class MediatorDependencyInjection {

    public static IServiceCollection AddMediator(this IServiceCollection serviceCollection) {
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        serviceCollection.AddMediatR(e => e.RegisterServicesFromAssembly(typeof(AuthorizationCommand).Assembly));

        return serviceCollection;
    }
}