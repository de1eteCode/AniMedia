using AniMedia.API.Tests.HttpClients;
using AniMedia.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Xunit;

namespace AniMedia.API.Tests;

[Collection("Sequential")]
public abstract class ApiTestBase : IAsyncLifetime {

    private readonly WebApplicationFactory<Program> _app;
    private readonly TestServer _server;
    private readonly IServiceProvider _serviceProvider;
    protected readonly IApiClient _apiClient;


    protected ApiTestBase() {
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        _app = new WebApiApplication()
            .WithWebHostBuilder(builder => {
                builder.UseConfiguration(configurationRoot);
            });
        
        _serviceProvider = _app.Services;
        _server = _app.Server;

        var httpClient = _server.CreateClient();
        
        httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Testing env");
        
        _apiClient = new ApiClient(httpClient);
    }

    public async Task InitializeAsync() {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await context.Database.MigrateAsync();
        await context.Clear();
    }

    [Fact]
    public abstract Task Test();

    public static void ChangeIp(string ipAddress) {
        WebApiApplication.FakeRemoteIpMiddleware.ChangeIp(ipAddress);
    }
    
    public Task DisposeAsync() {
        return Task.CompletedTask;
    }
}