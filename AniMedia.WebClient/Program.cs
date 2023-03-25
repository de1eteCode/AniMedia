using AniMedia.WebClient.Common.ApiServices;
using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Handlers;
using AniMedia.WebClient.Common.Providers;
using AniMedia.WebClient.Common.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AniMedia.WebClient;

public class Program {

    public static async Task Main(string[] args) {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        /// http client
        builder.Services.AddScoped<JwtAuthorizationMessageHandler>();
        builder.Services.AddHttpClient<IApiClient, ApiClient>("AniMedia.API", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiServiceUrl")!))
            .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();
        //.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AniMedia.API"));

        /// auth jwt handler
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<JwtAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        /// services
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<ITokenService, LocalStorageTokenService>();
        builder.Services.AddSingleton<IJwtTokenReadService, JwtReadService>();

        await builder.Build().RunAsync();
    }
}