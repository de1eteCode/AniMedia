using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Infrastructure.DI;
using AniMedia.IntegrationTests.Mocks;
using AniMedia.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AniMedia.IntegrationTests;

public abstract class IntegrationTestBase : IAsyncLifetime {
    private Guid? _currentUserUid = null;

    protected DatabaseContext ApplicationDbContext { get; }

    private IServiceProvider ServiceProvider { get; }

    public IntegrationTestBase() {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Tests.json")
            .Build();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IConfiguration>(configuration);

        serviceCollection.AddMediator();
        serviceCollection.AddDatabaseServices(configuration);
        serviceCollection.AddPasswordHashServices();
        serviceCollection.AddJwtGeneratorServices(configuration);
        serviceCollection.AddAppAuthentication(configuration);
        serviceCollection.AddAppAuthorization();
        serviceCollection.AddScoped<ICurrentUserService, MockCurrentUserService>();

        ServiceProvider = serviceCollection.BuildServiceProvider();

        ApplicationDbContext = ServiceProvider.GetRequiredService<DatabaseContext>() ??
            throw new InvalidOperationException("DatabaseContext service is not registered in the DI");
    }

    [Fact]
    public abstract Task Test();

    protected async Task<TResult> RequestAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default) {
        using var scope = ServiceProvider.CreateScope();

        var currentUserService = scope.ServiceProvider.GetService<ICurrentUserService>() as MockCurrentUserService ??
            throw new InvalidOperationException("ICurrentUserService is not expected MockCurrentUserService");

        currentUserService.SetUid(_currentUserUid);

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        return await mediator.Send(request, cancellationToken);
    }

    protected void SetUser(Guid uid) => _currentUserUid = uid;

    protected void SetUser(UserEntity user) => _currentUserUid = user.UID;

    protected void ClearUser() => _currentUserUid = null;

    public async Task InitializeAsync() {
        await ApplicationDbContext.Database.MigrateAsync();
        await ApplicationDbContext.Clear();
    }

    public Task DisposeAsync() {
        return Task.CompletedTask;
    }
}