using AniMedia.Application.Contracts.Identity;
using AniMedia.Application.Models.Identity;
using AniMedia.Identity;
using AniMedia.Identity.Models;
using AniMedia.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityExtensions {

    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        services.AddDbContext<ApplicationIdentityDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("IdentityDB"),
            b => b.MigrationsAssembly(typeof(ApplicationIdentityDbContext).Assembly.FullName)));

        services
            .AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IAuthorizationService, AuthorizationService>();

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Issuer)}"],
                    ValidAudience = configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Audience)}"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Key)}"]!))
                };
            });

        return services;
    }
}