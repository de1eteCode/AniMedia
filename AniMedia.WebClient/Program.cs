using System.Runtime.InteropServices.JavaScript;
using AniMedia.Domain.Interfaces;
using AniMedia.Domain.Services;
using AniMedia.WebClient.Common.ApiServices;
using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Handlers;
using AniMedia.WebClient.Common.Providers;
using AniMedia.WebClient.Common.Services;
using AniMedia.WebClient.Pages.Account.Components;
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
        builder.Services.AddHttpClient<IApiClient, ApiClient>("AniMedia.API",
                client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiServiceUrl")!))
            .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();
        //.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AniMedia.API"));

        /// auth jwt handler
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<JwtAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<JwtAuthenticationStateProvider>());

        /// services
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<ITokenService, LocalStorageTokenService>();
        builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
        builder.Services.AddSingleton<IJwtTokenReadService, JwtReadService>();

        await LoadJsIsolated();
        
        await builder.Build().RunAsync();
    }

    private static async Task LoadJsIsolated() {
        if (!OperatingSystem.IsBrowser()) {
            throw new PlatformNotSupportedException("Supported only browsers");
        }

        await JSHost.ImportAsync(nameof(EditProfileComponent), $"../Pages/Account/Components/{nameof(EditProfileComponent)}.razor.js");
    }
}