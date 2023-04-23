using System.Net;
using AniMedia.API.Tests.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
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
            services.AddScoped<FakeRemoteIpSetterMiddleware>();
            services.AddSingleton<IStartupFilter, CustomStartupFilter>();
        });

        return base.CreateHost(builder);
    }

    private class CustomStartupFilter : IStartupFilter {

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) {
            return app => {
                app.UseMiddleware<FakeRemoteIpSetterMiddleware>();
                next(app);
            };
        }
    }

    /// <summary>
    /// Мидлвара для подстановки фейкового IP адреса в запрос на сервер <br/>
    /// Данная фича необходима для некоторых запросов, которые логируют IP адрес клиента
    /// </summary>
    private class FakeRemoteIpSetterMiddleware : IMiddleware {

        private static IPAddress _defaultIp = IPAddress.Parse("127.0.0.1");

        public Task InvokeAsync(HttpContext context, RequestDelegate next) {
            var ipStr = context.ParseRemoteIpFromHeader();

            if (string.IsNullOrEmpty(ipStr) == false && IPAddress.TryParse(ipStr, out var newIp)) {
                context.Connection.RemoteIpAddress = newIp;
            }
            else {
                context.Connection.RemoteIpAddress = _defaultIp;
            }

            return next(context);
        }
    }
}