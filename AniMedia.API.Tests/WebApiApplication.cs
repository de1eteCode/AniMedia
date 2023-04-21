using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AniMedia.API.Tests;

public class WebApiApplication : WebApplicationFactory<Program> {
    
    private readonly string _environment;

    public WebApiApplication(string environment = "Development") {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder) {
        builder.UseEnvironment(_environment);
        

        builder.ConfigureServices(services => {
            services.AddScoped<FakeRemoteIpMiddleware>();
            services.AddSingleton<IStartupFilter, CustomStartupFilter>();
        });
        
        return base.CreateHost(builder);
    }
    
    private class CustomStartupFilter : IStartupFilter {

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) {
            return app => {
                app.UseMiddleware<FakeRemoteIpMiddleware>();
                next(app);
            };
        }
    }
    
    public class FakeRemoteIpMiddleware : IMiddleware {

        private static IPAddress _fakeIp = IPAddress.Parse("127.0.0.1");
    
        public Task InvokeAsync(HttpContext context, RequestDelegate next) {
            context.Connection.RemoteIpAddress = _fakeIp;
            return next(context);
        }

        public static void ChangeIp(string ipAddress) {
            if (IPAddress.TryParse(ipAddress, out var ip) == false) {
                throw new ArgumentException("Not valid ip address");
            }

            _fakeIp = ip;
        }
    }
}