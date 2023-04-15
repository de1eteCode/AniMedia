using AniMedia.Domain;
using AniMedia.WebClient.Common.ApiServices;
using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Handlers;
using AniMedia.WebClient.Common.Models;
using AniMedia.WebClient.Common.Providers;
using AniMedia.WebClient.Common.Services;
using Blazored.LocalStorage;
using Blazored.Toast;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AniMedia.WebClient;

public class Program {

    public static async Task Main(string[] args) {
        if (!OperatingSystem.IsBrowser()) {
            throw new PlatformNotSupportedException("Supported only browsers");
        }
        
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.Configure<UploadSettings>(builder.Configuration.GetSection(nameof(UploadSettings)));
        
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // http client
        builder.Services.AddScoped<JwtAuthorizationMessageHandler>();
        builder.Services.AddHttpClient<IApiClient, ApiClient>("AniMedia.API",
                client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiServiceUrl")!))
            .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AniMedia.API"));

        // auth jwt handler
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<JwtAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<JwtAuthenticationStateProvider>());

        // services
        builder.Services.AddDomainServices();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<ITokenService, LocalStorageTokenService>();
        builder.Services.AddSingleton<IJwtTokenReadService, JwtReadService>();
        
        // states
        builder.Services.AddFluxor(opt => opt.ScanAssemblies(typeof(Program).Assembly).UseReduxDevTools());
        
        // ui
        builder.Services.AddBlazoredToast();
        
        await builder.Build().RunAsync();
    }
}