using AniMedia.API.Common.Services;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Infrastructure.DI;

namespace AniMedia.API;

public class Program {
    public const string CorsPolicy = nameof(CorsPolicy);

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseInfrastructureServices(app.Environment.IsDevelopment());

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}