using AniMedia.API.Tests.Helpers;
using AniMedia.API.Tests.HttpClients;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AniMedia.API.Tests;

[Collection("SequentialApi")]
public abstract class ApiTestBase : IAsyncLifetime {

    public WebApplicationFactory<Program> App { get; }

    public IServiceProvider ServiceProvider { get; }

    protected ApiTestBase() {
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        App = new WebApiApplication()
            .WithWebHostBuilder(builder => {
                builder.UseConfiguration(configurationRoot);
            });
        
        ServiceProvider = App.Services;
    }

    public async Task InitializeAsync() {
        await using var scope = ServiceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await context.Database.MigrateAsync();
        await context.Clear();
    }

    /// <summary>
    /// Метод тестирования
    /// </summary>
    /// <returns></returns>
    [Fact]
    public abstract Task Test();

    /// <summary>
    /// Создание нового клиента
    /// </summary>
    /// <returns></returns>
    protected (IApiClient, HttpClient) GetClient() {
        var httpClient = App.Server.CreateClient();
        var apiClient = new ApiClient(httpClient);
        
        httpClient.SetUserAgent();
        httpClient.SetRemoteIp();
        
        return (apiClient, httpClient);
    }
    
    public async Task DisposeAsync() {
        await using var scope = ServiceProvider.CreateAsyncScope();
        
        var dirService = scope.ServiceProvider.GetRequiredService<IDirectoryService>();

        var pathContent = dirService.GetBinaryFilesDirectory();

        foreach (var filePath in Directory.GetFiles(pathContent, "*.*")) {
            var fInfo = new FileInfo(filePath);

            if (fInfo.Exists) {
                fInfo.Delete();
            }
        }
    }
}