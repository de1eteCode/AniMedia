using AniMedia.API.Services;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Infrastructure.DI;

namespace AniMedia.API;

public class Program {
    public const string CorsPolicy = nameof(CorsPolicy);

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddMediator();
        builder.Services.AddDatabaseServices(builder.Configuration);
        builder.Services.AddPasswordHashServices();
        builder.Services.AddJwtGeneratorServices(builder.Configuration);
        builder.Services.AddAppAuthentication(builder.Configuration);
        builder.Services.AddAppAuthorization();
        builder.Services.ConfigureCors(CorsPolicy, "https://localhost:7137");
        builder.Services.AddSwagger();

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(CorsPolicy);

        app.MapControllers();

        app.Run();
    }
}