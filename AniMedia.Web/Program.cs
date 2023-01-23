using AniMedia.Web.Contracts;
using AniMedia.Web.Data;
using AniMedia.Web.Services;
using AniMedia.Web.Services.Base;
using AniMedia.Web.Services.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AniMedia.Web;

public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();

        // auth options
        builder.Services.Configure<CookiePolicyOptions>(options => {
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.LoginPath = "/account/login";
            });

        builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

        builder.Services.AddHttpClient<IApiClient, ApiClient>(e => e.BaseAddress = new Uri(builder.Configuration["ApiServiceUrl"]!));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseCookiePolicy();
        app.UseAuthentication();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}