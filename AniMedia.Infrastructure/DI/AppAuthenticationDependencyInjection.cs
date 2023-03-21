using AniMedia.Application.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AniMedia.Infrastructure.DI;

public static class AppAuthenticationDependencyInjection {

    public static IServiceCollection AddAppAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration) {
        serviceCollection
            .AddAuthentication(options => {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options => {
                    options.SaveToken = true;
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

                    options.Events = new JwtBearerEvents {
                        OnMessageReceived = context => {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.Request.Path;

                            if (path.StartsWithSegments("/notification") && !string.IsNullOrEmpty(accessToken)) {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

        return serviceCollection;
    }
}